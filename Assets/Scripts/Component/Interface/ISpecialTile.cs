using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ISpecialTile : IComponent
    {
        void SubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter);
        void SubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate);
        void SubscribeOnTileExit(Action<Collider, ISpecialTile> onTileExit);
        void UnsubscribeOnTileEnter(Action<Collider, ISpecialTile> onTileEnter);
        void UnsubscribeOnTileUpdate(Action<Collider, ISpecialTile> onTileUpdate);
        void UnsubscribeOnTileExit(Action<Collider, ISpecialTile> onTileExit);
    }
}