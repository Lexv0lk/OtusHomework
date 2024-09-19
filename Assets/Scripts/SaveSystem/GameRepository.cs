using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem.DataSaving;

namespace SaveSystem
{
    public class GameRepository
    {
        private readonly IDataSaver _dataSaver;
        private readonly JsonSerializerSettings _serializerSettings;
        
        private Dictionary<string, string> _gameState = new();

        public GameRepository(IDataSaver dataSaver)
        {
            _dataSaver = dataSaver;
            _serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
        }

        public void LoadState()
        {
            if (_dataSaver.TryGetData(out string result))
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        }

        public void SaveState()
        {
            string saveStr = JsonConvert.SerializeObject(_gameState);
            _dataSaver.SaveData(saveStr);
        }

        public T GetData<T>()
        {
            string serializedData = _gameState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData, _serializerSettings);
        }

        public void SetData<T>(T data)
        {
            string serializedData = JsonConvert.SerializeObject(data, _serializerSettings);
            string key = typeof(T).Name;

            _gameState[key] = serializedData;
        }

        public bool TryGetData<T>(out T data)
        {
            data = default;
            string key = typeof(T).Name;

            if (_gameState.ContainsKey(key))
            {
                data = JsonConvert.DeserializeObject<T>(_gameState[key], _serializerSettings);
                return true;
            }

            return false;
        }
    }
}