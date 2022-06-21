using DAATS.Initializer.System.Window.Interface;
using UnityEngine;

namespace DAATS.Window.FogFollowWindow.Interface
{
    interface IFogFollowWindowView : IWindowView
    {
        void SetEventReceiver(IFogWindowEventReceiver eventReceiver);
    }
}
