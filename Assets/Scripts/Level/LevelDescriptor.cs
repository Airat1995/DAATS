using System.Linq;
using DAATS.Component;
using DAATS.Component.Interface;
using DAATS.Initializer.Component;
using DAATS.UserData;
using UnityEngine;
using DialogueEditor;


#if UNITY_EDITOR
using UnityEditor;
#if UNITY_2021_1_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
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
        private NPCConversation _conversation;
        public NPCConversation Conversation => _conversation;

        [SerializeField]
        private LevelData _levelInfo;
        public LevelData LevelInfo => _levelInfo;

        [SerializeField]
        private LevelType _levelType;
        public LevelType LevelType => _levelType;

        [SerializeField]
        private bool _hiddenVision;
        public bool HiddenVision => _hiddenVision;
        
        [SerializeField]
        private float _cameraOffset;
        public float CameraOffset => _cameraOffset;

        [SerializeField]
        private SimpleSpawnPoint _playerSpawnPoint;
        public ISpawnPoint PlayerSpawnTransform => _playerSpawnPoint;

        [SerializeField]
        private WaypointsEnemySpawnPoint[] _waypointEnemySpawnTransforms;
        public IWaypointsEnemySpawnPoint[] WaypointsSpawnPoints => _waypointEnemySpawnTransforms;

        [SerializeField]
        private WaypointsEnemySpawnPoint[] _chaoticEnemySpawnPoints;
        public IWaypointsEnemySpawnPoint[] ChaoticEnemySpawnPoints => _chaoticEnemySpawnPoints;

        [SerializeField]
        private MovableEnemySpawnPoint[] _stalkerEnemySpawnPoints;
        public IMovableEnemySpawnPoint[] StalkerEnemySpawnPoints => _stalkerEnemySpawnPoints;

        [SerializeField]
        private AISpawnPoint _aiSpawnPoint;
        public IAISpawnPoint AISpawnPoint => _aiSpawnPoint;

        [SerializeField]
        private Exit _exit;
        public IExit Exit => _exit;
        
        [SerializeField]
        private Symbol[] _requiredCollectables;
        public IRequiredCollectable[] RequiredCollectables => _requiredCollectables;
        
        [SerializeField]
        private Portal[] _portal;
        public IPortal[] Portals => _portal;

        [SerializeField]
        private Wall[] _walls;
        public IWall[] Walls => _walls;

        [SerializeField]
        private SlidingTile[] _slidingTiles;
        public ISlidingTile[] SlidingTiles => _slidingTiles;

        [SerializeField]
        private EnemyActivatorTile[] _activatorTiles;
        public IEnemyActivatorTile[] ActivatorTiles => _activatorTiles;

        [SerializeField]
        private EnemyDeactivatorTile[] _deactivatorTiles;
        public IEnemyDeactivatorTile[] DeactivatorTiles => _deactivatorTiles;

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
            _portal = prefabStage.FindComponentsOfType<Portal>();
            _walls = prefabStage.FindComponentsOfType<Wall>();
            _slidingTiles = prefabStage.FindComponentsOfType<SlidingTile>();

            _playerSpawnPoint = prefabStage.FindComponentsOfType<SimpleSpawnPoint>()
                .FirstOrDefault(spawnPoint => spawnPoint.tag.Equals(PlayerSpawnPointTag));
            _waypointEnemySpawnTransforms = prefabStage.FindComponentsOfType<WaypointsEnemySpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(WaypointEnemySpawnPointTag)).ToArray();
            _chaoticEnemySpawnPoints = prefabStage.FindComponentsOfType<WaypointsEnemySpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(ChaoticEnemySpawnPointTag)).ToArray();
            _stalkerEnemySpawnPoints = prefabStage.FindComponentsOfType<MovableEnemySpawnPoint>()
                .Where(spawnPoint => spawnPoint.tag.Equals(StalkerEnemySpawnPointTag)).ToArray();
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