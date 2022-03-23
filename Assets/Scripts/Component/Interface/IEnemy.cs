using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
	public interface IEnemy : IComponent
	{
		uint Damage { get; }

		float HitBounceDistance { get; }

		Transform Transform { get; }

		bool Enalbed { get; }

		void SubscribeOnCollide(Action<Collider, IEnemy> onCollide);
		void UnsubscribeOnCollide(Action<Collider, IEnemy> onCollide);

		void SubscribeOnEnableStateChanges(Action<bool> onStateChanged);
		void UnsubscribeOnEnableStateChanges(Action<bool> onStateChanged);
        
		void Enable();
		void Disable();		
    }
}