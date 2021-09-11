using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.Initializer.Level.Creator;
using DAATS.Initializer.Level.Interface;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System;
using DAATS.Initializer.System.Window;
using DAATS.Initializer.System.Window.FogFollowWindow.Interface;
using DAATS.Initializer.System.Window.GameWindow.Interface;
using DAATS.System;
using DAATS.System.Interface;
using DAATS.UserData;
using DAATS.UserData.Interface;
using Zenject;

namespace DAATS.Initializer.GameWorld.World
{
    public class GameplayWorld : IGameWorld
    {
        private readonly DiContainer _container;
        private List<ICallableSystem> _callableSystems = new List<ICallableSystem>();
        private List<IUpdatableSystem> _updatableSystems = new List<IUpdatableSystem>();
        private LevelCreator _levelCreator;
        private LevelData _currentLevel;

        private WindowManager _windowManager => _container.Resolve<WindowManager>();
        private IGetUserProgressData _userProgressData => _container.Resolve<IGetUserProgressData>();
        private ISetUserProgressData _setUserProgress => _container.Resolve<ISetUserProgressData>();
        private IResourceManager _resourceManager => _container.Resolve<IResourceManager>();
        private ILevelCollection _levelCollection => _container.Resolve<ILevelCollection>();

        public GameplayWorld(DiContainer container)
        {
            _container = container;
            CreateLastLevel();
        }

        public void AddCallableSystem(ICallableSystem addSystem)
        {
            _callableSystems.Add(addSystem);
        }

        public void AddUpdatableSystem(IUpdatableSystem addSystem)
        {
            _updatableSystems.Add(addSystem);
        }

        public void Update(float deltaTime)
        {
            foreach (var updatableSystem in _updatableSystems)
            {
                updatableSystem.Update(deltaTime);
            }
        }

        public async void FinishLevel()
        {
            await _setUserProgress.WriteCompletedLevel(_currentLevel, new LevelProgress(){LeftTime = 0, StarCount = 0});
            _windowManager.CloseAllWindows();
            _levelCreator.DestroyLevel();
            _updatableSystems.Clear();
            _callableSystems.Clear();
            CreateLastLevel();
        }
        
        private void CreateLastLevel()
        {
            var lastLevel = _levelCollection.GetNextLevelData(_userProgressData.LastBeatenLevel());
            _currentLevel = lastLevel;
            CreateLevel(lastLevel);
            _updatableSystems.Add(_windowManager);
        }

        private void CreateLevel(LevelData createLevel)
        {
            _windowManager.OpenWindow<IGameWindowController>();

            _levelCreator = new LevelCreator(createLevel,
                null, _resourceManager);
            _levelCreator.SpawnLevel();
            _container.Rebind<IPlayer>().FromInstance(_levelCreator.Player);

            var inputSystem = new InputSystem();
            var movementSystem = new MovementSystem(_levelCreator.Player.Transform);
            var playerMove = new PlayerMovementSystem(inputSystem, movementSystem, _levelCreator.Player);

            CreateWaypointEnemies();
            CreateChaoticEnemies();
            CreateStalkerEnemies();

             _callableSystems.Add(new PortalSystem(_levelCreator.Player, _levelCreator.Portals));

            _updatableSystems.Add(inputSystem);
            _updatableSystems.Add(playerMove);
            _updatableSystems.Add(movementSystem);

            var requiredCollectionSystem =
                new RequiredCollectionSystem(new Queue<IRequiredCollectable>(_levelCreator.RequiredCollectables),
                    _levelCreator.Player);
            var levelFinishSystem = new LevelFinishSystem(_levelCreator.Exit, _levelCreator.Player, this, requiredCollectionSystem);
            var slidingSystem = new SlidingTileSystem(_levelCreator.Player, movementSystem,
                _levelCreator.SlidingTiles,
                _levelCreator.Walls);

            _callableSystems.Add(requiredCollectionSystem);
            _callableSystems.Add(levelFinishSystem);
            _callableSystems.Add(slidingSystem);

            if(_levelCreator.HiddenVision)
            {
                _windowManager.OpenWindow<IFogFollowWindowController>();
            }
            
        }

        private void CreateStalkerEnemies()
        {
            foreach (var stalkerEnemy in _levelCreator.StalkerEnemies)
            {
                _updatableSystems.Add(new StalkerMovementSystem(stalkerEnemy, _levelCreator.Player));
            }
        }

        private void CreateChaoticEnemies()
        {
            foreach (var chaoticEnemy in _levelCreator.ChaoticEnemies)
            {
                _updatableSystems.Add(new ChaoticMovementSystem(chaoticEnemy));
            }
        }

        private void CreateWaypointEnemies()
        {
            foreach (var waypointEnemy in _levelCreator.WaypointEnemies)
            {
                var enemyMoveSystem = new MovementSystem(waypointEnemy.Transform);
                _updatableSystems.Add(new WaypointMovementSystem(enemyMoveSystem, waypointEnemy));
                _updatableSystems.Add(enemyMoveSystem);
            }
        }
    }
}