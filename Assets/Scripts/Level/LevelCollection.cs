using System.Collections.Generic;
using System.Linq;
using DAATS.Initializer.Level.Interface;
using DAATS.UserData;
using UnityEngine;

namespace DAATS.Initializer.Level
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "Levels/LevelCollection", order = 1)]
    public class LevelCollection : ScriptableObject, ILevelCollection
    {
        [SerializeField]
        private List<LevelDescriptor> _levelDescriptors;

        public LevelData GetNextLevelData(LevelData levelData)
        {
            var foundIndex = _levelDescriptors.FindIndex(levelDescr => levelDescr.LevelInfo == levelData);
            if (foundIndex + 1 >= _levelDescriptors.Count)
                foundIndex = _levelDescriptors.Count - 2;

            return _levelDescriptors[foundIndex + 1].LevelInfo;
        }

        public List<LevelData> AllLevels()
        {
            var levels = _levelDescriptors.Select(levelDescriptor => levelDescriptor.LevelInfo).ToList();
            return levels;
        }
    }
}