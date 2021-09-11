using DAATS.Initializer.GameWorld.Loader.Interface;
using DAATS.Initializer.System.Window.MainWindow.Interface;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using Zenject;

namespace DAATS.Initializer.System.Window.MainWindow
{
    public class MainWindowPresenter : IMainWindowPresenter, IMainWindowEventReceiver
    {
        private readonly IMainWindowView _mainWindowView;
        private readonly DiContainer _container;
        
        private WindowManager _windowManager => _container.Resolve<WindowManager>();
        private IGameWorldLoader _gameWorldLoader => _container.Resolve<IGameWorldLoader>();

        public MainWindowPresenter(IMainWindowView mainWindowView, DiContainer container)
        {
            _mainWindowView = mainWindowView;
            _container = container;
            
            _mainWindowView.SetEventReceiver(this);
        }

        public void SetActive(bool enable)
        {
            if(enable)
                _mainWindowView.Open();
            else
                _mainWindowView.Close();
        }

        public void Update(float deltaTime)
        {
        }

        public void Open()
        {
            _mainWindowView.Open();
        }

        public void Close()
        {
            _mainWindowView.Close();
        }

        public void OnStart()
        {
            _windowManager.CloseWindow<IMainWindowController>();
            _gameWorldLoader.CreateGameplayWorld();
        }

        public void OnSettings()
        {
            _windowManager.OpenWindow<ISettingsWindowController>();
        }
    }
}