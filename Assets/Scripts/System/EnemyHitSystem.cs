using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
	public class EnemyHitSystem : IEnemyHitSystem
    {
	    
		private readonly IControllerMovementSystem _playerMovementSystem;
		private readonly IPlayer _player;
		private readonly IPlayerHealthSystem _healthSystem;

		public EnemyHitSystem(List<IEnemy> enemies, IControllerMovementSystem playerMovementSystem, IPlayer player, IPlayerHealthSystem healthSystem)
        {
	        _playerMovementSystem = playerMovementSystem;
			_player = player;
			_healthSystem = healthSystem;
			foreach (var enemy in enemies)
            {
                enemy.SubscribeOnCollide(HitEnemy);
            }
        }

        private void HitEnemy(Collider collider, IEnemy enemy)
        {
			if(!_player.IsSameGameObject(collider.gameObject))
				return;
			
			_healthSystem.DealDamage(enemy);
			var bounceVector = _playerMovementSystem.MoveVector * -1 * enemy.HitBounceDistance;
			if (enemy is IMovableEnemy movableEnemy)
			{
				bounceVector = (movableEnemy.Transform.forward - _playerMovementSystem.MoveVector) *
				               enemy.HitBounceDistance;
			}
			
			_playerMovementSystem.BlockMove(false);
			_playerMovementSystem.SetFinalPosition(bounceVector);
			_playerMovementSystem.BlockMove(true);
        }
    }
}