using UnityEngine;

namespace DAATS.System.Interface
{
    public interface IMovementSytem : IUpdatableSystem
    {
        void SetPosition(Vector3 position);
    }
}
