using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Initializer.System.Window.SettingsWindow
{
    public class SettingsWindowView : MonoBehaviour, ISettingsWindowView
    {
        [SerializeField]
        private Toggle _musicToggle;

        [SerializeField]
        private Toggle _soundToggle;

        [SerializeField]
        private Slider _musicSlider;

        [SerializeField]
        private Slider _soundSlider;

        public void SetEventReceiver(ISettingsWindowEventReceiver settingsWindowEventReceiver)
        {
            _musicToggle.onValueChanged.AddListener(settingsWindowEventReceiver.SetMusicEnable);
            _soundToggle.onValueChanged.AddListener(settingsWindowEventReceiver.SetSoundEnable);

            _musicSlider.onValueChanged.AddListener(settingsWindowEventReceiver.SetMusicVolume);
            _soundSlider.onValueChanged.AddListener(settingsWindowEventReceiver.SetSoundVolume);
        }

        public void SetSettings(bool musicEnabled, bool soundEnabled, float musicVolume, float soundVolume)
        {
            _musicToggle.SetIsOnWithoutNotify(musicEnabled);
            _soundToggle.SetIsOnWithoutNotify(soundEnabled);
            
            _musicSlider.SetValueWithoutNotify(musicVolume);
            _soundSlider.SetValueWithoutNotify(soundVolume);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}