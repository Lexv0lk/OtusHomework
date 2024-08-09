using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.DataSaving
{
    public class FileEncryptedSaver : IDataSaver
    {
        public void SaveData(string value)
        {
            byte[] enctryptedBytes;

            using (Aes aes = Aes.Create())
            {
                
            }
        }

        public bool TryGetData(out string result)
        {
            
        }
    }
    
    public class PlayerPrefsSaver : IDataSaver
    {
        private const string DATA_SAVE_KEY = "Save";

        public void SaveData(string value)
        {
            PlayerPrefs.SetString(DATA_SAVE_KEY, value);
        }

        public bool TryGetData(out string result)
        {
            result = null;
            
            if (PlayerPrefs.HasKey(DATA_SAVE_KEY))
            {
                result = PlayerPrefs.GetString(DATA_SAVE_KEY);
                return true;
            }

            return false;
        }
    }
}