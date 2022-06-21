using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPlayer : IComponent, ITeleportable, IWorldTriggerObject
    {
        float Speed { get; }
        uint MaxHealth { get; }

        CharacterController CharacterController { get; }
    }
}