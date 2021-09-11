namespace DAATS.Initializer.System.Window.Interface
{
    public interface IWindowPresenter
    {
        void SetActive(bool enable);

        void Update(float deltaTime);
        
        void Open();
        void Close();
    }
}