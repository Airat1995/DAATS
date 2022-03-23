using System.Linq;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class ChaoticEnemyMovementSystem : IChaoticEnemyMovementSystem
    {
        private readonly IChaoticEnemy _enemy;

        private IWaypoint _moveWaypoint = default;
        private bool _enabled = false;

        public ChaoticEnemyMovementSystem(IChaoticEnemy enemy)
        {
            _enemy = enemy;
            _enemy.Agent.speed = _enemy.Speed;
            _enabled = _enemy.Enalbed;
            _enemy.SubscribeOnEnableStateChanges(OnStateChanged);
        }

        private void OnStateChanged(bool newState)
        {
            _enemy.Agent.isStopped = newState;
            _enabled = newState;
        }

        public void SetPosition(Vector3 position)
        {
           _enemy.Transform.position = position;
        }

        public void Update(float deltaTime)
        {
            if (!_enabled) return;
            if (_moveWaypoint == null)
            {
                SetMovepoint();
                return;
            }

            if (_enemy.Agent.remainingDistance <= WaypointMovementSystem.DISTANCE_THRESHOLD)
                SetMovepoint();
        }

        private void SetMovepoint()
        {
            var range = _enemy.Waypoints.Where(waypoint => !waypoint.Equals(_moveWaypoint));
            var rand = Random.Range(0, _enemy.Waypoints.Count - 1);
            _moveWaypoint = range.ElementAt(rand);
            _enemy.Agent.SetDestination(_moveWaypoint.Position);

        }
    }
}