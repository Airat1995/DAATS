using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ISpecialTile<T> : IComponent where T : class
    {
        void SubscribeOnTileEnter(Action<Collider, T> onTileEnter);
        void SubscribeOnTileUpdate(Action<Collider, T> onTileUpdate);
        void SubscribeOnTileExit(Action<Collider, T> onTileExit);
        void UnsubscribeOnTileEnter(Action<Collider, T> onTileEnter);
        void UnsubscribeOnTileUpdate(Action<Collider, T> onTileUpdate);
        void UnsubscribeOnTileExit(Action<Collider, T> onTileExit);
    }
}