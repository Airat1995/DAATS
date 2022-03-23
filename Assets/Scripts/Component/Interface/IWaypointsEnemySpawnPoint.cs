using System.Collections.Generic;

namespace DAATS.Component.Interface
{
    public interface IWaypointsEnemySpawnPoint : IEnemySpawnPoint
    {
        List<IWaypoint> Waypoints { get; }
    }
}