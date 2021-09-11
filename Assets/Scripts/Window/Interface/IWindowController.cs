namespace DAATS.Initializer.System.Window.Interface
{
    public interface IWindowController
    {
        void Open();
        void Close();

        void Update(float deltaTime);
    }
}