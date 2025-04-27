using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource effects;

    public void ToggleMusic()
    {
        backgroundMusic.mute = !backgroundMusic.mute;
    }

    public void ToggleEffects()
    {
        effects.mute = !effects.mute;
    }
}
