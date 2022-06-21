using System.Collections.Generic;
using DAATS.Component.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.Component
{
    public class BasicAIPlayer : MonoBehaviour, IAIPlayer, IVisualElement
    {
        public Transform Transform => transform;

        [SerializeField] 
        private float _speed;

        public float Speed
        {
            get =>_speed;
            set => _speed = value;
        }

        public float TimeToRethink { get; set; }

        public float ChanceToMiss { get; set; }

        public List<Transform> PointsOfInterest { get; set; }

        [SerializeField]
        private NavMeshAgent _movementAgent;
        public NavMeshAgent MovementAgent => _movementAgent;
        
        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;
        
        public bool IsSameGameObject(GameObject other)
        {
            return other == gameObject;
        }
    }
}