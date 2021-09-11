using System;
using UnityEngine;

namespace DAATS.UserData
{
    [Serializable]
    public struct UserSettingsData
    {
        public float MusicVolume;
        public float SoundVolume;

        public bool MusicEnabled;
        public bool SoundEnabled;

        public SystemLanguage Language;
    }
}