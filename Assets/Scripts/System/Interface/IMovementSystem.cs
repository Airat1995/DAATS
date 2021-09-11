using UnityEngine;

namespace DAATS.System.Interface
{
    public interface IMovementSystem : IUpdatableSystem
    {
        bool MoveFinished { get; }
        void Move(Vector3 startPosition, Vector3 endPosition, float speed);

        Vector3 MoveVector { get; }

        bool MoveBlocked { get; }
        void BlockMove(bool block);
        void Stop();
    }
}