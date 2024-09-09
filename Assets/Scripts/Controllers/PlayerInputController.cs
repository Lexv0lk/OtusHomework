using System;
using DI;
using UI;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private readonly TeamViewSetup _teamViewSetup;

        public event Action<HeroView> Clicked;

        public PlayerInputController(TeamViewSetup teamViewSetup)
        {
            _teamViewSetup = teamViewSetup;
        }

        public void Initialize()
        {
            _teamViewSetup.BlueTeam.OnHeroClicked += OnHeroViewClicked;
            _teamViewSetup.RedTeam.OnHeroClicked += OnHeroViewClicked;
        }

        public void Dispose()
        {
            _teamViewSetup.BlueTeam.OnHeroClicked -= OnHeroViewClicked;
            _teamViewSetup.RedTeam.OnHeroClicked -= OnHeroViewClicked;
        }

        private void OnHeroViewClicked(HeroView view)
        {
            Clicked?.Invoke(view);
        }
    }
}