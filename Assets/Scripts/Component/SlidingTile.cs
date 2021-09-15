using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Component
{
    [RequireComponent(typeof(Collider))]
    public class SlidingTile : MonoBehaviour, ISlidingTile, IVisualElement
    {
        private Action<Collider, ISpecialTile> _onTileEnterAction = (collider, tile) => { };
        private Action<Collider, ISpecialTile> _onTileUpdateAction = (collider, tile) => { };
        private Action<Collider, ISpecialTile> _onTileExitAction = (collider, tile) => { };

        [SerializeField]
        private MeshRenderer _renderer;

        public Material Material => _renderer.material;


        public void SubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter)
        {
            _onTileEnterAction += onTileEnter;
        }

        public void SubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate)
        {
            _onTileUpdateAction += onTileUpdate;
        }

        public void SubscribeOnTileExit(Action<Collider, ISpecialTile> onTileExit)
        {
            _onTileExitAction += onTileExit;
        }

        public void UnsubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter)
        {
            _onTileEnterAction -= onTileEnter;
        }

        public void UnsubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate)
        {
            _onTileUpdateAction -= onTileUpdate;
        }

        public void UnsubscribeOnTileExit(Action<Collider, ISpecialTile> onTileExit)
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