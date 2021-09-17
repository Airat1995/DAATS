using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IWall : IComponent
    {
        void SubscribeOnWallHit(Action<Collider, IWall> onWallHit);
        void UnsubscribeOnWallHit(Action<Collider, IWall> onWallHit);
    }
}