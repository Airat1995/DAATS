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

		public PlayerHealthSystem(IPlayer player, IGameWorld world)
		{
			_player = player;
			_world = world;
			_health = player.MaxHealth;
		}

		public void DealDamage(IEnemy enemy)
		{
			_health -= enemy.Damage;

			if(_health <= 0)
				_world.LoseLevel();
		}
	}
}