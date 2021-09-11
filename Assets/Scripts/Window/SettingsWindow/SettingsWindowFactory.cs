using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.Mangers.Sound.Interface;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using DAATS.UserData.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.System.Window.SettingsWindow
{
    public class SettingsWindowFactory : IWindowFactory<ISettingsWindowController>
    {
        private readonly IWindowResourceManager _windowResource;
        private readonly DiContainer _container;
        private readonly ISoundManager _soundManager;
        private readonly IGetUserSettingsData _getUserSettings;
        private readonly ISetUserSettingsData _setUserSettings;

        public SettingsWindowFactory(IWindowResourceManager windowResource, DiContainer container)
        {
            _windowResource = windowResource;
            _container = container;
            _soundManager = container.Resolve<ISoundManager>();
            _getUserSettings = container.Resolve<IGetUserSettingsData>();
            _setUserSettings = container.Resolve<ISetUserSettingsData>();
        }

        public ISettingsWindowController GetWindowController(Transform parent)
        {
            var userDataController = new SettingsUserDataController(_setUserSettings, _getUserSettings, _soundManager);
            var view = _windowResource.LoadWindowView<SettingsWindowView>(parent);
            var presenter = new SettingsWindowPresenter(view, userDataController, _container);
            var controller = new SettingsWindowController(presenter);

            return controller;
        }

        public void UnloadWindow()
        {
            _windowResource.UnloadWindowView<SettingsWindowView>();
        }
    }
}