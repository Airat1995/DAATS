using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPlayer : IComponent, ITeleportable
    {
        float Speed { get; }
        uint MaxHealth { get; }

        bool IsSameGameObject(GameObject other);
    }
}