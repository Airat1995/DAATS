using DAATS.Component.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class WaypointEnemy : BasicEnemy, IWaypointEnemy, IVisualElement
    {
        [SerializeField]
        private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public List<IWaypoint> Waypoints { get; set; }

    }
}