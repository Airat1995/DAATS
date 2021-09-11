using UnityEngine;

namespace DAATS.System.Interface
{
    public interface IInputSystem : IUpdatableSystem
    {
        Vector2 MovementVector { get; }
    }
}