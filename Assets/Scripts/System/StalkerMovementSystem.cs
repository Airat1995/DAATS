using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
    public class StalkerMovementSystem : IUpdatableSystem
    {
        private readonly IStalkerEnemy _enemy;
        private readonly IPlayer _followPlayer;

        public StalkerMovementSystem(IStalkerEnemy enemy, IPlayer followPlayer)
        {
            _enemy = enemy;
            _followPlayer = followPlayer;
            _enemy.Agent.speed = _enemy.Speed;
        }

        public void Update(float deltaTime)
        {
            _enemy.Agent.SetDestination(_followPlayer.Transform.position);
        }
    }
}