using Sirenix.OdinInspector;
using UnityEngine;

namespace Storage
{
    public class StorageMock : MonoBehaviour
    {
        [ShowInInspector] private int _goldAmount;
        [ShowInInspector] private int _rubyAmount;

        public void AddGold(int val) => _goldAmount += val;
        public void AddRuby(int val) => _rubyAmount += val;
    }
}