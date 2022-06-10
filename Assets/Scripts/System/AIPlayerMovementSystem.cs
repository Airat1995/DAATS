using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace DAATS.Initializer.System
{
    public class AIPlayerMovementSystem : IControllerMovementSystem
    {
        private const float ANGLE_TO_ENEMY_CHECK = 45.0f;
        private const float DISTANCE_CHECK = 0.05f;
        
        private readonly IAIPlayer _aiPlayer;
        private readonly IExit _exit;

        private readonly RaycastHit[] _raycastHits;
        private readonly Vector3[] _vertices;
        private readonly float _resetTime;
        private readonly float _chanceToFail;
        private readonly LayerMask _enemyLayerMask;
        private readonly LayerMask _wallLayerMask;

        private int _currentInterestPoint = 0;
        private float _timer = 0.0f;
        private bool _moveBlocked = false;
        private bool _moveToRightPosition = false;

        public Vector3 MoveVector => _aiPlayer.Transform.forward;
        public Vector3 CurrentPosition => _aiPlayer.Transform.position;

        public bool MoveBlocked => _moveBlocked;

        public AIPlayerMovementSystem(IAIPlayer aiPlayer, IExit exit)
        {
            _aiPlayer = aiPlayer;
            _exit = exit;
            _aiPlayer.MovementAgent.speed = _aiPlayer.Speed;
            _resetTime = _aiPlayer.TimeToRethink;
            _chanceToFail = _aiPlayer.ChanceToMiss;
            
            var navMesh = NavMesh.CalculateTriangulation();
            _vertices = navMesh.vertices;

            _enemyLayerMask = LayerMask.GetMask("Enemy");
            _wallLayerMask = LayerMask.GetMask("Wall");
            _raycastHits = new RaycastHit[100];
        }

        public void Update(float deltaTime)
        {
            var enemyNearby = CheckForEnemy();
            _aiPlayer.MovementAgent.isStopped = enemyNearby switch
            {
                true when !_moveBlocked => true,
                false when _moveBlocked => false,
                _ => _aiPlayer.MovementAgent.isStopped
            };

            var reachedDestination = _aiPlayer.MovementAgent.remainingDistance < DISTANCE_CHECK;
            if (reachedDestination && _moveToRightPosition)
                _moveToRightPosition = false;

            if (enemyNearby) return;

            _timer -= deltaTime;
            if (_timer > 0.0f) return;
            if (_moveToRightPosition) return;
            RandomizeMovement();
        }
        
        public void BlockMove(bool block)
        {
            if(_moveBlocked == block)
                return;
            
            _moveBlocked = block;
            _aiPlayer.MovementAgent.isStopped = _moveBlocked;
        }

        private void RandomizeMovement()
        {
            var randMiss = Random.Range(0, 1.0f);
            Vector3 finalPos;
            if (randMiss < _chanceToFail)
            {
                var randPoint = Random.Range(0, _vertices.Length);
                finalPos = _vertices[randPoint];
            }
            else
            {
                _moveToRightPosition = true;
                var movePosition = GetNearestPoint();
                _currentInterestPoint = Mathf.Min(_aiPlayer.PointsOfInterest.Count - 1, _currentInterestPoint + 1);
                finalPos = movePosition;
                
            }
            SetFinalPosition(finalPos);
        }

        private bool CheckForEnemy()
        {
            var enemyNearby = false;
            var aiPlayerPosition = _aiPlayer.Transform.position;
            var ray = new Ray(aiPlayerPosition, _aiPlayer.Transform.forward);
            var size = Physics.SphereCastNonAlloc(ray, _aiPlayer.Speed * _timer, _raycastHits,
                _aiPlayer.Speed * _resetTime, _enemyLayerMask);
            if (size <= 0)
                return false;

            for (var raycastIndex = 0; raycastIndex < size; raycastIndex++)
            {
                var raycastHit = _raycastHits[raycastIndex];
                
                var angleTooLarge = AngleCheck(aiPlayerPosition, raycastHit);
                if (angleTooLarge)
                    continue;
                
                var wallBetween = WallcastCheck(aiPlayerPosition, raycastHit);
                if (wallBetween)
                    continue;

                enemyNearby = true;

            }

            return enemyNearby;
        }

        private bool AngleCheck(Vector3 aiPlayerPosition, RaycastHit raycastHit)
        {
            var angle = Vector2.Angle(new Vector2(aiPlayerPosition.x, aiPlayerPosition.z),
                new Vector2(raycastHit.point.x, raycastHit.point.z));
            return angle > ANGLE_TO_ENEMY_CHECK;
        }
        
        private bool WallcastCheck(Vector3 aiPlayerPosition, RaycastHit raycastHit)
        {
            var wallRay = new Ray(aiPlayerPosition, aiPlayerPosition - raycastHit.point);
            var hit = Physics.Raycast(wallRay, raycastHit.distance, _wallLayerMask);
            return hit;
        }

        private Vector3 GetNearestPoint()
        {
            var nearestPoint = _aiPlayer.PointsOfInterest[0].position;
            if (_aiPlayer.PointsOfInterest.Count <= 1) return nearestPoint;
            var aiPosition = _aiPlayer.Transform.position;
            var minDistance = float.MaxValue;
            var foundIndex = 0;
            for (var index = 0; index < _aiPlayer.PointsOfInterest.Count; index++)
            {
                var point = _aiPlayer.PointsOfInterest[index];
                var distance = Vector3.Distance(aiPosition, point.position);
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                foundIndex = index;
            }

            nearestPoint = _aiPlayer.PointsOfInterest[foundIndex].position;

            return nearestPoint;
        }
        

        public void SetPosition(Vector3 position)
        {
            _aiPlayer.MovementAgent.Warp(position);
            RandomizeMovement();
        }

        public void SetFinalPosition(Vector3 endPosition)
        {
            _aiPlayer.MovementAgent.SetDestination(endPosition);
            _timer = _resetTime;
        }
    }
}