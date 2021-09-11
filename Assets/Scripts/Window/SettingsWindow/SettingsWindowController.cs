using DAATS.Initializer.System.Window.SettingsWindow.Interface;

namespace DAATS.Initializer.System.Window.SettingsWindow
{
    public class SettingsWindowController : ISettingsWindowController
    {
        private readonly ISettingsWindowPresenter _presenter;

        public SettingsWindowController(ISettingsWindowPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Open()
        {
            _presenter.Open();
        }

        public void Close()
        {
            _presenter.Close();
        }

        public void Update(float deltaTime)
        {
            _presenter.Update(deltaTime);
        }
    }
}