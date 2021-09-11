namespace DAATS.Initializer.Mangers.Sound.Interface
{
    public interface ISoundManager
    {
        void PlayMusic(string name);
        void PlaySound(string name);

        void StopMusic();
        void StopSound();

        void SetMusicVolume(float volume);
        void SetSoundVolume(float volume);

        void EnableMusic(bool enable);
        void EnableSound(bool enable);
    }
}