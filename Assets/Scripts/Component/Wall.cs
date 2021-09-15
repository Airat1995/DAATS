using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.Component
{
    public class Wall : MonoBehaviour, IWall, IVisualElement
    {
        [SerializeField]
        private MeshRenderer _renderer;
        public Material Material => _renderer.material;

        private Action<Collider, IWall> _onWallHit = (collision, wall) => { };

        public void SubscribeOnWallHit(Action<Collider, IWall> onWallHit)
        {
            _onWallHit += onWallHit;
        }

        public void UnsubscribeOnWallHit(Action<Collider, IWall> onWallHit)
        {
            _onWallHit -= onWallHit;
        }

        void OnTriggerEnter(Collider collider)
        {
            _onWallHit.Invoke(collider, this);
        }
    }
}