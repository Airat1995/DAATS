using DAATS.Component.Interface;
using DAATS.System.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace DAATS.System
{
    public class SlidingTileSystem : ISlidingTileSystem
    {
        private static readonly int OFFSET = 100000;

        private readonly IPlayer _player;
        private readonly IControllerMovementSystem _movementSystem;
        private bool _reverseMove = false;
        private Vector3 _lastNormal = Vector3.zero;

        public SlidingTileSystem(IPlayer player, IControllerMovementSystem movementSystem, List<ISlidingTile> slidingTiles, List<IWall> walls)
        {
            _player = player;
            _movementSystem = movementSystem;

            foreach (var slidingTile in slidingTiles)
            {
                slidingTile.SubscribeOnTileUpdate(OnSlide);
                slidingTile.SubscribeOnTileExit(OnSlideEnd);
            }

            foreach (var wall in walls)
            {
                wall.SubscribeOnWallHit(OnWallHit);
            }
        }

        private void OnSlideEnd(Collider collider, ISpecialTile slideTile)
        {
            _lastNormal = Vector3.zero;
            _reverseMove = false;
        }

        private void OnSlide(Collider collider, ISpecialTile slideTile)
        {
            if (_movementSystem.MoveBlocked) return;
            if (!_player.IsSameGameObject(collider.gameObject)) return;
            var moveVector = !_reverseMove || _lastNormal == Vector3.zero ? _movementSystem.MoveVector : _lastNormal;
            _movementSystem.SetFinalPosition(moveVector * OFFSET);
            _movementSystem.BlockMove(true);
            _reverseMove = true;
        }

        private void OnWallHit(Collider collider, IWall wall)
        {
            if (!_player.IsSameGameObject(collider.gameObject)) return;
            _movementSystem.BlockMove(false);

            Physics.Raycast(new Ray(collider.transform.position, collider.transform.forward), out var hitInfo, 1.0f);

            Vector3 incomingVec = hitInfo.point - collider.transform.position;
            _lastNormal = Vector3.Reflect(incomingVec, hitInfo.normal);
        }
    }
}