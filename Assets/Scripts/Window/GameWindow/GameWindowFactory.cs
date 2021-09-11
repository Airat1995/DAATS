using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.GameWindow.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowFactory : IWindowFactory<IGameWindowController>
    {
        private readonly IWindowResourceManager _resourceManager;
        private readonly DiContainer _container;

        public GameWindowFactory(IWindowResourceManager resourceManager, DiContainer container)
        {
            _resourceManager = resourceManager;
            _container = container;
        }

        public IGameWindowController GetWindowController(Transform parent)
        {
            var gameWindowView = _resourceManager.LoadWindowView<GameWindowView>(parent);
            var gameWindowPresenter = new GameWindowPresenter(gameWindowView, _container);
            var gameWindowController = new GameWindowController(gameWindowPresenter);

            return gameWindowController;

        }

        public void UnloadWindow()
        {
            _resourceManager.UnloadWindowView<GameWindowView>();
        }
    }
}