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

        
        private Action<Collider,ICollectable> _onColliderAction = (collision, collectable) => { };

        public void SubscribeCollision(Action<Collider, ICollectable> tryToCollect)
        {
            _onColliderAction += tryToCollect;
        }

        public void UnsubscribeCollision(Action<Collider, ICollectable> tryToCollect)
        {
            _onColliderAction -= tryToCollect;
        }

        public void OnTriggerEnter(Collider collider)
        {
            _onColliderAction?.Invoke(collider, this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _onColliderAction = null;
        }
    }
}