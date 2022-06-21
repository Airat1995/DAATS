using System.Collections.Generic;
using DAATS.Component.Interface;
using DAATS.System.Interface;

namespace DAATS.Initializer.System
{
	public class WallHitSystem : IWallHitSystem
	{
		private readonly List<IWall> _walls;
		private readonly IPlayer _player;

		public WallHitSystem(List<IWall> walls, IPlayer player)
		{
			_walls = walls;
			_player = player;
		}
	}
}