using DAATS.Initializer.System.Window.Interface;

namespace DAATS.Initializer.System.Window.SettingsWindow.Interface
{
    public interface ISettingsWindowView : IWindowView
    {
        void SetEventReceiver(ISettingsWindowEventReceiver settingsWindowEventReceiver);

        void SetSettings(bool musicEnabled, bool soundEnabled, float musicVolume, float soundVolume);
    }
}