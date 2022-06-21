using System;
using DAATS.Component.Interface;
using DAATS.System.Interface;
using UnityEngine;

namespace DAATS.Initializer.System
{
    public class ExitLevelSystem : IExitLevelSystem
    {
        private readonly IExit _exitComponent;
        private readonly IWorldTriggerObject _worldTriggerObject;

        private Action _onExitReach = () => { };

        public ExitLevelSystem(IExit exitComponent, IWorldTriggerObject worldTriggerObject)
        {
            _exitComponent = exitComponent;
            _worldTriggerObject = worldTriggerObject;

            _exitComponent.SubscribeOnCollide(OnExitReached);
        }

        private void OnExitReached(Collider obj)
        {
            if (_worldTriggerObject.IsSameGameObject(obj.gameObject))
                _onExitReach();
        }

        public void SubscribeOnLevelExitReach(Action onExitReach)
        {
            _onExitReach += onExitReach;
        }
    }
}