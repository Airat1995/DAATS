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
        private int _damage;
        public int Damage => _damage;

        public List<IWaypoint> Waypoints { get; set; }

        public Transform Transform => transform;

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

        private Action<Collision> _onCollide = (collider) => { };

        public void SubscribeOnCollide(Action<Collision> onCollide)
        {
            _onCollide += onCollide;
        }

        public void UnsubscribeOnCollide(Action<Collision> onCollide)
        {
            _onCollide -= onCollide;
        }
        private void OnCollisionEnter(Collision collision)
        {
            _onCollide.Invoke(collision);
        }
    }
}