using DAATS.Component.Interface;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DAATS.Initializer.Component
{
    public class MovableEnemySpawnPoint : MonoBehaviour,  IMovableEnemySpawnPoint
    {
        [SerializeField]
        private bool _enabledFromStart = true;
        public bool EnabledFromStart => _enabledFromStart;

        [SerializeField]
        private float _speed = 0;
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public Transform SpawnTransform => transform;

        private IMovableEnemy _associatedEnemy;
        public IMovableEnemy AssociatedEnemy => _associatedEnemy;
        
        public void AddAssociatedEnemy(IMovableEnemy associatedEnemy)
        {
            if(_associatedEnemy != null) return;
            _associatedEnemy = associatedEnemy;
            associatedEnemy.Speed = _speed;
        }
    }
}