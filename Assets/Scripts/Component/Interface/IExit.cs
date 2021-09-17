using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IExit : IComponent
    {
        void SubscribeOnCollide(Action<Collider> collisionAction);
        void UnsubcribeOnCollide(Action<Collider> collisionAction);
    }
}