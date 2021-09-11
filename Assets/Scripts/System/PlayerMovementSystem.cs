using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class PlayerMovementSystem : IUpdatableSystem
    {
        private readonly IInputSystem _inputSystem;
        private readonly MovementSystem _playerMovementSystem;
        private readonly IPlayer _currentPlayer;
        private readonly float _speed;

        public PlayerMovementSystem(IInputSystem inputSystem, MovementSystem playerMovementSystem, IPlayer currentPlayer)
        {
            _inputSystem = inputSystem;
            _playerMovementSystem = playerMovementSystem;
            _currentPlayer = currentPlayer;
            _speed = _currentPlayer.Speed;
        }

        public void Update(float deltaTime)
        {
            var movementVector = _inputSystem.MovementVector;
            var moveEndPosition = _currentPlayer.Transform.position
                                  + new Vector3(movementVector.x, 0, movementVector.y);
            _playerMovementSystem.Move(_currentPlayer.Transform.position, moveEndPosition, _speed);
        }
    }
}