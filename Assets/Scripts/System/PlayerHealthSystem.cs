using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;

namespace DAATS.System
{
	public class PlayerHealthSystem : IPlayerHealthSystem
	{
		private readonly IPlayer _player;
		private readonly IGameWorld _world;

		private uint _health;
		private uint _maxHealth;

		private List<Action<uint, uint>> _onHealthChangeCallbacks;

		public PlayerHealthSystem(IPlayer player, IGameWorld world)
		{
			_onHealthChangeCallbacks = new List<Action<uint, uint>>();
			_player = player;
			_world = world;
			_health = player.MaxHealth;
			_maxHealth = player.MaxHealth;
		}

		public void DealDamage(IEnemy enemy)
		{
			_health -= enemy.Damage;

			foreach (var healthChange in _onHealthChangeCallbacks)
			{
				healthChange(_health, _maxHealth);
			}

			if(_health <= 0)
				_world.LoseLevel();
		}

        public void SubscribeOnHealthChange(Action<uint, uint> onHealthChange)
        {
            _onHealthChangeCallbacks.Add(onHealthChange);
        }

        public void UnsubscribeOnHealthChange(Action<uint, uint> onHealthChange)
        {
            _onHealthChangeCallbacks.Remove(onHealthChange);
        }
    }
}