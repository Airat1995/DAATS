using DAATS.Initializer.System.Window.Interface;
using UnityEngine;

namespace DAATS.Initializer.Manager.Resource.Interface
{
    public interface IWindowResourceManager
    {
        T LoadWindowView<T>(Transform parent) where T : MonoBehaviour, IWindowView;

        void UnloadWindowView<T>() where T : MonoBehaviour, IWindowView;
    }
}