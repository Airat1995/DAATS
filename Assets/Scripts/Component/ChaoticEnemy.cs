using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class ChaoticEnemy : MonoBehaviour, IChaoticEnemy, IVisualElement
    {
        [SerializeField]
        private float _speed;
        public float Speed => _speed;

        [SerializeField]
        private uint _damage;
        public uint Damage => _damage;

		[SerializeField]
		private float _hitBounceDistance;
		public float HitBounceDistance => _hitBounceDistance;

        [SerializeField]
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;

        [SerializeField]
        private Renderer _renderer;
        public Material Material => _renderer.material;

        public Transform Transform => transform;
        public List<IWaypoint> Waypoints { get; set; }

		

		private Action<Collider, IEnemy> _onCollide = (collider, enemy) => { };

        public void SubscribeOnCollide(Action<Collider, IEnemy> onCollide)
        {
            _onCollide += onCollide;
        }

        public void UnsubscribeOnCollide(Action<Collider, IEnemy> onCollide)
        {
            _onCollide -= onCollide;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _onCollide.Invoke(collider, this);
        }

    }
}