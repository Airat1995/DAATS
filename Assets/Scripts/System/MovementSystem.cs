using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class MovementSystem : IMovementSystem
    {
        //Distance where we think that object meet final position
        public static readonly float DISTANCE_THRESHOLD = 0.005f;
        private readonly Transform _moveTransform;

        public bool MoveFinished { get; private set; } = true;
        public Vector3 MoveVector => (_endPosition - _currentPosition).normalized;
        public bool MoveBlocked => _blocked;


        private Vector3 _endPosition;
        private Vector3 _currentPosition;
        private float _speed;
        private bool _blocked;



        public MovementSystem(Transform moveTransform)
        {
            _moveTransform = moveTransform;
            _endPosition = Vector3.zero;
            _currentPosition = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            if(MoveFinished)
                return;
            if (Vector3.Distance(_currentPosition, _endPosition) <= DISTANCE_THRESHOLD)
            {
                Stop();
                return;
            }

            Vector3 finalDistPosition = _currentPosition + MoveVector * _speed * deltaTime;
            _moveTransform.LookAt(finalDistPosition);
            _moveTransform.transform.position = finalDistPosition;
            _currentPosition = finalDistPosition;
        }


        public void Move(Vector3 startPosition, Vector3 endPosition, float speed)
        {
            if(_blocked)
                return;
            _moveTransform.transform.position = startPosition;
            _currentPosition = startPosition;
            _endPosition = new Vector3(endPosition.x, startPosition.y, endPosition.z);
            _speed = speed;
            MoveFinished = false;
        }

        public void BlockMove(bool block)
        {
            _blocked = block;
        }

        public void Stop()
        {
            _endPosition = _currentPosition;
            _blocked = false;
            MoveFinished = true;
        }
    }
}