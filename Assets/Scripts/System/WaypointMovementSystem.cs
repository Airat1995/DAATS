using System;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class WaypointMovementSystem : IWaypointMovementSystem
    {
        //Distance where we think that object meet final position
        public static readonly float DISTANCE_THRESHOLD = 0.005f;
        private readonly Transform _moveTransform;
        private readonly IWaypointEnemy _enemy;
        
        
        private int _moveIndex = 0;
        private bool _moveFinished = true;
        private Vector3 _endPosition;
        private Vector3 _currentPosition;
        private float _speed;
        private bool _enabled = false;
                
        private Vector3 MoveVector
        {
            get
            {
                var moveVector = (_endPosition - _currentPosition).normalized;
                if (moveVector != Vector3.zero)
                    return moveVector;

                return _moveTransform.forward.normalized;
            }
        }

        public WaypointMovementSystem(IWaypointEnemy waypointEnemy)
        {
            _enemy = waypointEnemy;
            _moveTransform = waypointEnemy.Transform;
            _enabled = _enemy.Enalbed;
            _enemy.SubscribeOnEnableStateChanges(OnStateChanged);
        }

        private void OnStateChanged(bool newState)
        {
            _enabled = newState;
        }

        public void Update(float deltaTime)
        {
            if(!_enabled) return;
            Move(deltaTime);
            if (!_moveFinished) return;
            SetNewMovePoint(_enemy.Transform.position,
                _enemy.Waypoints[_moveIndex].Position, _enemy.Speed);
        }

        public void SetPosition(Vector3 position)
        {
            _moveTransform.transform.position = position;
            _currentPosition = position;
        }

        private void Move(float deltaTime)
        {
            if (Vector3.Distance(_currentPosition, _endPosition) <= DISTANCE_THRESHOLD)
            {
                Stop();
                return;
            }

            Vector3 finalDistPosition = _currentPosition + MoveVector * _speed * deltaTime;
            _moveTransform.LookAt(finalDistPosition);
            SetPosition(finalDistPosition);
        }

        private void SetNewMovePoint(Vector3 startPosition, Vector3 endPosition, float speed)
        {
            _moveTransform.transform.position = startPosition;
            _currentPosition = startPosition;
            _endPosition = new Vector3(endPosition.x, startPosition.y, endPosition.z);
            _speed = speed;
            _moveFinished = false;

            if (_moveIndex == _enemy.Waypoints.Count - 1)
                _moveIndex = -1;
            ++_moveIndex;
        }

        private void Stop()
        {
            _endPosition = _currentPosition;
            _moveFinished = true;
        }
    }
}