using DAATS.Component.Interface;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class StalkerEnemy : MonoBehaviour, IStalkerEnemy, IVisualElement
    {
        [SerializeField]
        private int _damage;
        public int Damage => _damage;

        [SerializeField]
        private NavMeshAgent _meshAgent;
        public NavMeshAgent Agent => _meshAgent;
        
        public Transform Transform => transform;

        [SerializeField]
        private float _speed;
        public float Speed => _speed;

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