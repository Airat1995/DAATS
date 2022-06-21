using DAATS.Component.Interface;
using DAATS.Window.FogFollowWindow.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Window.FogFollowWindow
{
    class FogFollowWindowPresenter : IFogFollowWindowPresenter
    {
        private readonly IPlayer _player;
        private readonly IFogFollowWindowView _window;
        private readonly Camera _camera;

        public FogFollowWindowPresenter(IFogFollowWindowView window, DiContainer container)
        {
            _player = container.Resolve<IPlayer>();
            _window = window;
            _camera = Camera.main;
        }

        public void Close()
        {
            _window.Close();
        }

        public void Open()
        {
            _window.Open();
        }

        public void SetActive(bool enable)
        {
            if(enable)
                _window.Open();
            else
                _window.Close();
        }

        public void Update(float deltaTime)
        {
            var objectPosition = _camera.WorldToScreenPoint(_player.Transform.position, Camera.MonoOrStereoscopicEye.Mono);
        }
    }
}
