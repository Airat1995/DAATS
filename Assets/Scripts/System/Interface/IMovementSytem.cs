using UnityEngine;

namespace DAATS.System.Interface
{
    public interface IMovementSytem : IUpdatableSystem
    {
        void UnlockMovement();
        void BlockMovement();
 
        void SetPosition(Vector3 position);
    }
}
