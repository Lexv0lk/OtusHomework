using System;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.DataSaving
{
    public class EncryptedFileSaver : IDataSaver
    {
        private const string FILENAME = "SaveEnc.txt";
        private string _lastKey;

        public void SaveData(string value)
        {
            if (_lastKey != null)
            {
                PlayerPrefs.DeleteKey(_lastKey);
                _lastKey = null;
            }

            byte[] encryptedBytes = Encrypt(value);
            File.WriteAllBytes(FILENAME, encryptedBytes);
        }

        public bool TryGetData(out string result)
        {
            result = null;
            
            if (File.Exists(FILENAME))
            {
                try
                {
                    result = Decrypt(File.ReadAllBytes(FILENAME));
                    return true;
                }
                catch (PlayerPrefsException e)
                {
                    Debug.LogError(e.Message);
                    return false;
                }
            }

            return false;
        }

        private byte[] Encrypt(string value)
        {
            byte[] encryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(value);
                    
                    encryptedBytes = ms.ToArray();
                }

                SaveEncryptionKey(encryptedBytes, aes.Key);
            }

            return encryptedBytes;
        }

        private string Decrypt(byte[] encryptedBytes)
        {
            byte[] key = LoadEncryptionKey(encryptedBytes);
            string result;

            using (Aes aes = Aes.Create())
            {
                byte[] IV = new byte[aes.IV.Length];

                ICryptoTransform decryptor;

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    ms.Read(IV, 0, IV.Length);

                    aes.IV = IV;
                    aes.Key = key;
                    
                    decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        using (StreamReader sr = new StreamReader(cs))
                            result = sr.ReadToEnd();
                }
            }

            return result;
        }

        private void SaveEncryptionKey(byte[] encryptedBytes, byte[] encryptionKey)
        {
            _lastKey = ComputeHashKey(encryptedBytes);

            string serializedEncryptionKey = JsonConvert.SerializeObject(encryptionKey);
            PlayerPrefs.SetString(_lastKey, serializedEncryptionKey);
        }

        private byte[] LoadEncryptionKey(byte[] encryptedBytes)
        {
            _lastKey = ComputeHashKey(encryptedBytes);

            if (PlayerPrefs.HasKey(_lastKey) == false)
                throw new PlayerPrefsException("No such key " + _lastKey);

            string json = PlayerPrefs.GetString(_lastKey);
            return JsonConvert.DeserializeObject<byte[]>(json);
        }

        private string ComputeHashKey(byte[] encryptedBytes)
        {
            string result = null;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(encryptedBytes);
                result = BitConverter.ToString(hashBytes).Replace("-", "");
            }
            
            return result;
        }
    }
}