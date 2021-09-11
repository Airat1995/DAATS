using System.Linq;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class ChaoticMovementSystem : IUpdatableSystem
    {
        private readonly IChaoticEnemy _enemy;

        private IWaypoint _moveWaypoint = default;

        public ChaoticMovementSystem(IChaoticEnemy enemy)
        {
            _enemy = enemy;
            _enemy.Agent.speed = _enemy.Speed;
        }

        public void Update(float deltaTime)
        {
            if (_moveWaypoint == null)
            {
                SetMovepoint();
                return;
            }

            if (_enemy.Agent.remainingDistance <= MovementSystem.DISTANCE_THRESHOLD)
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