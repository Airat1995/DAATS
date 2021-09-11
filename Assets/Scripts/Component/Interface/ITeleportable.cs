using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface ITeleportable : IComponent
    {
        Transform Transform { get; }
    }
}
