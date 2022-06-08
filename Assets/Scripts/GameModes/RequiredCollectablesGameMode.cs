using DAATS.Component.Interface;
using DAATS.Initializer.GameModes.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.GameModes
{
    public class RequiredCollectablesGameMode : IGameMode
    {
        private readonly IGameWorld _gameWorld;

        private bool _allCollected;

        public RequiredCollectablesGameMode(ICollectionSystem collectionSystem, IPlayerHealthSystem playerHealthSystem, IExitLevelSystem exitLevelSystem, IGameWorld gameWorld)
        {
            _gameWorld = gameWorld;

            playerHealthSystem.SubscribeOnHealthChange(OnHealthChanged);
            collectionSystem.SubscribeOnCollectedAll(OnAllCollected);
            exitLevelSystem.SubscribeOnLevelExitReach(OnExitReached);
        }

        private void OnHealthChanged(uint curr, uint max)
        {
            if (curr == 0)
                _gameWorld.LoseLevel();
        }

        private void OnAllCollected(ICollectable collectable)
        {
            _allCollected = true;
        }

        private void OnExitReached()
        {
            if (_allCollected)
                _gameWorld.FinishLevel();
        }
    }
}