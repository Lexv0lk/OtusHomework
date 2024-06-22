using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().HitPointsEnded += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _character.GetComponent<HitPointsComponent>().HitPointsEnded -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject character)
        {
            _gameManager.FinishGame();
        }
    }
}