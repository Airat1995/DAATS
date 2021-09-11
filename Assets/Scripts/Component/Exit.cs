using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    [RequireComponent(typeof(Collider))]
    public class Exit : MonoBehaviour, IExit
    {
        private Action<Collision> _onCollisionEnter = collision => { };

        public void SubscribeOnCollide(Action<Collision> collisionAction)
        {
            _onCollisionEnter += collisionAction;
        }

        public void UnsubcribeOnCollide(Action<Collision> collisionAction)
        {
            _onCollisionEnter -= collisionAction;
        }

        void OnCollisionEnter(Collision collision)
        {
            _onCollisionEnter?.Invoke(collision);
        }
    }
}