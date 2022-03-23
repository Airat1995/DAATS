using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.Initializer.Component;
using DAATS.Initializer.Level;
using DAATS.Initializer.Manager.Resource.Interface;
using DAATS.Initializer.System.Window.Interface;
using DAATS.UserData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DAATS.Initializer.Manager.Resource
{
    public class ResourceManager : IResourceManager, IWindowResourceManager
    {
        private static readonly string _resourceFolder = "";

        private Dictionary<Type, GameObject> _windows = new Dictionary<Type, GameObject>();

        public LevelDescriptor GetLevel(LevelData levelData, Transform spawnPoint)
        {
            string levelObjectName = _resourceFolder + "Level/" + levelData.Name;
            var levelGO = InstantiateObject<LevelDescriptor>(spawnPoint, levelObjectName);

            return levelGO;
        }

        public Player GetPlayerObject(ISpawnPoint spawnPoint)
        {
            string resPlayerName = _resourceFolder + "Player/Player";
            var playerGO = InstantiateObject<Player>(spawnPoint.SpawnTransform, resPlayerName);

            return playerGO;
        }

        public StalkerEnemy GetStalkerEnemyObject(IEnemySpawnPoint spawnPoint)
        {
            string enemyName = _resourceFolder + "Enemy/Stalker/Stalker";
            var enemyGO = InstantiateObject<StalkerEnemy>(spawnPoint.SpawnTransform, enemyName);
            SetEnemyInitialData(spawnPoint, enemyGO);

            return enemyGO;
        }

        public ChaoticEnemy GetChaoticEnemyObject(IWaypointsEnemySpawnPoint spawnPoint)
        {
            string enemyName = _resourceFolder + "Enemy/Chaotic/Chaotic";
            var enemyGO = InstantiateObject<ChaoticEnemy>(spawnPoint.SpawnTransform, enemyName);
            enemyGO.Waypoints = spawnPoint.Waypoints;
            SetEnemyInitialData(spawnPoint, enemyGO);

            return enemyGO;
        }

        public WaypointEnemy GetWaypointEnemyObject(IWaypointsEnemySpawnPoint spawnPoint)
        {
            string enemyName = _resourceFolder + "Enemy/Waypoint/Waypoint";
            var enemyGO = InstantiateObject<WaypointEnemy>(spawnPoint.SpawnTransform, enemyName);
            enemyGO.Waypoints = spawnPoint.Waypoints;
            
            SetEnemyInitialData(spawnPoint, enemyGO);
            return enemyGO;
        }

        private void SetEnemyInitialData(IEnemySpawnPoint spawnPoint, IEnemy enemy)
        {
            spawnPoint.AddAssociatedEnemy(enemy);
            if (spawnPoint.EnabledFromStart)
                enemy.Enable();
            else
                enemy.Disable();
        }

        public void UnloadLevel(LevelDescriptor level)
        {
            Object.Destroy(level.gameObject);
        }

        public void UnloadPlayer(IPlayer player)
        {
            Object.Destroy((Object)player);
        }

        public void UnloadEnemy(IEnemy enemy)
        {
            Object.Destroy((Object)enemy);
        }

        public void UnloadSymbol(ICollectable requiredCollectable)
        {
            Object.Destroy((Object)requiredCollectable);
        }

        public void UnloadExit(IExit exit)
        {
            Object.Destroy((Object)exit);
        }

        public T LoadWindowView<T>(Transform parent) where T : MonoBehaviour, IWindowView
        {
            var windowName = _resourceFolder + "Window/" + typeof(T).Name;
            var window = InstantiateObject<T>(parent, windowName);
            window.gameObject.SetActive(false);
            window.transform.SetAsFirstSibling();
            _windows.Add(typeof(T), window.gameObject);
            return window;
        }

        public void UnloadWindowView<T>()  where T : MonoBehaviour, IWindowView
        {
            if (_windows.ContainsKey(typeof(T)))
            {
                Object.Destroy(_windows[typeof(T)]);
                _windows.Remove(typeof(T));
            }
            else
                Debug.LogError($"Unable to close window: {typeof(T)}");
        }

        private static T InstantiateObject<T>(Transform parent, string windowName) where T : MonoBehaviour
        {
            var res = Resources.Load<T>(windowName);
            var spawnedObject = Object.Instantiate(res, parent);
            return spawnedObject;
        }
    }
}