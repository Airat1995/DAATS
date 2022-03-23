using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Initializer.Component
{
    public class BasicEnemy : MonoBehaviour, IEnemy, IVisualElement
    {
        private bool? _enabled;		
        public bool Enalbed => _enabled ?? false;

        [SerializeField]
        private uint _damage;
        public uint Damage => _damage;

		[SerializeField]
		private float _hitBounceDistance;
		public float HitBounceDistance => _hitBounceDistance;

        public Transform Transform => transform;

        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;


        private Action<Collider, IEnemy> _onCollide = (collider, enemy) => { };

        public void SubscribeOnCollide(Action<Collider, IEnemy> onCollide)
        {
            _onCollide += onCollide;
        }

        public void UnsubscribeOnCollide(Action<Collider, IEnemy> onCollide)
        {
            _onCollide -= onCollide;
        }

        private void OnTriggerEnter(Collider collider)
        {
            _onCollide.Invoke(collider, this);
        }

        public void Enable()
        {
            if(_enabled.HasValue && _enabled.Value) return;
            _enabled = true;
            _onStateChanged.Invoke(true);
        }

        public void Disable()
        {
            if(_enabled.HasValue && !_enabled.Value) return;
            _enabled = false;
            _onStateChanged.Invoke(false);
        }

        private Action<bool> _onStateChanged = (state) => { };
        public void SubscribeOnEnableStateChanges(Action<bool> onStateChanged)
        {
            _onStateChanged += onStateChanged;
        }

        public void UnsubscribeOnEnableStateChanges(Action<bool> onStateChanged)
        {
            _onStateChanged -= onStateChanged;
        }

    }
}