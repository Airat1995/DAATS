using System.Collections.Generic;
using DAATS.Component.Interface;

namespace DAATS.Initializer.Level.Creator.Interface
{
    public interface ILevelCreator
    {
        IPlayer Player { get; }
        List<IRequiredCollectable> RequiredCollectables { get; }
        IExit Exit { get; }
        List<IPortal> Portals { get; }

        bool HiddenVision{ get; }

        List<IStalkerEnemy> StalkerEnemies { get; }
        List<IChaoticEnemy> ChaoticEnemies { get; }
        List<IWaypointEnemy> WaypointEnemies { get; }

        List<ISlidingTile> SlidingTiles { get; }
        List<IWall> Walls { get; }

        void SpawnLevel();
        void DestroyLevel();
    }
}