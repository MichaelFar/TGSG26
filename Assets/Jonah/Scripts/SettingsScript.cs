using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    //calling mouse look script for mouse sensitivity variable
    public MoveCamera cameraSpeed;
    public Slider sensitivitySlider;

    //stuff for audio
    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private AudioMixMode MixMode;

    //audio slider settings
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
        LogrithmicMixerVolume
    }

    //mouse speed settings
    public void Start()
    {
        cameraSpeed.mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100);
        sensitivitySlider.value = cameraSpeed.mouseSensitivity / 10;
    }


    public void Update()
    {
        PlayerPrefs.SetFloat("currentSensitivity", cameraSpeed.mouseSensitivity);
    }

    
   public void AdjustSpeed(float newSpeed)
     {
        cameraSpeed.mouseSensitivity = newSpeed * 10;
     }






    /*
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    Mathf.Log10(volume)*20*/
}
