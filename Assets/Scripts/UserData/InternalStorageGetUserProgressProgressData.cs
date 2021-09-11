using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAATS.UserData.Interface;
using Newtonsoft.Json;
using UnityEngine;
using static System.Text.Encoding;

namespace DAATS.UserData
{
    class InternalStorageGetUserProgressProgressData : ISetUserProgressData, IGetUserProgressData, ISetUserSettingsData,  IGetUserSettingsData
    {
        private static readonly string _progressFileName = "DAATSGameProgress.json";
        private static readonly string _settingsFileName = "DAATSSavedSettings.json";

        //Return as last beaten level if player never played before or beat even first tutorial
        private static readonly LevelData _tutorialLevelData = new LevelData() {LevelNum = -1, Name = "Empty"};

        private readonly string _fullFileLocation;

        public InternalStorageGetUserProgressProgressData()
        {
            _fullFileLocation = Path.Combine(Application.persistentDataPath, _progressFileName);
        }

        public Task WriteCompletedLevel(LevelData beatenLevel, LevelProgress levelProgress)
        {
            var allBeatenLevels = ReadLevelsJson();

            if (allBeatenLevels.CompletedLevels == null)
                allBeatenLevels.CompletedLevels = new List<UserProgress>();
            else if (allBeatenLevels.CompletedLevels.Exists(progress => progress.LevelData == beatenLevel))
                return Task.CompletedTask;
            

            allBeatenLevels.CompletedLevels.Add(new UserProgress()
                {LevelData = beatenLevel, LevelProgress = levelProgress});
            string fullJson = JsonConvert.SerializeObject(allBeatenLevels);

            return WriteTextAsync(_fullFileLocation, fullJson);
        }

        public Task WriteSavedSettings(UserSettingsData settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            return WriteTextAsync(_settingsFileName, json);
        }

        public LevelData LastBeatenLevel()
        {
            var beatenLevels = GetBeatenLevels();
            LevelData lastBeatenLevel = _tutorialLevelData;

            foreach (var levelProgress in beatenLevels.Where(levelProgress => levelProgress.Key > lastBeatenLevel))
            {
                lastBeatenLevel = levelProgress.Key;
            }

            return lastBeatenLevel;

        }

        public Dictionary<LevelData, LevelProgress> GetBeatenLevels()
        {
            if (!File.Exists(_fullFileLocation))
                return new Dictionary<LevelData, LevelProgress>();
            
            var beatenLevels = ReadLevelsJson();

            var levelsDict = new Dictionary<LevelData, LevelProgress>();
            foreach (var beatenLevel in beatenLevels.CompletedLevels)
            {
                levelsDict[beatenLevel.LevelData] = beatenLevel.LevelProgress;
            }

            return levelsDict;
        }

        public UserSettingsData SavedSettings()
        {
            var json = ReadTextFromFile(_settingsFileName);
            if (string.IsNullOrEmpty(json))
            {
                return new UserSettingsData()
                {
                    Language = Application.systemLanguage, MusicEnabled = true, MusicVolume = 1.0f, SoundEnabled = true,
                    SoundVolume = 1.0f
                };
            }

            var settings = JsonConvert.DeserializeObject<UserSettingsData>(json);
            return settings;
        }
        
        private UserProgressData ReadLevelsJson()
        {
            if (!File.Exists(_fullFileLocation))
                return new UserProgressData();

            var json = ReadTextFromFile(_fullFileLocation);
            var beatenLevels = JsonConvert.DeserializeObject<UserProgressData>(json);
            return beatenLevels;
        }

        #region IO
        private static string ReadTextFromFile(string fileName)
        {
            return !File.Exists(fileName) ? string.Empty : File.ReadAllText(fileName, UTF8);
        }

        static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = UTF8.GetBytes(text);

            using FileStream sourceStream = new FileStream(filePath,
                FileMode.Create, FileAccess.Write, FileShare.None,4096, true);
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }
        #endregion
    }
}
