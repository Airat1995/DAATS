using DAATS.Initializer.System.Window.MainWindow.Interface;

namespace DAATS.Initializer.System.Window.MainWindow
{
    public class MainWindowController : IMainWindowController
    {
        private readonly IMainWindowPresenter _windowPresenter;

        public MainWindowController(IMainWindowPresenter windowPresenter)
        {
            _windowPresenter = windowPresenter;
        }

        public void Open()
        {
            _windowPresenter.Open();
        }

        public void Close()
        {
            _windowPresenter.Close();
        }

        public void Update(float deltaTime)
        {
            _windowPresenter.Update(deltaTime);
        }
    }
}