using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    class PortalSystem : IPortalSystem
    {
        private readonly ITeleportable _teleportable;
        private readonly IControllerMovementSystem _movementSystem;

        public PortalSystem(ITeleportable teleportable, IControllerMovementSystem movementSystem, IPortal[] portals)
        {
            _teleportable = teleportable;
            _movementSystem = movementSystem;
            foreach (var portal in portals)
            {
                portal.SubscribeOnTeleportEnter(TeleportElement);
            }
        }

        private void TeleportElement(Collider collider, IPortal portal)
        {
            if(portal.ConnectedPortal == null)
                return;

            if(!ReferenceEquals(collider.transform, _teleportable.Transform))
                return;

            portal.Teleport(_teleportable, _movementSystem);
        }
    }
}
