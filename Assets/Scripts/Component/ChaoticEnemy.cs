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
        private int _damage;
        public int Damage => _damage;

        [SerializeField]
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;

        [SerializeField]
        private Renderer _renderer;
        public Material Material => _renderer.material;

        public Transform Transform => transform;
        public List<IWaypoint> Waypoints { get; set; }

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