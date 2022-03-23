using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    [RequireComponent(typeof(Collider))]
    public class SlidingTile : MonoBehaviour, ISlidingTile, IVisualElement
    {
        private Action<Collider, ISlidingTile> _onTileEnterAction = (collider, tile) => { };
        private Action<Collider, ISlidingTile> _onTileUpdateAction = (collider, tile) => { };
        private Action<Collider, ISlidingTile> _onTileExitAction = (collider, tile) => { };

        [SerializeField]
        private MeshRenderer _renderer;

        public Material Material => _renderer.material;


        public void SubscribeOnTileEnter(Action<Collider, ISlidingTile> onTileEnter)
        {
            _onTileEnterAction += onTileEnter;
        }

        public void SubscribeOnTileUpdate(Action<Collider, ISlidingTile> onTileUpdate)
        {
            _onTileUpdateAction += onTileUpdate;
        }

        public void SubscribeOnTileExit(Action<Collider, ISlidingTile> onTileExit)
        {
            _onTileExitAction += onTileExit;
        }

        public void UnsubscribeOnTileEnter(Action<Collider, ISlidingTile> onTileEnter)
        {
            _onTileEnterAction -= onTileEnter;
        }

        public void UnsubscribeOnTileUpdate(Action<Collider, ISlidingTile> onTileUpdate)
        {
            _onTileUpdateAction -= onTileUpdate;
        }

        public void UnsubscribeOnTileExit(Action<Collider, ISlidingTile> onTileExit)
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