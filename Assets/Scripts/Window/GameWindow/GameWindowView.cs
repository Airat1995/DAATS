using DAATS.Initializer.System.Window.GameWindow.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Initializer.System.Window.GameWindow
{
    public class GameWindowView : MonoBehaviour, IGameWindowView
    {
        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Slider _healthSlider;

        public void SetEventReceiver(IGameWindowEventReceiver eventReceiver)
        {
            _pauseButton.onClick.RemoveAllListeners();
            _pauseButton.onClick.AddListener(eventReceiver.Pause);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void UpdateHealth(float leftPercent)
        {
            _healthSlider.value = leftPercent;
        }
    }
}