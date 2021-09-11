using System;
using System.Collections.Generic;
using DAATS.Initializer.System.Window.Fabric.Interface;
using DAATS.Initializer.System.Window.Interface;
using DAATS.Initializer.System.Window.MainWindow.Interface;
using DAATS.System.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DAATS.Initializer.System.Window
{
    public class WindowManager : IUpdatableSystem
    {
        private Transform _mainCanvas;
        private readonly IAbstractWindowFactory _abstractWindowFactory;
        private Dictionary<Type, IWindowController> _openedWindows = new Dictionary<Type, IWindowController> ();

        public WindowManager(IAbstractWindowFactory abstractWindowFactory)
        {
            _abstractWindowFactory = abstractWindowFactory;
            _mainCanvas = Object.FindObjectOfType<Canvas>().transform;

            OpenWindow<IMainWindowController>();
        }
        
        public void OpenWindow<T>() where T : IWindowController
        {
            var exists = _openedWindows.ContainsKey(typeof(T));
            if (exists)
            {
                Debug.Log($"[WindowManager]: Window {nameof(T)} is already opened!");
            }
            else
            {
                var openedWindow = _abstractWindowFactory.GetWindowController<T>(_mainCanvas);
                openedWindow.Open();
                _openedWindows.Add(typeof(T), openedWindow);
            }
        }
        
        public void CloseWindow<T>() where T : IWindowController
        {
            var openedWindow = _openedWindows.ContainsKey(typeof(T));
            if (openedWindow)
            {
                _openedWindows[typeof(T)].Close();
                _openedWindows.Remove(typeof(T));
                _abstractWindowFactory.RemoveWindow<T>();
            }
            else
            {
                Debug.Log($"[WindowManager]: Window {nameof(T)} is already closed!");
            }

        }

        public void Update(float deltaTime)
        {
            foreach (var openedWindow in _openedWindows)
            {
                openedWindow.Value.Update(deltaTime);
            }
        }

        public void CloseAllWindows()
        {
            foreach (var windowController in _openedWindows)
            {
                windowController.Value.Close();
                var windowType = windowController.Key;
                _abstractWindowFactory.RemoveWindow(windowType);
            }
            _openedWindows.Clear();
        }
    }
}