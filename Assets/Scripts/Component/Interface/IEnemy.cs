using System;
using UnityEngine;

namespace DAATS.Component.Interface
{
	public interface IEnemy : IComponent
	{
		uint Damage { get; }

		float HitBounceDistance { get; }

		Transform Transform { get; }

		void SubscribeOnCollide(Action<Collider, IEnemy> onCollide);
		void UnsubscribeOnCollide(Action<Collider, IEnemy> onCollide);
	}
}