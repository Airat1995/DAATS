using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class StalkerEnemyMovementSystem : IStalkerEnemyMovementSystem
    {
        private readonly IStalkerEnemy _enemy;
        private readonly IPlayer _followPlayer;

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

        public void Update(float deltaTime)
        {
            _enemy.Agent.SetDestination(_followPlayer.Transform.position);
        }
    }
}