using DAATS.Component.Interface;
using DAATS.System.Interface;
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

        private bool _teleportWasUsed = false;
        
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
            _teleportWasUsed = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _teleportWasUsed = false;
        }

        private void OnDestroy()
        {
            _onPortalEnterActions = null;
        }

        public void Teleport(ITeleportable teleportableElement, IMovementSytem movementSystem)
        {
            if(_teleportWasUsed)
                return;
            movementSystem.SetPosition( _connectedPortal.Transform.position);
            _connectedPortal.SetTeleported();            
        }

        private void SetTeleported()
        {
            _teleportWasUsed = true;
        }
    }
}