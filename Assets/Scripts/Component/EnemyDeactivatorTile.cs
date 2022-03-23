using UnityEngine;
using DAATS.Component.Interface;
using System;

namespace DAATS.Initializer.Component
{
    public class EnemyDeactivatorTile : MonoBehaviour, IEnemyDeactivatorTile, IVisualElement
    {

        [SerializeField]
        private EnemyActivatorTile[] _activatorTiles;

        public IEnemyActivatorTile[] ConnectedTile => _activatorTiles;

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

        private Action<Collider, IEnemyDeactivatorTile> _onTileEnterAction = (collider, tile) => { };
        private Action<Collider, IEnemyDeactivatorTile> _onTileUpdateAction = (collider, tile) => { };
        private Action<Collider, IEnemyDeactivatorTile> _onTileExitAction = (collider, tile) => { };


        public void SubscribeOnTileEnter(Action<Collider, IEnemyDeactivatorTile> onTileEnter)
        {
            _onTileEnterAction += onTileEnter;
        }

        public void SubscribeOnTileUpdate(Action<Collider, IEnemyDeactivatorTile> onTileUpdate)
        {
            _onTileUpdateAction += onTileUpdate;
        }

        public void SubscribeOnTileExit(Action<Collider, IEnemyDeactivatorTile> onTileExit)
        {
            _onTileExitAction += onTileExit;
        }

        public void UnsubscribeOnTileEnter(Action<Collider, IEnemyDeactivatorTile> onTileEnter)
        {
            _onTileEnterAction -= onTileEnter;
        }

        public void UnsubscribeOnTileUpdate(Action<Collider, IEnemyDeactivatorTile> onTileUpdate)
        {
            _onTileUpdateAction -= onTileUpdate;
        }

        public void UnsubscribeOnTileExit(Action<Collider, IEnemyDeactivatorTile> onTileExit)
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