using DAATS.Initializer.Mangers.Sound.Interface;
using DAATS.UserData;
using DAATS.UserData.Interface;
using UnityEngine;

namespace DAATS.Initializer.System.Window.SettingsWindow
{
    public class SettingsUserDataController
    {
        private readonly ISetUserSettingsData _setUserData;
        private readonly IGetUserSettingsData _getUserData;
        private readonly ISoundManager _soundManager;

        private UserSettingsData _userSettings;

        public float MusicVolume => _userSettings.MusicVolume;
        public float SoundVolume => _userSettings.SoundVolume;

        public bool MusicEnabled => _userSettings.MusicEnabled;
        public bool SoundEnabled => _userSettings.SoundEnabled;

        public SettingsUserDataController(ISetUserSettingsData setUserData, IGetUserSettingsData getUserData, ISoundManager soundManager)
        {
            _setUserData = setUserData;
            _getUserData = getUserData;
            _soundManager = soundManager;

            _userSettings = _getUserData.SavedSettings();
        }

        public void SetMusicVolume(float volume)
        {
            _userSettings.MusicVolume = volume;
            _soundManager.SetMusicVolume(volume);
        }

        public void SetSoundVolume(float volume)
        {
            _userSettings.SoundVolume = volume;
            _soundManager.SetSoundVolume(volume);
        }

        public void EnableMusic(bool enable)
        {
            _userSettings.MusicEnabled = enable;
            _soundManager.EnableMusic(enable);
        }

        public void EnableSound(bool enable)
        {
            _userSettings.SoundEnabled = enable;
            _soundManager.EnableSound(enable);
        }

        public void SetLanguage(SystemLanguage language)
        {
            _userSettings.Language = language;
        }

        public void Write()
        {
            _setUserData.WriteSavedSettings(_userSettings);
        }
    }
}