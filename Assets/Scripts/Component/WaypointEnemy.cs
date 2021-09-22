using DAATS.Component.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class WaypointEnemy : MonoBehaviour, IWaypointEnemy, IVisualElement
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

        public List<IWaypoint> Waypoints { get; set; }

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