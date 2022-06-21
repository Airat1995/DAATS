using System.Collections.Generic;

namespace DAATS.Component.Interface
{
    public interface IWaypointsEnemySpawnPoint : IMovableEnemySpawnPoint
    {
        List<IWaypoint> Waypoints { get; }
    }
}