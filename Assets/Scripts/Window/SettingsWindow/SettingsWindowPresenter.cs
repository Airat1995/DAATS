using DAATS.Initializer.GameWorld.Loader.Interface;
using DAATS.Initializer.System.Window.SettingsWindow.Interface;
using UnityEngine;
using Zenject;

namespace DAATS.Initializer.System.Window.SettingsWindow
{
    public class SettingsWindowPresenter : ISettingsWindowPresenter, ISettingsWindowEventReceiver
    {
        private readonly ISettingsWindowView _window;
        private readonly SettingsUserDataController _controller;
        private readonly DiContainer _container;

        private IGameWorldLoader _gameWorldLoader => _container.Resolve<IGameWorldLoader>();

        public SettingsWindowPresenter(ISettingsWindowView window, SettingsUserDataController controller, DiContainer container)
        {
            _window = window;
            _controller = controller;
            _container = container;
            _window.SetEventReceiver(this);

            _window.SetSettings(_controller.MusicEnabled, _controller.SoundEnabled, _controller.MusicVolume,
                _controller.SoundVolume);
        }

        public void SetActive(bool enable)
        {
            if(enable)
                _window.Open();
            else
                _window.Close();
        }

        public void Update(float deltaTime)
        {
        }

        public void Open()
        {
            _gameWorldLoader.Pause();
            _window.Open();
        }

        public void Close()
        {
            _gameWorldLoader.Resume();
            _controller.Write();
            _window.Close();
        }

        public void SetMusicEnable(bool enable)
        {
            _controller.EnableMusic(enable);
        }

        public void SetSoundEnable(bool enable)
        {
            _controller.EnableSound(enable);
        }

        public void SetMusicVolume(float value)
        {
            _controller.SetMusicVolume(value);
        }

        public void SetSoundVolume(float value)
        {
            _controller.SetSoundVolume(value);
        }

        public void SetLanguage(SystemLanguage language)
        {
            _controller.SetLanguage(language);
        }
    }
}