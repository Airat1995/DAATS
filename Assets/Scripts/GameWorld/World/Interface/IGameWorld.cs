using DAATS.System.Interface;

namespace DAATS.Initializer.GameWorld.World.Interface
{
    public interface IGameWorld
    {
        void AddCallableSystem(ICallableSystem addSystem);
        void AddUpdatableSystem(IUpdatableSystem addSystem);
        
        void Update(float deltaTime);

        void FinishLevel();

		void LoseLevel();
    }
}