using DAATS.Initializer.System.Window.GameWindow.Interface;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowController : IGameWindowController
    {
        private readonly IGameWindowPresenter _presenter;

        public GameWindowController(IGameWindowPresenter presenter)
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