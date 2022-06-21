using DAATS.Component.Interface;

namespace DAATS.Initializer.Level.Creator.Interface
{
    public interface ILevelCreator
    {
        IPlayer Player { get; }
        IAIPlayer AIPlayer { get; }
        ICollectable[] Collectables { get; }
        IExit Exit { get; }
        IPortal[] Portals { get; }
        LevelType LevelType { get; }

        bool HiddenVision { get; }

        float CameraOffset { get; }


        IStalkerEnemy[] StalkerEnemies { get; }
        IChaoticEnemy[] ChaoticEnemies { get; }
        IWaypointEnemy[] WaypointEnemies { get; }

        ISlidingTile[] SlidingTiles { get; }

        IEnemyActivatorTile[] ActivatorTiles { get; }
        IEnemyDeactivatorTile[] DeactivatorTiles { get; }

        IWall[] Walls { get; }

        void SpawnLevel();
        void DestroyLevel();
    }
}