using System.Collections.Generic;

namespace DAATS.UserData.Interface
{
    public interface IGetUserProgressData
    {
        LevelData LastBeatenLevel();
        Dictionary<LevelData, LevelProgress> GetBeatenLevels();
    }
}