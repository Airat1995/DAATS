using System.Collections.Generic;
using System.Linq;
using DAATS.Component.Interface;
using DAATS.Initializer.Level.Creator.Interface;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.UserData;
using UnityEngine;

namespace DAATS.Initializer.Level.Creator
{
    public class LevelCreator : ILevelCreator
    {
        private readonly LevelData _levelData;
        private readonly Transform _levelSpawnTransform;
        private readonly IResourceManager _resourceManager;        
        private LevelDescriptor _spawnedLevelDescriptor;

        public IPlayer Player { get; private set; }

        public List<IRequiredCollectable> RequiredCollectables { get; private set; }
        public List<IPortal> Portals {get; private set; }
        
        public IExit Exit => _spawnedLevelDescriptor.Exit;
        public List<IStalkerEnemy> StalkerEnemies { get; private set; }
        public List<IChaoticEnemy> ChaoticEnemies { get; private set; }
        public List<IWaypointEnemy> WaypointEnemies { get; private set; }
        public List<ISlidingTile> SlidingTiles { get; private set; }
        public List<IWall> Walls { get; private set; }
        public bool HiddenVision{get; private set; }        

        public LevelCreator(LevelData levelData, Transform levelSpawnTransform, IResourceManager resourceManager)
        {
            _levelData = levelData;
            _levelSpawnTransform = levelSpawnTransform;
            _resourceManager = resourceManager;
        }

        public void SpawnLevel()
        {
            _spawnedLevelDescriptor = _resourceManager.GetLevel(_levelData, _levelSpawnTransform);
            Player = _resourceManager.GetPlayerObject(_spawnedLevelDescriptor.PlayerSpawnTransform);
            RequiredCollectables = new List<IRequiredCollectable>(_spawnedLevelDescriptor.RequiredCollectables);

            StalkerEnemies = FillStalkerEnemies();
            ChaoticEnemies = FillChaoticEnemies();
            WaypointEnemies = FillWaypointEnemies();
            HiddenVision = _spawnedLevelDescriptor.HiddenVision;
            Portals = _spawnedLevelDescriptor.Portals;
            SlidingTiles = _spawnedLevelDescriptor.SlidingTiles;
            Walls = _spawnedLevelDescriptor.Walls;
        }

        public void DestroyLevel()
        {
            foreach (var requiredCollectable in RequiredCollectables)
            {
                _resourceManager.UnloadSymbol(requiredCollectable);
            }

            _resourceManager.UnloadExit(Exit);
            _resourceManager.UnloadPlayer(Player);
            _resourceManager.UnloadLevel(_spawnedLevelDescriptor);
        }

        private List<IWaypointEnemy> FillWaypointEnemies()
        {
            var waypointEnemies = new List<IWaypointEnemy>(_spawnedLevelDescriptor.WaypointsSpawnPoints.Count);
            waypointEnemies.AddRange(_spawnedLevelDescriptor.WaypointsSpawnPoints.Select(stalkerEnemySpawnTransform => _resourceManager.GetWaypointEnemyObject(stalkerEnemySpawnTransform)));

            return waypointEnemies;
        }

        private List<IChaoticEnemy> FillChaoticEnemies()
        {
            var chaoticEnemies = new List<IChaoticEnemy>(_spawnedLevelDescriptor.ChaoticEnemySpawnPoints.Count);
            chaoticEnemies.AddRange(_spawnedLevelDescriptor.ChaoticEnemySpawnPoints.Select(stalkerEnemySpawnTransform => _resourceManager.GetChaoticEnemyObject(stalkerEnemySpawnTransform)));

            return chaoticEnemies;
        }

        private List<IStalkerEnemy> FillStalkerEnemies()
        {
            var stalkerEnemies = new List<IStalkerEnemy>(_spawnedLevelDescriptor.StalkerEnemySpawnTransforms.Count);
            stalkerEnemies.AddRange(_spawnedLevelDescriptor.StalkerEnemySpawnTransforms.Select(stalkerEnemySpawnTransform => _resourceManager.GetStalkerEnemyObject(stalkerEnemySpawnTransform)));

            return stalkerEnemies;
        }
    }
}