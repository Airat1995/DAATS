using System.Linq;
using DAATS.Component.Interface;
using DAATS.Initializer.Level.Creator.Interface;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.UserData;
using DialogueEditor;
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

        public IRequiredCollectable[] RequiredCollectables { get; private set; }
        public IPortal[] Portals {get; private set; }
        
        public IExit Exit => _spawnedLevelDescriptor.Exit;
        public IStalkerEnemy[] StalkerEnemies { get; private set; }
        public IChaoticEnemy[] ChaoticEnemies { get; private set; }
        public IWaypointEnemy[] WaypointEnemies { get; private set; }
        public ISlidingTile[] SlidingTiles { get; private set; }
        public IWall[] Walls { get; private set; }        
        public IEnemyActivatorTile[] ActivatorTiles {get; private set;}
        public IEnemyDeactivatorTile[] DeactivatorTiles { get; private set; }

        public bool HiddenVision { get; private set; }
        public float CameraOffset { get; private set; }
        public NPCConversation Conversation { get; private set; }

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
            RequiredCollectables = _spawnedLevelDescriptor.RequiredCollectables;

            StalkerEnemies = FillStalkerEnemies();
            ChaoticEnemies = FillChaoticEnemies();
            WaypointEnemies = FillWaypointEnemies();
            HiddenVision = _spawnedLevelDescriptor.HiddenVision;
            Portals = _spawnedLevelDescriptor.Portals;
            SlidingTiles = _spawnedLevelDescriptor.SlidingTiles;
            Walls = _spawnedLevelDescriptor.Walls;
            CameraOffset = _spawnedLevelDescriptor.CameraOffset;
            Conversation = _spawnedLevelDescriptor.Conversation;
            ActivatorTiles = _spawnedLevelDescriptor.ActivatorTiles;
            DeactivatorTiles = _spawnedLevelDescriptor.DeactivatorTiles;
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

        private IWaypointEnemy[] FillWaypointEnemies()
        {
            var waypointEnemies = _spawnedLevelDescriptor.WaypointsSpawnPoints.Select(stalkerEnemySpawnTransform => _resourceManager.GetWaypointEnemyObject(stalkerEnemySpawnTransform)).ToArray();
            return waypointEnemies;
        }

        private IChaoticEnemy[] FillChaoticEnemies()
        {
            var chaoticEnemies = _spawnedLevelDescriptor.ChaoticEnemySpawnPoints.Select(stalkerEnemySpawnTransform => _resourceManager.GetChaoticEnemyObject(stalkerEnemySpawnTransform)).ToArray();
            return chaoticEnemies;
        }

        private IStalkerEnemy[] FillStalkerEnemies()
        {
            var stalkerEnemies = _spawnedLevelDescriptor.StalkerEnemySpawnPoints.Select(stalkerEnemySpawnTransform => _resourceManager.GetStalkerEnemyObject(stalkerEnemySpawnTransform)).ToArray();
            return stalkerEnemies;
        }
    }
}