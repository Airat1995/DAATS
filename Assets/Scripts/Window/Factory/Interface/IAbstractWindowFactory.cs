using System;
using DAATS.Initializer.System.Window.Interface;
using UnityEngine;

namespace DAATS.Initializer.System.Window.Fabric.Interface
{
    public interface IAbstractWindowFactory
    {
        IWindowController GetWindowController<T>(Transform parent) where T : IWindowController;
        void RemoveWindow<T>() where T : IWindowController;
        void RemoveWindow(Type type);
    }
}