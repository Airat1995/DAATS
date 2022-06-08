using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
	public class PlayerHealthSystem : IPlayerHealthSystem
	{

		private readonly uint _maxHealth;
		private uint _health;

		private Action<uint, uint> _onHealthChangeCallbacks = (curr, max) => { };

		public PlayerHealthSystem(IPlayer player)
		{
			_health = player.MaxHealth;
			_maxHealth = player.MaxHealth;
		}

		public void DealDamage(IEnemy enemy)
		{
			_health -= enemy.Damage;
			_onHealthChangeCallbacks(_health, _maxHealth);
		}

        public void SubscribeOnHealthChange(Action<uint, uint> onHealthChange)
        {
            _onHealthChangeCallbacks += onHealthChange;
        }

        public void UnsubscribeOnHealthChange(Action<uint, uint> onHealthChange)
        {
            _onHealthChangeCallbacks -= onHealthChange;
        }
    }
}