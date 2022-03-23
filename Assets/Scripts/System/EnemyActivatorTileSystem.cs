using UnityEngine;
using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.System
{
    public class EnemyActivatorTileSystem : IEnemyActivatorTileSystem
    {
        private readonly IPlayer _player;

        public EnemyActivatorTileSystem(IPlayer player, IEnemyActivatorTile[] activatorTiles)
        {
            _player = player;

            foreach (var slidingTile in activatorTiles)
            {
				slidingTile.SubscribeOnTileEnter(OnEnterTile);
            }           
        }

		private void OnEnterTile(Collider collider, IEnemyActivatorTile activatorTile)
		{
            if (!_player.IsSameGameObject(collider.gameObject)) return;
            foreach (var tile in activatorTile.EnemySpawnPoints)
            {
                tile.AssociatedEnemy.Enable();
            }
		}
    }
}