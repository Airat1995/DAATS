using DAATS.Component.Interface;
using DAATS.Initializer.Component;
using DAATS.Initializer.Level;
using DAATS.UserData;
using UnityEngine;

namespace DAATS.Initializer.Manager.Resource.Interface
{
    public interface IResourceManager
    {
        LevelDescriptor GetLevel(LevelData levelData, Transform spawnPoint);
        void UnloadLevel(LevelDescriptor level);
        
        Player GetPlayerObject(ISpawnPoint spawnPoint);
        StalkerEnemy GetStalkerEnemyObject(ISpawnPoint spawnPoint);
        ChaoticEnemy GetChaoticEnemyObject(IWaypointsSpawnPoint spawnPoint);
        WaypointEnemy GetWaypointEnemyObject(IWaypointsSpawnPoint spawnPoint);

        void UnloadPlayer(IPlayer player);
        void UnloadEnemy(IEnemy enemy);
        void UnloadSymbol(ICollectable requiredCollectable);
        void UnloadExit(IExit exit);
    }
}