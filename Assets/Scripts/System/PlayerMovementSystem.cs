using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class PlayerMovementSystem : IControllerMovementSystem
    {
        private readonly IInputSystem _inputSystem;
        private readonly IPlayer _currentPlayer;
        private readonly CharacterController _playerCharacter;        
        private readonly float _speed;

        private bool _blocked = false;
        

        private Vector3 _endPosition;
        private Vector3 _currentPosition;

        public bool MoveBlocked => _blocked;
        public Vector3 MoveVector
        {
            get
            {               
                if(_blocked)
                    return  (_endPosition - _currentPosition).normalized;

                return _currentPlayer.Transform.forward;
            }
        }

        public PlayerMovementSystem(IInputSystem inputSystem, IPlayer currentPlayer)
        {
            _inputSystem = inputSystem;
            _currentPlayer = currentPlayer;
            _speed = _currentPlayer.Speed;
            _playerCharacter = currentPlayer.CharacterController;
        }

        public void Update(float deltaTime)
        {
            var inputVector = _inputSystem.InputData;
            var movementVector = !_blocked ? new Vector3(inputVector.x, 0, inputVector.y) : MoveVector;
            Move(movementVector * _speed * deltaTime);
        }

        public void BlockMove(bool block)
        {
            _blocked = block;
        }

        private void Move(Vector3 moveVector)
        {
            if(moveVector == Vector3.zero)
                return;
            _playerCharacter.Move(moveVector);
            _currentPosition = _currentPlayer.Transform.position;
            _currentPlayer.Transform.rotation = Quaternion.LookRotation(moveVector);

            if(_endPosition == _currentPosition)
                _blocked = false;
        }

        public void SetFinalPosition(Vector3 endPosition)
        {
            _currentPosition = _currentPlayer.Transform.position;
            _endPosition = endPosition;
        }

        public void SetPosition(Vector3 position)
        {
            _playerCharacter.enabled = false;
            _endPosition = position;
            _currentPosition = position;
            _currentPlayer.Transform.position = position;
            _playerCharacter.enabled = true;
        }
    }
}