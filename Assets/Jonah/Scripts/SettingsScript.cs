using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsScript : MonoBehaviour
{

    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private AudioMixMode MixMode;

    public void OnChangeSlider(float Value)
    {
        switch (MixMode)
        {
            case AudioMixMode.LogrithmicMixerVolume:
                Mixer.SetFloat("volume", Mathf.Log10(Value) * 20);
                break;
        }
    }
    

    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
    
    
    
    /*
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    Mathf.Log10(volume)*20*/
}
