using DAATS.Component.Interface;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class StalkerEnemy : MonoBehaviour, IStalkerEnemy, IVisualElement
    {
        [SerializeField]
        private uint _damage;
        public uint Damage => _damage;

		[SerializeField]
        private float _speed;
        public float Speed => _speed;

		[SerializeField]
		private float _hitBounceDistance;
		public float HitBounceDistance => _hitBounceDistance;

        [SerializeField]
        private NavMeshAgent _meshAgent;
        public NavMeshAgent Agent => _meshAgent;
        
        public Transform Transform => transform;

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

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