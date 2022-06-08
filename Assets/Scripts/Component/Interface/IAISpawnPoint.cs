using System.Collections.Generic;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IAISpawnPoint : ISpawnPoint
    {
        public float Speed { get; }
        
        public float ChanceToMiss { get; }
        
        public float TimeToRethink { get; }
        
        public List<Transform> PointsOfInterest { get; }
    }
}