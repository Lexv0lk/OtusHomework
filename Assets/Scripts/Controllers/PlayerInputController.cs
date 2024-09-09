using System;
using DI;
using UI;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private readonly UIService _uiService;

        public event Action<HeroView> Clicked;

        public PlayerInputController(UIService _uiService)
        {
            this._uiService = _uiService;
        }

        public void Initialize()
        {
            _uiService.GetBluePlayer().OnHeroClicked += OnHeroViewClicked;
            _uiService.GetRedPlayer().OnHeroClicked += OnHeroViewClicked;
        }

        public void Dispose()
        {
            _uiService.GetBluePlayer().OnHeroClicked -= OnHeroViewClicked;
            _uiService.GetRedPlayer().OnHeroClicked -= OnHeroViewClicked;
        }

        private void OnHeroViewClicked(HeroView view)
        {
            Clicked?.Invoke(view);
        }
    }
}