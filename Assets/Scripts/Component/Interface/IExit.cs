using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IExit : IComponent
    {
        Vector3 Position { get; }
        
        void SubscribeOnCollide(Action<Collider> collisionAction);
        void UnsubcribeOnCollide(Action<Collider> collisionAction);

    }
}