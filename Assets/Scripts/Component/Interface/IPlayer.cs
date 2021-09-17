using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPlayer : IComponent, ITeleportable
    {
        float Speed { get; }
        uint MaxHealth { get; }

        CharacterController CharacterController { get; }
        bool IsSameGameObject(GameObject other);
    }
}