using System;
using System.Collections.Generic;
using System.Linq;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class MoreCollectionSystem : ICountableCollectionSystem
    {
        private readonly IWorldTriggerObject _collector;
        private readonly ICollectable[] _allCollectables;

        private Action<ICollectable> _onOnCollected = (collectable) => { };
        private Action<ICollectable> _onAllCollected = (collectable) => { };

        public uint CollectedCount { get; private set; } = 0;
        public bool AllCollected { get; private set; }

        private Action<Transform> _aiPlayerCollect = (collectable) => { };

        public MoreCollectionSystem(IWorldTriggerObject collector, ICollectable[] allCollectables)
        {
            _collector = collector;
            _allCollectables = allCollectables;
            
            foreach (var collectable in _allCollectables)
            {
                collectable.SubscribeCollision(TryToCollect);
            }
        }

        public void TryToCollect(Collider collider, ICollectable collectable)
        {
            if (!_collector.IsSameGameObject(collider.gameObject))
                return;

            collectable.Collect();
            CollectedCount += 1;
            _onOnCollected(collectable);
            AllCollected = CheckAllCollected();
            _aiPlayerCollect(collectable.Transform);

            if (AllCollected)
                _onAllCollected(collectable);
        }

        public void SubscribeOnCollectedOne(Action<ICollectable> onCollected)
        {
            _onOnCollected += onCollected;
        }

        public void SubscribeOnCollectedAll(Action<ICollectable> allCollected)
        {
            _onAllCollected += allCollected;
        }

        private bool CheckAllCollected()
        {
            return _allCollectables.All(collectable => collectable.Collected);
        }
    }
}