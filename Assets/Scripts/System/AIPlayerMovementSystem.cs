using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DAATS.Initializer.System
{
    public class AIPlayerMovementSystem : IControllerMovementSystem
    {
        private readonly IAIPlayer _aiPlayer;
        public Vector3 MoveVector { get; }
        public Vector3 CurrentPosition { get; }
        public bool MoveBlocked { get; }

        public AIPlayerMovementSystem(IAIPlayer aiPlayer)
        {
            _aiPlayer = aiPlayer;
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public void SetFinalPosition(Vector3 endPosition)
        {
            throw new NotImplementedException();
        }

        public void BlockMove(bool block)
        {
            throw new NotImplementedException();
        }
    }
}