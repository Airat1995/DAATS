using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ICollectable
    {
        void SubscribeCollision(Action<Collider, ICollectable> tryToCollect);
        void UnsubscribeCollision(Action<Collider, ICollectable> tryToCollect);
        
        void Hide();
    }
}