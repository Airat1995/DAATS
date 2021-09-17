using DAATS.Component.Interface;
using DAATS.System.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace DAATS.System
{
    class PortalSystem : IPortalSystem
    {
        private readonly ITeleportable _teleportable;
        private readonly IControllerMovementSystem _movementSystem;

        public PortalSystem(ITeleportable teleportable, IControllerMovementSystem movementSystem, List<IPortal> portals)
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
