using UnityEngine;

namespace DAATS.Initializer.System.Window.SettingsWindow.Interface
{
    public interface ISettingsWindowEventReceiver
    {
        void SetMusicEnable(bool enable);
        void SetSoundEnable(bool enable);

        void SetMusicVolume(float value);
        void SetSoundVolume(float value);

        void SetLanguage(SystemLanguage language);
    }
}