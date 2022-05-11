using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Component.Interface
{
    public interface IAIPlayer : IComponent, ITeleportable
    {
        float Speed { get; }

        NavMeshAgent MovementAgent { get; }
        
        bool IsSameGameObject(GameObject other);
    }
}