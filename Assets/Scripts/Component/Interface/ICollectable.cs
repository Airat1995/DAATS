using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ICollectable
    {
        Transform Transform { get; }
        bool Collected { get; }
        
        void SubscribeCollision(Action<Collider, ICollectable> tryToCollect);
        void UnsubscribeCollision(Action<Collider, ICollectable> tryToCollect);
        
        void Collect();
    }
}