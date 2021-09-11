using DAATS.Initializer.System.Window.Interface;

namespace DAATS.Initializer.System.Window.GameWindow.Interface
{
    public interface IGameWindowView : IWindowView
    {
        void SetEventReceiver(IGameWindowEventReceiver eventReceiver);
    }
}