using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ToggleMusic(bool value)
    {
        if (value)
            audioMixer.SetFloat("Music", -80);
        else
            audioMixer.SetFloat("Music", 0);

    }

    public void ChangeMusicValue(float value)
    {
        var volume = Mathf.Lerp(-80, 0, value);
        audioMixer.SetFloat("Music", volume);

        PlayerPrefs.SetFloat("Music", value);
    }
    public void ChangeEffectsValue(float value)
    {
        var volume = Mathf.Lerp(-80, 0, value);
        audioMixer.SetFloat("Effects", volume);
        PlayerPrefs.SetFloat("Effects", value);

    }
    public void ChangeMasterSwitchValue(float value)
    {
        var volume = Mathf.Lerp(-80, 0, value);
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", value);

    }
}
