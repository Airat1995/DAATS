using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class Symbol : MonoBehaviour, IRequiredCollectable, IVisualElement
    {
        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

        public Transform Transform => transform;
        
        private Action<Collider,ICollectable> _onColliderAction = (collision, collectable) => { };
        
        public bool Collected { get; private set; }

        public void SubscribeCollision(Action<Collider, ICollectable> tryToCollect)
        {
            _onColliderAction += tryToCollect;
        }

        public void UnsubscribeCollision(Action<Collider, ICollectable> tryToCollect)
        {
            _onColliderAction -= tryToCollect;
        }

        public void Collect()
        {
            Collected = true;
            Hide();
        }

        public void OnTriggerEnter(Collider collider)
        {
            _onColliderAction?.Invoke(collider, this);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _onColliderAction = null;
        }
    }
}