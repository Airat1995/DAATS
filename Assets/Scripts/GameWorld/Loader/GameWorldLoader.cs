using DAATS.Initializer.GameWorld.Loader.Interface;
using DAATS.Initializer.GameWorld.World;
using DAATS.Initializer.GameWorld.World.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.GameWorld.Loader
{
    public class GameWorldLoader : IGameWorldLoader, ITickable
    {
        private readonly DiContainer _container;

        private IGameWorld _currentGameWorld;
        private bool _paused = false;
        private float _timeScaleBeforePause = 0.0f;

        public GameWorldLoader(DiContainer container)
        {
            _container = container;
        }

        public void CreateGameplayWorld()
        {
            _currentGameWorld = new GameplayWorld(_container);
        }

        public void ReloadCurrentWorld()
        {
            CreateGameplayWorld();
        }
        
        public void Pause()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0;
            _paused = true;
        }

        public void Resume()
        {
            Time.timeScale = _timeScaleBeforePause;
            _paused = false;
        }

        public void Tick()
        {
            if (!_paused)
            {
                var deltaTime = Time.deltaTime;
                if (_currentGameWorld != null)
                    _currentGameWorld.Update(deltaTime);
            }
        }
    }
}