using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Time.Configs;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Time
{
    public class ServerTimeController : IInitializable
    {
        public bool IsActualTimeReceived { get; private set; }
        
        private readonly ServerTimeConfig _config;

        private DateTime _serverBaseTime;
        private DateTime _localBaseTime;
        private TimeSpan _elapsedTime;
        private bool _isFetchingTime;

        public ServerTimeController(ServerTimeConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            StartFetchingTime().Forget();
        }

        public DateTime GetCurrentTime()
        {
            if (IsActualTimeReceived == false)
                throw new Exception("Actual time wasn't yet received from the server");
            
            _elapsedTime = DateTime.Now - _localBaseTime;
            return _serverBaseTime.Add(_elapsedTime);
        }

        private async UniTaskVoid StartFetchingTime()
        {
            while (true)
            {
                if (_isFetchingTime == false)
                    await FetchServerTime();
                
                await UniTask.Delay((int)_config.FetchTimeDelay * 1000, DelayType.Realtime);
            }
        }

        private async UniTask FetchServerTime()
        {
            _isFetchingTime = true;
            
            Debug.Log("Started fetching...");
            var webRequest = UnityWebRequest.Get(_config.ServerTimeUrl);
            await webRequest.SendWebRequest().ToUniTask();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ServerTimeResponse response = JsonConvert.DeserializeObject<ServerTimeResponse>(webRequest.downloadHandler.text);

                _serverBaseTime = DateTime.Parse(response.DateTime);
                _localBaseTime = DateTime.Now;
                IsActualTimeReceived = true;
                
                Debug.Log($"Fetched server time: {_serverBaseTime}");
            }
            else
            {
                Debug.LogError($"Failed to fetch server time. Error: {webRequest.error}");
                await FetchServerTime();
            }

            _isFetchingTime = false;
        }
    }

    [Serializable]
    public class ServerTimeResponse
    {
        [JsonProperty("datetime")]
        public string DateTime;
    }
}