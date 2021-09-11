using DAATS.Initializer.System.Window.FogFollowWindow.Interface;
using DAATS.Window.FogFollowWindow.Interface;

namespace DAATS.Window.FogFollowWindow
{
    class FogFollowWindowController : IFogFollowWindowController
    {
        private readonly IFogFollowWindowPresenter _presenter;

        public FogFollowWindowController(IFogFollowWindowPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Close()
        {
            _presenter.Close();
        }

        public void Open()
        {
            _presenter.Open();
        }

        public void Update(float deltaTime)
        {
            _presenter.Update(deltaTime);
        }
    }
}
