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
        public IAIPlayer AIPlayer { get; private set; }
        
        public LevelType LevelType { get; private set; }

        public ICollectable[] Collectables { get; private set; }
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
            Collectables = _spawnedLevelDescriptor.RequiredCollectables;

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
            LevelType = _spawnedLevelDescriptor.LevelType;

            if (_spawnedLevelDescriptor.AISpawnPoint != null)
                AIPlayer = _resourceManager.GetAIPlayer(_spawnedLevelDescriptor.AISpawnPoint);
        }

        public void DestroyLevel()
        {
            foreach (var requiredCollectable in Collectables)
                _resourceManager.UnloadSymbol(requiredCollectable);
            foreach (var stalkerEnemy in StalkerEnemies)
                _resourceManager.UnloadEnemy(stalkerEnemy);
            foreach (var chaoticEnemy in ChaoticEnemies)
                _resourceManager.UnloadEnemy(chaoticEnemy);
            foreach (var waypointEnemy in WaypointEnemies)
                _resourceManager.UnloadEnemy(waypointEnemy);
            
            _resourceManager.UnloadExit(Exit);
            _resourceManager.UnloadPlayer(Player);
            _resourceManager.UnloadLevel(_spawnedLevelDescriptor);
            if (AIPlayer != null)
                _resourceManager.UnloadAIplayer(AIPlayer);
 
            StalkerEnemies = null;
            ChaoticEnemies = null;
            WaypointEnemies = null;
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