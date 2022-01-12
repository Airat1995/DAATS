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
		private readonly float _reachDistance;

        private bool _blocked = false;
        

        private Vector3 _endPosition;
        private Vector3 _currentPosition;

        public bool MoveBlocked => _blocked;		
		public Vector3 CurrentPosition => _currentPosition;
        public Vector3 MoveVector
        {
            get
            { 
                if(_blocked)
                    return (_endPosition - _currentPosition).normalized;

                return _currentPlayer.Transform.forward;
            }
        }

        public PlayerMovementSystem(IInputSystem inputSystem, IRealPlayer currentPlayer)
        {
            _inputSystem = inputSystem;
            _currentPlayer = currentPlayer;
            _speed = _currentPlayer.Speed;
            _playerCharacter = currentPlayer.CharacterController;
			_reachDistance = currentPlayer.CharacterController.minMoveDistance * 10;
        }

        public void Update(float deltaTime)
        {
            var inputVector = _inputSystem.InputData;
            var movementVector = !_blocked ? new Vector3(inputVector.x, 0, inputVector.y) : MoveVector;
            Move(movementVector * _speed * deltaTime);
        }

        private void Move(Vector3 moveVector)
        {
			moveVector.y = 0;
            if(Vector3.Distance(moveVector, Vector3.zero) < _reachDistance)
			{
				_blocked = false;
                return;
			}
            _playerCharacter.Move(moveVector);
            _currentPosition = _currentPlayer.Transform.position;
            _currentPlayer.Transform.rotation = Quaternion.LookRotation(moveVector);
        }

        public void SetFinalPosition(Vector3 endPosition)
        {
            _currentPosition = _currentPlayer.Transform.position;
            _endPosition = new Vector3(endPosition.x, 0, endPosition.z);
        }

        public void SetPosition(Vector3 position)
        {
            _playerCharacter.enabled = false;
            _endPosition = position;
            _currentPosition = position;
            _currentPlayer.Transform.position = position;
            _playerCharacter.enabled = true;
        }

        public void UnlockMovement()
        {
            _blocked = false;
        }

        public void BlockMovement()
        {
            _blocked = true;
        }
    }
}