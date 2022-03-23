using UnityEngine;
using DAATS.Component.Interface;

namespace DAATS.Initializer.Component
{
    public class EnemySpawnPoint : MonoBehaviour, IEnemySpawnPoint
    {

        [SerializeField]
        private bool _enabledFromStart;
        public bool EnabledFromStart => _enabledFromStart;

        public Transform SpawnTransform => transform;

        private IEnemy _associatedEnemy;
        public IEnemy AssociatedEnemy => _associatedEnemy;

        /// <summary> Add associated enemy to the spawn point. Can be called only once. </summary>
        public void AddAssociatedEnemy(IEnemy associatedEnemy) 
        {
            if(_associatedEnemy != null) return;
            _associatedEnemy = associatedEnemy;
        }
        
    }
}