using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class EnemyActivatorTile : MonoBehaviour, IEnemyActivatorTile, IVisualElement
    {
        [SerializeField]
        private MovableEnemySpawnPoint[] _simpleSpawnPoints;

        [SerializeField]
        private WaypointsEnemySpawnPoint[] _waypointSpawnPoints;

        private IMovableEnemySpawnPoint[] _spawnPoints;
        public IMovableEnemySpawnPoint[] EnemySpawnPoints
        {
            get
            {
                if(_spawnPoints == null)
                {
                    _spawnPoints = new IMovableEnemySpawnPoint[_simpleSpawnPoints.Length + _waypointSpawnPoints.Length];
                    _simpleSpawnPoints.CopyTo(_spawnPoints, 0);
                    _waypointSpawnPoints.CopyTo(_spawnPoints, _simpleSpawnPoints.Length);
                }                

                return _spawnPoints;
            }
        }

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

        private Action<Collider, IEnemyActivatorTile> _onTileEnterAction = (collider, tile) => { };
        private Action<Collider, IEnemyActivatorTile> _onTileUpdateAction = (collider, tile) => { };
        private Action<Collider, IEnemyActivatorTile> _onTileExitAction = (collider, tile) => { };


        public void SubscribeOnTileEnter(Action<Collider, IEnemyActivatorTile> onTileEnter)
        {
            _onTileEnterAction += onTileEnter;
        }

        public void SubscribeOnTileUpdate(Action<Collider, IEnemyActivatorTile> onTileUpdate)
        {
            _onTileUpdateAction += onTileUpdate;
        }

        public void SubscribeOnTileExit(Action<Collider, IEnemyActivatorTile> onTileExit)
        {
            _onTileExitAction += onTileExit;
        }

        public void UnsubscribeOnTileEnter(Action<Collider, IEnemyActivatorTile> onTileEnter)
        {
            _onTileEnterAction -= onTileEnter;
        }

        public void UnsubscribeOnTileUpdate(Action<Collider, IEnemyActivatorTile> onTileUpdate)
        {
            _onTileUpdateAction -= onTileUpdate;
        }

        public void UnsubscribeOnTileExit(Action<Collider, IEnemyActivatorTile> onTileExit)
        {
            _onTileExitAction -= onTileExit;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _onTileEnterAction.Invoke(collider, this);
        }

        private void OnTriggerStay(Collider collider)
        {
            _onTileUpdateAction.Invoke(collider, this);
        }

        private void OnTriggerExit(Collider collider)
        {
            _onTileExitAction.Invoke(collider, this);
        }
    }
}