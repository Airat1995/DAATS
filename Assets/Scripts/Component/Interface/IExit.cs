using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IExit : IComponent
    {
        void SubscribeOnCollide(Action<Collision> collisionAction);
        void UnsubcribeOnCollide(Action<Collision> collisionAction);
    }
}