using DAATS.Component.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class AIPlayerExitLevelSystem : IPlayerExitLevelSystem
    {
        private readonly IAIPlayer _aiPlayer;
        private readonly IExit _exit;
        private readonly IGameWorld _currentWorld;

        public AIPlayerExitLevelSystem(IAIPlayer aiPlayer, IExit exit, IGameWorld currentWorld)
        {
            _aiPlayer = aiPlayer;
            _exit = exit;
            _currentWorld = currentWorld;

            _exit.SubscribeOnCollide(CheckCollision);
        }

        private void CheckCollision(Collider obj)
        {
            _currentWorld.LoseLevel();
        }
    }
}