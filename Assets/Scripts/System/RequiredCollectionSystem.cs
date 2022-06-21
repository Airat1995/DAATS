using System;
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

        private Action<ICollectable> _onCollectedOne = (collectable) => { };
        private Action<ICollectable> _onCollectedAll = (collectable) => { };

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

            collectable.Collect();
            _requiredCollectables.Dequeue();

            _onCollectedOne(collectable);
            if (AllCollected)
                _onCollectedAll(collectable);
        }

        public void SubscribeOnCollectedOne(Action<ICollectable> onCollected)
        {
            _onCollectedOne += onCollected;
        }

        public void SubscribeOnCollectedAll(Action<ICollectable> allCollected)
        {
            _onCollectedAll += allCollected;
        }
    }
}