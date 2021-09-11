using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IMovableEnemy : IEnemy
    {
        float Speed { get; }

        Transform Transform { get; }
    }
}
