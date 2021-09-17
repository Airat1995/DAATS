using DAATS.Component.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class LevelFinishSystem : ILevelFinishSystem
    {
        private readonly IExit _exitComponent;
        private readonly IPlayer _player;
        private readonly IGameWorld _currentWorld;
        private readonly ICollectionSystem _collectionSystem;

        public LevelFinishSystem(IExit exitComponent, IPlayer player, IGameWorld currentWorld, ICollectionSystem collectionSystem)
        {
            _exitComponent = exitComponent;
            _player = player;
            _currentWorld = currentWorld;
            _collectionSystem = collectionSystem;

            _exitComponent.SubscribeOnCollide(CheckCollision);
        }

        private void CheckCollision(Collider collider)
        {
            if (_player.IsSameGameObject(collider.gameObject) && _collectionSystem.AllCollected)
                _currentWorld.FinishLevel();
        }
    }
}