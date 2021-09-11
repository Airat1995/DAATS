using DAATS.Initializer.System.Window.GameWindow.Interface;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using Zenject;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowPresenter : IGameWindowPresenter, IGameWindowEventReceiver
    {
        private readonly IGameWindowView _gameWindowView;
        private readonly DiContainer _container;

        private WindowManager _windowManager => _container.Resolve<WindowManager>();

        public GameWindowPresenter(IGameWindowView gameWindowView, DiContainer container)
        {
            _gameWindowView = gameWindowView;
            _container = container;
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