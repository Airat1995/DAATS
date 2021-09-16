using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ICameraComponent : IComponent
    {
        Rect BoundingBox { get; }

        float Speed { get; }
        
        float TeleportSpeed { get; }
        float TeleportDistance {get;}

        Camera Camera { get; }
        Transform Transform { get; }
    }
}