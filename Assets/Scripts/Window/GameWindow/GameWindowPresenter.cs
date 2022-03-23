using DAATS.Initializer.System.Window.GameWindow.Interface;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using DAATS.System;
using DAATS.System.Interface;
using Zenject;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowPresenter : IGameWindowPresenter, IGameWindowEventReceiver
    {
        private readonly IGameWindowView _gameWindowView;
        private readonly DiContainer _container;

        private WindowManager _windowManager => _container.Resolve<WindowManager>();

        private IPlayerHealthSystem _playerHealthSystem => _container.Resolve<PlayerHealthSystem>();

        public GameWindowPresenter(IGameWindowView gameWindowView, DiContainer container)
        {
            _gameWindowView = gameWindowView;
            _gameWindowView.SetEventReceiver(this);
            _container = container;
            _playerHealthSystem.SubscribeOnHealthChange(OnHealthChange);
        }

        private void OnHealthChange(uint currentHealth, uint maxHealth)
        {
            float leftPercent = 0;
            if (currentHealth != 0)            
                leftPercent = maxHealth/currentHealth;            
            _gameWindowView.UpdateHealth(leftPercent);
        }

        public void SetActive(bool enable)
        {
            if(enable)
                _gameWindowView.Open();
            else
                _gameWindowView.Close();
        }

        public void Update(float deltaTime)
        {
        }

        public void Open()
        {
            _gameWindowView.Open();
        }

        public void Close()
        {
            _gameWindowView.Close();
        }

        public void Pause()
        {
            _windowManager.OpenWindow<ISettingsWindowController>();
        }
    }
}