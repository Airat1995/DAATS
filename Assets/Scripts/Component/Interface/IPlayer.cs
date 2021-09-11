using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IPlayer : IComponent, ITeleportable
    {
        public float Speed { get; }
        public uint MaxHealth { get; }

        bool IsSameGameObject(GameObject other);
    }
}