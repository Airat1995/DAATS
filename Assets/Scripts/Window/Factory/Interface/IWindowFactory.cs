using DAATS.Initializer.System.Window.Interface;
using UnityEngine;

namespace DAATS.Initializer.System.Window.Fabric.Interface
{
    public interface IWindowFactory<out T> where T : IWindowController
    {
        T GetWindowController(Transform parent);
        void UnloadWindow();
    }
}