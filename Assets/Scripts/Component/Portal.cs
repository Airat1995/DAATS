using DAATS.Component.Interface;
using System;
using UnityEngine;

namespace DAATS.Component
{
    [RequireComponent(typeof(Collider))]
    class Portal : MonoBehaviour, IPortal
    {
        [SerializeField]
        private Portal _connectedPortal;
        public IPortal ConnectedPortal => _connectedPortal;

        public Transform Transform => transform;

        private Action<Collider, IPortal> _onPortalEnterActions = (collider, portal) => { };
        
        public void SubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter)
        {
            _onPortalEnterActions += onTeleoprtEnter;
        }

        public void UnsubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter)
        {
            _onPortalEnterActions -= onTeleoprtEnter;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _onPortalEnterActions?.Invoke(collider, this);
        }

        private void OnDestroy()
        {
            _onPortalEnterActions = null;
        }
    }
}