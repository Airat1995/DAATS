using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class RequiredCollectionSystem : ICollectionSystem
    {
        private readonly Queue<IRequiredCollectable> _requiredCollectables;
        private readonly IPlayer _player;

        public bool AllCollected => _requiredCollectables.Count == 0;

        public RequiredCollectionSystem(Queue<IRequiredCollectable> requiredCollectables, IPlayer player)
        {
            _requiredCollectables = requiredCollectables;
            _player = player;
            foreach (var collectable in _requiredCollectables)
            {
                collectable.SubscribeCollision(TryToCollect);
            }
        }

        public void TryToCollect(Collider collider, ICollectable collectable)
        {
            if(!_player.IsSameGameObject(collider.gameObject))
                return;

            if(_requiredCollectables.Peek() != collectable)
                return;

            collectable.Hide();
            _requiredCollectables.Dequeue();
        }
    }
}