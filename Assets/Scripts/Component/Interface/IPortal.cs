using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPortal : IComponent
    {
        Transform Transform { get; }

        IPortal ConnectedPortal { get; }

        void Teleport(ITeleportable teleportableElement);

        void SubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter);
        void UnsubscribeOnTeleportEnter(Action<Collider, IPortal> onTeleoprtEnter);
    }
}
