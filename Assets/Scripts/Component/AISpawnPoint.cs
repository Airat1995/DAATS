using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class AISpawnPoint : MonoBehaviour, IAISpawnPoint
    {
        public Transform SpawnTransform => transform;

        [SerializeField]
        private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField]
        [Range(0, 1)]
        private float _chanceToMiss;

        public float ChanceToMiss
        {
            get => _chanceToMiss;
            set => _chanceToMiss = value;
        }

        [SerializeField]
        private float _timeToRethink;

        public float TimeToRethink
        {
            get => _timeToRethink;
            set => _timeToRethink = value;
        }

        [SerializeField]
        private List<Transform> _pointsOfInterest;
        public List<Transform> PointsOfInterest => _pointsOfInterest;
    }
}