using System.Collections.Generic;

namespace DAATS.Component.Interface
{
    public interface IWaypointsSpawnPoint : ISpawnPoint
    {
        List<IWaypoint> Waypoints { get; }
    }
}