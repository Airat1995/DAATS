using System.Collections.Generic;

namespace DAATS.Component.Interface
{
    public interface IWaypointEnemy : IMovableEnemy
    {
        public List<IWaypoint> Waypoints { get; }
    }
}