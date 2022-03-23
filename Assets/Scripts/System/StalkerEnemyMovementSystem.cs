using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class StalkerEnemyMovementSystem : IStalkerEnemyMovementSystem
    {
        private readonly IStalkerEnemy _enemy;
        private readonly IPlayer _followPlayer;

        private bool _enabled = false;

        public StalkerEnemyMovementSystem(IStalkerEnemy enemy, IPlayer followPlayer)
        {
            _enemy = enemy;
            _followPlayer = followPlayer;
            _enemy.Agent.speed = _enemy.Speed;
            _enabled = _enemy.Enalbed;
            _enemy.SubscribeOnEnableStateChanges(OnStateChanged);
        }

        public void SetPosition(Vector3 position)
        {
            _enemy.Transform.position = position;
        }

        public void Update(float deltaTime)
        {
            if(!_enabled) return;
            _enemy.Agent.SetDestination(_followPlayer.Transform.position);
        }

        private void OnStateChanged(bool newState)
        {
            _enemy.Agent.isStopped = !newState;            
            _enabled = newState;
        }
    }
}