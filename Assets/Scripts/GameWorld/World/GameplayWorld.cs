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
using DialogueEditor;
using UnityEngine;
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
        private ICameraComponent _camera => _container.Resolve<ICameraComponent>();
        private ConversationManager _conversationManager => _container.Resolve<ConversationManager>();

        public GameplayWorld(DiContainer container)
        {
            _container = container;
            CreateLastLevel();
        }

        public void AddCallableSystem(ICallableSystem addSystem)
        {
            bool isAlreadyHaveBind = _container.HasBinding(addSystem.GetType());
            if(!isAlreadyHaveBind)    
                _container.Bind(addSystem.GetType()).FromInstance(addSystem);
            _callableSystems.Add(addSystem);
        }

        public void AddUpdatableSystem(IUpdatableSystem addSystem)
        {
            bool isAlreadyHaveBind = _container.HasBinding(addSystem.GetType());
            if(!isAlreadyHaveBind)            
                _container.Bind(addSystem.GetType()).FromInstance(addSystem);
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
			await _setUserProgress.WriteCompletedLevel(_currentLevel, new LevelProgress() { LeftTime = 0, StarCount = 0 });
            if(_levelCreator.Conversation != null)
            {
                _conversationManager.StartConversation(_levelCreator.Conversation);
                ConversationManager.OnConversationEnded += OnConversationEnded;
            }
            else
            {
                LoadNextLevel();
            }
		}

        private void OnConversationEnded()
        {
            ConversationManager.OnConversationEnded -= OnConversationEnded;
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
			ClearWorld();
			CreateLastLevel();
        }

		public void LoseLevel()
		{
			ClearWorld();
			CreateLastLevel();
		}

		
		private void ClearWorld()
		{
			_windowManager.CloseAllWindows();
			_levelCreator.DestroyLevel();
			_updatableSystems.Clear();
			_callableSystems.Clear();
            foreach (var updatableSystem in _updatableSystems)
            {
                _container.Unbind(updatableSystem.GetType());
            }
            foreach (var callableSystem in _callableSystems)
            {
                _container.Unbind(callableSystem.GetType());
            }
		}
        
        private void CreateLastLevel()
        {
            var lastLevel = _levelCollection.GetNextLevelData(_userProgressData.LastBeatenLevel());
            _currentLevel = lastLevel;
            CreateLevel(lastLevel);            
            AddUpdatableSystem(_windowManager);
        }

        private void CreateLevel(LevelData createLevel)
        {
            _levelCreator = new LevelCreator(createLevel,
                null, _resourceManager);
            _levelCreator.SpawnLevel();
            _container.Rebind<IPlayer>().FromInstance(_levelCreator.Player);

            var inputSystem = new InputSystem();
           AddUpdatableSystem(inputSystem);           

            var playerMove = new PlayerMovementSystem(inputSystem, _levelCreator.Player);
           AddUpdatableSystem(playerMove);

            CreateWaypointEnemies();
            CreateChaoticEnemies();
            CreateStalkerEnemies();

            var portalSystem = new PortalSystem(_levelCreator.Player, playerMove, _levelCreator.Portals);
             AddCallableSystem(portalSystem);
            
            var cameraFollow = new CameraFollowSystem(_camera, _levelCreator.Player, _levelCreator.CameraOffset);
            AddUpdatableSystem(cameraFollow);

            var requiredCollectionSystem =
                new RequiredCollectionSystem(new Queue<IRequiredCollectable>(_levelCreator.RequiredCollectables),
                    _levelCreator.Player);
            var levelFinishSystem = new LevelFinishSystem(_levelCreator.Exit, _levelCreator.Player, this, requiredCollectionSystem);
            var slidingSystem = new SlidingTileSystem(_levelCreator.Player, playerMove,
                _levelCreator.SlidingTiles,
                _levelCreator.Walls);

			var allEnemies = new List<IEnemy>();
			allEnemies.AddRange(_levelCreator.WaypointEnemies);
			allEnemies.AddRange(_levelCreator.ChaoticEnemies);
			allEnemies.AddRange(_levelCreator.StalkerEnemies);
			
			var playerHealthSystem = new PlayerHealthSystem(_levelCreator.Player, this);
			AddCallableSystem(playerHealthSystem);
			
			var enemyHitSystem = new EnemyHitSystem(allEnemies, playerMove, _levelCreator.Player, playerHealthSystem);
			AddCallableSystem(enemyHitSystem);
            AddCallableSystem(enemyHitSystem);

            AddCallableSystem(requiredCollectionSystem);
            AddCallableSystem(levelFinishSystem);
            AddCallableSystem(slidingSystem);

            _windowManager.OpenWindow<IGameWindowController>();
            if(_levelCreator.HiddenVision)
            {
                _windowManager.OpenWindow<IFogFollowWindowController>();
            }
        }

        private void CreateStalkerEnemies()
        {
            foreach (var stalkerEnemy in _levelCreator.StalkerEnemies)
            {
                AddUpdatableSystem(new StalkerEnemyMovementSystem(stalkerEnemy, _levelCreator.Player));
            }
        }

        private void CreateChaoticEnemies()
        {
            foreach (var chaoticEnemy in _levelCreator.ChaoticEnemies)
            {
                AddUpdatableSystem(new ChaoticEnemyMovementSystem(chaoticEnemy));
            }
        }

        private void CreateWaypointEnemies()
        {
            foreach (var waypointEnemy in _levelCreator.WaypointEnemies)
            {
                AddUpdatableSystem(new WaypointMovementSystem(waypointEnemy));
            }
        }
    }
}