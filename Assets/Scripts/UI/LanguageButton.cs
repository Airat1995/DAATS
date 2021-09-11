using System;
using UnityEngine;
using UnityEngine.UI;

namespace DAATS.Initializer.UI
{
    [RequireComponent(typeof(Button))]
    public class LanguageButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private SystemLanguage _language;

        public Action<SystemLanguage> OnButtonPressAction = language => { };

        private void Start()
        {
            _button.onClick.AddListener(() => OnButtonPressAction(_language));
        }

        public void SubscribeOnClick(Action<SystemLanguage> clickEvent)
        {
            OnButtonPressAction += clickEvent;
        }
    }
}