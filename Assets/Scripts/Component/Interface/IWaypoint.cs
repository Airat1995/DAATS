using UnityEngine;

namespace DAATS.Component.Interface
{
    public interface IWaypoint : IComponentData
    {
        Vector3 Position { get; }
    }
}