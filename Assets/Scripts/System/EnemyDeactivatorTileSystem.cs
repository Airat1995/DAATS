using UnityEngine;
using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.System
{
    public class EnemyDeactivatorTileSystem : IEnemyDeactivatorTileSystem
    {
        private readonly IPlayer _player;

        public EnemyDeactivatorTileSystem(IPlayer player, IEnemyDeactivatorTile[] deactivatorTiles)
        {
            _player = player;

            foreach (var slidingTile in deactivatorTiles)
            {
				slidingTile.SubscribeOnTileEnter(OnEnterTile);
            }           
        }

		private void OnEnterTile(Collider collider, IEnemyDeactivatorTile deactivatorTile)
		{
            if (!_player.IsSameGameObject(collider.gameObject)) return;
            foreach (var activatorTile in deactivatorTile.ConnectedTile)
            {
                foreach (var enemySpawnPoint in activatorTile.EnemySpawnPoints)
                {
                    enemySpawnPoint.AssociatedEnemy.Disable();
                }
            }            
		}
    }
}