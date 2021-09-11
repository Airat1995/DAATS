using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IEnemy : IComponent
    {
        int Damage { get; }

        void SubscribeOnCollide(Action<Collision> onCollide);
        void UnsubscribeOnCollide(Action<Collision> onCollide);
    }
}