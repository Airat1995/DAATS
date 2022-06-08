using System;
using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.Initializer.GameModes.Interface;
using DAATS.Initializer.GameWorld.World.Interface;
using DAATS.System.Interface;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DAATS.Initializer.GameModes
{
    public class AllCollectablesGameMode : IGameMode
    {
        private readonly ICollectionSystem _playerCollectionSystem;
        private readonly ICollectionSystem _aiCollectionSystem;
        private readonly IPlayerHealthSystem _playerHealthSystem;
        private readonly IExitLevelSystem _exitLevelSystem;
        
        private readonly IGameWorld _gameWorld;

        private readonly Action<ICollectable> _collected = collectable => { };

        private readonly int _allCollectablesCount;
        private int _playerCollectedCount = 0;
        private int _aiCollectedCount = 0;

        public AllCollectablesGameMode(ICollectable[] allCollectables, ICollectionSystem playerCollectionSystem, ICollectionSystem aiCollectionSystem, IPlayerHealthSystem playerHealthSystem, IExitLevelSystem exitLevelSystem, IGameWorld gameWorld, IAIPlayer aiPlayer)
        {
            playerCollectionSystem.SubscribeOnCollectedOne(PlayerCollectedOne);
            aiCollectionSystem.SubscribeOnCollectedOne(AICollectedOne);
            _exitLevelSystem = exitLevelSystem;
            _gameWorld = gameWorld;

            _allCollectablesCount = allCollectables.Length;
            
            _collected += (collectable) =>
            {
                aiPlayer.PointsOfInterest.Remove(collectable.Transform);
            };
        }

        private void AICollectedOne(ICollectable collectable)
        {
            ++_aiCollectedCount;
            _collected(collectable);

            // if (_aiCollectedCount < Mathf.RoundToInt(_allCollectablesCount/2.0f + 0.1f))
            //     return;

            //_gameWorld.LoseLevel();
        }

        private void PlayerCollectedOne(ICollectable collectable)
        {
            ++_playerCollectedCount;
            _collected(collectable);
            
            // if (_playerCollectedCount < Mathf.RoundToInt(_allCollectablesCount%2 + 0.1f))
            //     return;
            
            //_gameWorld.FinishLevel();
        }
    }
}