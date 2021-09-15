using DAATS.System.Interface;
using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPortal : IComponent
    {
        Transform Transform { get; }

        IPortal ConnectedPortal { get; }

        void Teleport(ITeleportable teleportableElement, IMovementSytem _movementSystem);

        void SubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter);
        void UnsubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter);
    }
}
