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
        IStalkerEnemy GetStalkerEnemyObject(IMovableEnemySpawnPoint spawnPoint);
        IChaoticEnemy GetChaoticEnemyObject(IWaypointsEnemySpawnPoint spawnPoint);
        IWaypointEnemy GetWaypointEnemyObject(IWaypointsEnemySpawnPoint spawnPoint);
        IAIPlayer GetAIPlayer(IAISpawnPoint spawnPoint);

        void UnloadPlayer(IPlayer player);
        void UnloadEnemy(IEnemy enemy);
        void UnloadAIplayer(IAIPlayer aiPlayer);
        void UnloadSymbol(ICollectable requiredCollectable);
        void UnloadExit(IExit exit);
    }
}