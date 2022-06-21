using UnityEngine;
using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
    public class EnemyActivatorTileSystem : IEnemyActivatorTileSystem
    {
        private readonly IWorldTriggerObject _triggerObject;
        private readonly IAIPlayer _aiPlayer;

        public EnemyActivatorTileSystem(IWorldTriggerObject triggerObject, IEnemyActivatorTile[] activatorTiles)
        {
            _triggerObject = triggerObject;

            foreach (var slidingTile in activatorTiles)
            {
				slidingTile.SubscribeOnTileEnter(OnEnterTile);
            }           
        }

		private void OnEnterTile(Collider collider, IEnemyActivatorTile activatorTile)
        {
            if (!_triggerObject.IsSameGameObject(collider.gameObject))
                return;
            foreach (var tile in activatorTile.EnemySpawnPoints)
            {
                tile.AssociatedEnemy?.Enable();
            }
		}
    }
}