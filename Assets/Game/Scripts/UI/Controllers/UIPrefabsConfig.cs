using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    [CreateAssetMenu(fileName = "UI Prefabs Config", menuName = "Configs/UI Prefabs")]
    public class UIPrefabsConfig : ScriptableObject
    {
        [SerializeField] private AssetReference[] _prefabs;
        
        public AssetReference[] Prefabs => _prefabs;
    }
}