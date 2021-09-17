using UnityEngine;

namespace DAATS.System.Interface
{
    public interface IControllerMovementSystem : IMovementSytem
    {
        Vector3 MoveVector { get; }
        bool MoveBlocked { get; }

        void SetFinalPosition(Vector3 endPosition);     
        void BlockMove(bool block);
    }
}
