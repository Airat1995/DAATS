using DAATS.Component.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class WaypointEnemy : BasicEnemy, IWaypointEnemy, IVisualElement
    {
        [SerializeField]
        private float _speed;
        public float Speed => _speed;

        public List<IWaypoint> Waypoints { get; set; }

    }
}