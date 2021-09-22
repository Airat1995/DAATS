using DAATS.Component.Interface;

namespace DAATS.System.Interface
{
	public interface IPlayerHealthSystem : ICallableSystem
	{
		void DealDamage(IEnemy enemy);
	}
}