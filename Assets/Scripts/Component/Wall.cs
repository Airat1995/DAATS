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

        private Action<Collision, IWall> _onWallHit = (collision, wall) => { };

        public void SubscribeOnWallHit(Action<Collision, IWall> onWallHit)
        {
            _onWallHit += onWallHit;
        }

        public void UnsubscribeOnWallHit(Action<Collision, IWall> onWallHit)
        {
            _onWallHit -= onWallHit;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _onWallHit.Invoke(collision, this);
        }
    }
}