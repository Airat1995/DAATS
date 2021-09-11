using System;
using System.Collections.Generic;
using System.Linq;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.FogFollowWindow.Interface;
using DAATS.Initializer.System.Window.GameWindow;
using DAATS.Initializer.System.Window.GameWindow.Interface;
using DAATS.Initializer.System.Window.Interface;
using DAATS.Initializer.System.Window.MainWindow.Interface;
using DAATS.Initializer.System.Window.SettingsWindow;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using DAATS.Window.FogFollowWindow;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.System.Window.Fabric
{
    public class AbstractWindowFactory : IAbstractWindowFactory
    {
        private Dictionary<Type, IWindowFactory<IWindowController>> _windowControllers =
            new Dictionary<Type, IWindowFactory<IWindowController>>();

        public AbstractWindowFactory(IWindowResourceManager windowResourceManager, DiContainer container)
        {
            var mainWindowFactory = new MainWindowFactory(windowResourceManager, container);
            var gameWindowFactory = new GameWindowFactory(windowResourceManager, container);
            var settingsWindowFactory = new SettingsWindowFactory(windowResourceManager, container);
            var fogFollowWindowFactory = new FogFollowWindowFactory(windowResourceManager, container);
            _windowControllers.Add(typeof(IMainWindowController), mainWindowFactory);
            _windowControllers.Add(typeof(IGameWindowController), gameWindowFactory);
            _windowControllers.Add(typeof(ISettingsWindowController), settingsWindowFactory);
            _windowControllers.Add(typeof(IFogFollowWindowController), fogFollowWindowFactory);
        }

        public IWindowController GetWindowController<T>(Transform parent) where T : IWindowController
        {
            return _windowControllers.FirstOrDefault(window => window.Key == typeof(T)).Value.GetWindowController(parent);
        }

        public void RemoveWindow<T>() where T : IWindowController
        {
            _windowControllers.FirstOrDefault(window => window.Key == typeof(T)).Value.UnloadWindow();
        }

        public void RemoveWindow(Type type)
        {
            _windowControllers.FirstOrDefault(window => window.Key == type).Value.UnloadWindow();
        }
    }
}