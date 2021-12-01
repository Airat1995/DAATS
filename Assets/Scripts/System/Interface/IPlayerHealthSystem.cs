using System;
using DAATS.Component.Interface;

namespace DAATS.System.Interface
{
	public interface IPlayerHealthSystem : ICallableSystem
	{
		void DealDamage(IEnemy enemy);

		void SubscribeOnHealthChange(Action<uint, uint> onHealthChange);
		void UnsubscribeOnHealthChange(Action<uint, uint> onHealthChange);
	}
}