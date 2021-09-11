using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.FogFollowWindow.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Window.FogFollowWindow
{
    class FogFollowWindowFactory : IWindowFactory<IFogFollowWindowController>
    {

        private readonly IWindowResourceManager _resourceManager;
        private readonly DiContainer _container;

        public FogFollowWindowFactory(IWindowResourceManager resourceManager, DiContainer container)
        {
            _resourceManager = resourceManager;
            _container = container;
        }

        public IFogFollowWindowController GetWindowController(Transform parent)
        {
            var fogFollowWindowView = _resourceManager.LoadWindowView<FogFollowWindowView>(parent);
            var fogFollowWindowPresenter = new FogFollowWindowPresenter(fogFollowWindowView, _container);
            var fogFollowWindowController = new FogFollowWindowController(fogFollowWindowPresenter);

            return fogFollowWindowController;
        }

        public void UnloadWindow()
        {
            _resourceManager.UnloadWindowView<FogFollowWindowView>();
        }
    }
}
