using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
    public class WaypointMovementSystem : IUpdatableSystem
    {
        private readonly IMovementSystem _movementSystem;
        private readonly IWaypointEnemy _waypointEnemy;
        private int _moveIndex = 0;
        private bool _moveBack = false;

        public WaypointMovementSystem(IMovementSystem movementSystem, IWaypointEnemy waypointEnemy)
        {
            _movementSystem = movementSystem;
            _waypointEnemy = waypointEnemy;
        }

        public void Update(float deltaTime)
        {
            if (!_movementSystem.MoveFinished) return;
            _movementSystem.Move(_waypointEnemy.Transform.position, 
                _waypointEnemy.Waypoints[_moveIndex].Position, _waypointEnemy.Speed);

            if (_moveIndex == _waypointEnemy.Waypoints.Count - 1)
                _moveBack = true;
            else if (_moveIndex == 0 && _moveBack)
                _moveBack = false;

            if (_moveBack)
                --_moveIndex;
            else
                ++_moveIndex;
        }
    }
}