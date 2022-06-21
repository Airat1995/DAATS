using System.Collections.Generic;

namespace DAATS.Component.Interface
{
    public interface IWaypointEnemy : IMovableEnemy
    {
        List<IWaypoint> Waypoints { get; }
    }
}