using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.MainWindow;
using DAATS.Initializer.System.Window.MainWindow.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.System.Window.Fabric
{
    public class MainWindowFactory : IWindowFactory<IMainWindowController>
    {
        private readonly IWindowResourceManager _resourceManager;
        private readonly DiContainer _container;
        
        public MainWindowFactory(IWindowResourceManager resourceManager, DiContainer container)
        {
            _container = container;
            _resourceManager = resourceManager;
        }

        public IMainWindowController GetWindowController(Transform parent)
        {
            var mainWindowView = _resourceManager.LoadWindowView<MainWindowView>(parent);
            var mainWindowPresenter = new MainWindowPresenter(mainWindowView, _container);
            var mainWindowController = new MainWindowController(mainWindowPresenter);

            return mainWindowController;
        }

        public void UnloadWindow()
        {
            _resourceManager.UnloadWindowView<MainWindowView>();
        }
    }
}