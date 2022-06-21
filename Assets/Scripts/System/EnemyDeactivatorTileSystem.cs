using UnityEngine;
using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
    public class EnemyDeactivatorTileSystem : IEnemyDeactivatorTileSystem
    {
        private readonly IWorldTriggerObject _worldTriggerObject;

        public EnemyDeactivatorTileSystem(IWorldTriggerObject worldTriggerObject, IEnemyDeactivatorTile[] deactivatorTiles)
        {
            _worldTriggerObject = worldTriggerObject;

            foreach (var slidingTile in deactivatorTiles)
            {
				slidingTile.SubscribeOnTileEnter(OnEnterTile);
            }           
        }

		private void OnEnterTile(Collider collider, IEnemyDeactivatorTile deactivatorTile)
        {
            if (!_worldTriggerObject.IsSameGameObject(collider.gameObject))
                return;
            foreach (var activatorTile in deactivatorTile.ConnectedTile)
            {
                foreach (var enemySpawnPoint in activatorTile.EnemySpawnPoints)
                {
                    enemySpawnPoint.AssociatedEnemy?.Disable();
                }
            }
		}
    }
}