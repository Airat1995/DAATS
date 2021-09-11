using System.Collections.Generic;
using UnityEngine.AI;

namespace DAATS.Component.Interface
{
    public interface IChaoticEnemy : IMovableEnemy
    {
        public NavMeshAgent Agent { get; }
        public List<IWaypoint> Waypoints { get; }
    }
}