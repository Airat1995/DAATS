using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.System
{
    public class SlidingTileSystem : ISlidingTileSystem
    {
        private static readonly int OFFSET = 100000;

        private readonly IPlayer _player;
        private readonly IMovementSystem _movementSystem;

        public SlidingTileSystem(IPlayer player, IMovementSystem movementSystem, List<ISlidingTile> slidingTiles, List<IWall> walls)
        {
            _player = player;
            _movementSystem = movementSystem;

            foreach (var slidingTile in slidingTiles)
            {
                slidingTile.SubscribeOnTileUpdate(OnSlide);
            }

            foreach (var wall in walls)
            {
                wall.SubscribeOnWallHit(OnWallHit);
            }
        }

        private void OnSlide(Collider collider, ISpecialTile slideTile)
        {
            if(_movementSystem.MoveBlocked) return;
            if (!_player.IsSameGameObject(collider.gameObject)) return;
            _movementSystem.Move(_player.Transform.position, _movementSystem.MoveVector * OFFSET, _player.Speed);
            _movementSystem.BlockMove(true);
        }

        private void OnWallHit(Collision collision, IWall wall)
        {
            if (!_player.IsSameGameObject(collision.gameObject)) return;
            _movementSystem.Stop();
        }
    }
}