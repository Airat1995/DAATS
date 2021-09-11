using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IWall : IComponent
    {
        void SubscribeOnWallHit(Action<Collision, IWall> onWallHit);
        void UnsubscribeOnWallHit(Action<Collision, IWall> onWallHit);
    }
}