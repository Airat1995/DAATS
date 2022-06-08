using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class ChaoticEnemy : BasicEnemy, IChaoticEnemy, IVisualElement
    {
        [SerializeField]
        private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField]
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;

        public List<IWaypoint> Waypoints { get; set; }

    }
}