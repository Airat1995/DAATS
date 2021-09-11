using System.Collections.Generic;
using DAATS.UserData;

namespace DAATS.Initializer.Level.Interface
{
    public interface ILevelCollection
    {
        LevelData GetNextLevelData(LevelData levelData);

        List<LevelData> AllLevels();
    }
}