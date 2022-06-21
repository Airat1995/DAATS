using System;
using DAATS.Component.Interface;
using UnityEngine;

namespace DAATS.System.Interface
{
    public interface ICollectionSystem : ICallableSystem
    {
        bool AllCollected { get; }

        void TryToCollect(Collider collider, ICollectable collectable);

        void SubscribeOnCollectedOne(Action<ICollectable> onCollected);
        void SubscribeOnCollectedAll(Action<ICollectable> allCollected);
    }
}