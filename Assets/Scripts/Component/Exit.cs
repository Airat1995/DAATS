using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    [RequireComponent(typeof(Collider))]
    public class Exit : MonoBehaviour, IExit
    {
        private Action<Collider> _onCollisionEnter = collision => { };

        public Vector3 Position => transform.position;

        public void SubscribeOnCollide(Action<Collider> collisionAction)
        {
            _onCollisionEnter += collisionAction;
        }

        public void UnsubcribeOnCollide(Action<Collider> collisionAction)
        {
            _onCollisionEnter -= collisionAction;
        }

        void OnTriggerEnter(Collider collider)
        {
            _onCollisionEnter?.Invoke(collider);
        }
    }
}