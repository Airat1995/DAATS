using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ISpecialTile : IComponent
    {
        void SubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter);
        void SubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate);
        void UnsubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter);
        void UnsubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate);
    }
}