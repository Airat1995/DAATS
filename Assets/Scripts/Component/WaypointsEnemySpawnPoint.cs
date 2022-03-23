using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class WaypointsEnemySpawnPoint : MonoBehaviour, IWaypointsEnemySpawnPoint
    {
        [SerializeField]
        private List<Waypoint> _waypoints;
        public List<IWaypoint> Waypoints => new List<IWaypoint>(_waypoints);

        [SerializeField]
        private bool _enabledFromStart = true;
        public bool EnabledFromStart => _enabledFromStart;

        public Transform SpawnTransform => transform;

        private IEnemy _associatedEnemy;
        public IEnemy AssociatedEnemy => _associatedEnemy;

        /// <summary> Add associated enemy to the spawn point. Can be called only once. </summary>
        public void AddAssociatedEnemy(IEnemy associatedEnemy) 
        {
            if(_associatedEnemy != null) return;
            _associatedEnemy = associatedEnemy;
        }
    }
}