using UnityEngine;

namespace Time.Configs
{
    [CreateAssetMenu(fileName = "Server Time Config", menuName = "Configs/ServerTime")]
    public class ServerTimeConfig : ScriptableObject
    {
        [SerializeField] private string _serverTimeUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";
        [SerializeField] private float _fetchTimeDelay = 180f;
        
        public string ServerTimeUrl => _serverTimeUrl;
        public float FetchTimeDelay => _fetchTimeDelay;
    }
}