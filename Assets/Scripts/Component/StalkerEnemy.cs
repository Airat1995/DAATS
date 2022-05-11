using DAATS.Component.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class StalkerEnemy : BasicEnemy, IStalkerEnemy
    {

		[SerializeField]
        private float _speed;
        public float Speed => _speed;


        [SerializeField]
        private NavMeshAgent _meshAgent;
        public NavMeshAgent Agent => _meshAgent;
        
    }
}