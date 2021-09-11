namespace DAATS.Initializer.GameWorld.Loader.Interface
{
    public interface IGameWorldLoader
    {
        void CreateGameplayWorld();
        void ReloadCurrentWorld();
        void Pause();
        void Resume();
    }
}