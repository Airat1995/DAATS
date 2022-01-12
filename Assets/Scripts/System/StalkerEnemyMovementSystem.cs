using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class StalkerEnemyMovementSystem : IStalkerEnemyMovementSystem
    {
        private readonly IStalkerEnemy _enemy;
        private readonly IPlayer _followPlayer;

        private bool _movementStopped = true;

        public StalkerEnemyMovementSystem(IStalkerEnemy enemy, IPlayer followPlayer)
        {
            _enemy = enemy;
            _followPlayer = followPlayer;
            _enemy.Agent.speed = _enemy.Speed;
        }

        public void SetPosition(Vector3 position)
        {
            _enemy.Transform.position = position;
        }

        public void UnlockMovement()
        {
            _movementStopped = false;
        }

        public void BlockMovement()
        {
            _movementStopped = true;
            _enemy.Agent.isStopped = true;
        }

        public void Update(float deltaTime)
        {
            if(_movementStopped)
                return;
            _enemy.Agent.SetDestination(_followPlayer.Transform.position);
        }
    }
}