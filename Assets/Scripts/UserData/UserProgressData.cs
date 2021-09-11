using System;
using System.Collections.Generic;

namespace DAATS.UserData
{
    [Serializable]
    public struct UserProgressData
    {
        public List<UserProgress> CompletedLevels;
    }

    [Serializable]
    public struct UserProgress
    {
        public LevelData LevelData;
        public LevelProgress LevelProgress;
    }

    [Serializable]
    public struct LevelData
    {
        public int LevelNum;
        public string Name;

        public static bool operator >(LevelData firstLevelData, LevelData secondLevelData)
        {
            return firstLevelData.LevelNum >= secondLevelData.LevelNum;
        }

        public static bool operator <(LevelData firstLevelData, LevelData secondLevelData)
        {
            return secondLevelData > firstLevelData;
        }

        public static bool operator ==(LevelData firstLevelData, LevelData secondLevelData)
        {
            return firstLevelData.LevelNum == secondLevelData.LevelNum && firstLevelData.Name == secondLevelData.Name;
        }

        public static bool operator !=(LevelData firstLevelData, LevelData secondLevelData)
        {
            return !(firstLevelData == secondLevelData);
        }

        public LevelData GetNextLevel()
        {
            return new LevelData() { Name = Name, LevelNum = LevelNum + 1 };
        }

        public override bool Equals(object obj)
        {
            return obj is LevelData data &&
                   LevelNum == data.LevelNum &&
                   Name == data.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = -935109479;
            hashCode = hashCode * -1521134295 + LevelNum.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }

    [Serializable]
    public struct LevelProgress
    {
        public int StarCount;
        public int LeftTime;
    }
}
