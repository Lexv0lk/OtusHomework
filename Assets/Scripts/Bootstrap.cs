using Client.Configs;
using Client.Services;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private PoolService _poolService;
        [SerializeField] private GameObject _endGameView;
        [SerializeField] private VFXConfig _vfxConfig;
        [SerializeField] private Entity[] _bases;
        [SerializeField] private EcsStartup _ecsStartup;
        
        private void Awake()
        {
            EntityManager entityManager = new EntityManager(_poolService);
            EndGameController endGameController = new EndGameController(entityManager, _endGameView, _bases);
            
            _ecsStartup.Initialize(entityManager, _vfxConfig);
        }
    }
}