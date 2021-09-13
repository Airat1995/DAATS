using System.Collections.Generic;
using System.Linq;
using DAATS.Component;
using DAATS.Component.Interface;
using DAATS.Initializer.Component;
using DAATS.UserData;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace DAATS.Initializer.Level
{
    [ExecuteInEditMode]
    public class LevelDescriptor : MonoBehaviour
    {
        private static readonly string PlayerSpawnPointTag = "PlayerSpawnPoint";
        private static readonly string WaypointEnemySpawnPointTag = "WaypointEnemySpawnPoint";
        private static readonly string ChaoticEnemySpawnPointTag = "ChaoticEnemySpawnPoint";
        private static readonly string StalkerEnemySpawnPointTag = "StalkerEnemySpawnPoint";

        [SerializeField]
        private LevelData _levelInfo;
        public LevelData LevelInfo => _levelInfo;

        [SerializeField]
        private bool _hiddenVision;
        public bool HiddenVision => _hiddenVision;

        [SerializeField]
        private SimpleSpawnPoint _playerSpawnPoint;
        public ISpawnPoint PlayerSpawnTransform => _playerSpawnPoint;

        [SerializeField]
        private List<WaypointsSpawnPoint> _waypointEnemySpawnTransforms;
        public List<IWaypointsSpawnPoint> WaypointsSpawnPoints => new List<IWaypointsSpawnPoint>(_waypointEnemySpawnTransforms);

        [SerializeField]
        private List<WaypointsSpawnPoint> _chaoticEnemySpawnPoints;
        public List<IWaypointsSpawnPoint> ChaoticEnemySpawnPoints => new List<IWaypointsSpawnPoint>(_chaoticEnemySpawnPoints);

        [SerializeField]
        private List<SimpleSpawnPoint> _stalkerEnemySpawnPoints;
        public List<ISpawnPoint> StalkerEnemySpawnTransforms => new List<ISpawnPoint>(_stalkerEnemySpawnPoints);

        [SerializeField]
        private Exit _exit;
        public IExit Exit => _exit;
        
        [SerializeField]
        private List<Symbol> _requiredCollectables;
        public List<IRequiredCollectable> RequiredCollectables => new List<IRequiredCollectable>(_requiredCollectables);
        
        [SerializeField]
        private List<Portal> _portal;
        public List<IPortal> Portals => new List<IPortal>(_portal);

        [SerializeField]
        private List<Wall> _walls;
        public List<IWall> Walls => new List<IWall>(_walls);

        [SerializeField]
        private List<SlidingTile> _slidingTiles;
        public List<ISlidingTile> SlidingTiles => new List<ISlidingTile>(_slidingTiles);

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_levelInfo.Name))
                gameObject.name = _levelInfo.Name;
        }

#if UNITY_EDITOR
        [ContextMenu("Fill level info")]
        private void FillLevelInfo()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            _exit = prefabStage.FindComponentOfType<Exit>();
            _portal = prefabStage.FindComponentsOfType<Portal>().ToList();
            _walls = prefabStage.FindComponentsOfType<Wall>().ToList();
            _slidingTiles = prefabStage.FindComponentsOfType<SlidingTile>().ToList();

            _playerSpawnPoint = prefabStage.FindComponentsOfType<SimpleSpawnPoint>()
                .FirstOrDefault(spawnPoint => spawnPoint.tag.Equals(PlayerSpawnPointTag));
            _waypointEnemySpawnTransforms = prefabStage.FindComponentsOfType<WaypointsSpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(WaypointEnemySpawnPointTag)).ToList();
            _chaoticEnemySpawnPoints = prefabStage.FindComponentsOfType<WaypointsSpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(ChaoticEnemySpawnPointTag)).ToList();
            _stalkerEnemySpawnPoints = prefabStage.FindComponentsOfType<SimpleSpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(StalkerEnemySpawnPointTag)).ToList();
            EditorUtility.SetDirty(gameObject);        }
        public void FillInfo(string levelName, int levelNum, bool hide)
        {
            _levelInfo.LevelNum = levelNum;
            _levelInfo.Name = levelName;
            _hiddenVision = hide;
        }
#endif

    }
}