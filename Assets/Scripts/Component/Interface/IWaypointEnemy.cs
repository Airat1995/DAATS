using System.Collections.Generic;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IWaypointEnemy : IMovableEnemy
    {
        List<IWaypoint> Waypoints { get; }
    }
}