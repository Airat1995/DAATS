using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class WaypointsSpawnPoint : MonoBehaviour, IWaypointsSpawnPoint
    {
        [SerializeField]
        private List<Waypoint> _waypoints;
        public List<IWaypoint> Waypoints => new List<IWaypoint>(_waypoints);

        public Transform SpawnTransform => transform;
    }
}