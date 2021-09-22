using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.System
{
	public class EnemyHitSystem : IEnemyHitSystem
    {

        private readonly List<IEnemy> _enemies;
		private readonly IControllerMovementSystem _playerMovementSystem;
		private readonly IPlayer _player;
		private readonly IPlayerHealthSystem _healthSystem;

		public EnemyHitSystem(List<IEnemy> enemies, IControllerMovementSystem playerMovementSystem, IPlayer player, IPlayerHealthSystem healthSystem)
        {
            _enemies = enemies;
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
			_playerMovementSystem.BlockMove(false);
			_playerMovementSystem.SetFinalPosition(_playerMovementSystem.MoveVector * -1 * enemy.HitBounceDistance);
			_playerMovementSystem.BlockMove(true);
        }
    }
}