using UnityEngine.AI;

namespace DAATS.Component.Interface
{
    public interface IStalkerEnemy : IMovableEnemy
    {
        NavMeshAgent Agent { get; }
    }
}