using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class Player : MonoBehaviour, IRealPlayer, IVisualElement
    {
        [SerializeField]
        private float _speed;
        public float Speed => _speed;

        [SerializeField]
        private uint _maxHealth;
        public uint MaxHealth => _maxHealth;
        public Transform Transform => transform;

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;
        
        [SerializeField]
        private CharacterController _characterController;    
        public CharacterController CharacterController => _characterController;


        public bool IsSameGameObject(GameObject collisionGameObject)
        {
            return collisionGameObject == gameObject;
        }
    }
}