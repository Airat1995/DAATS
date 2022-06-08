using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Component.Interface
{
    public interface IAIPlayer : IComponent, ITeleportable, IWorldTriggerObject
    {
        float Speed { get; set; }
        
        float TimeToRethink { get; }
        
        float ChanceToMiss { get; }
        
        List<Transform> PointsOfInterest { get; }

        NavMeshAgent MovementAgent { get; }
    }
}