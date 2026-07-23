using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsScript : BaseUI
{
    //calling mouse look script for mouse sensitivity variable
    public MoveCamera cameraSpeed;
    public Slider sensitivitySlider;
    /*
    private float _Sensitivity;
    public float Sensitivity { get { return _Sensitivity; } set { _Sensitivity = value; } }
    */
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

        cameraSpeed.mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity");
        sensitivitySlider.value = cameraSpeed.mouseSensitivity / 10;
        print("options applied");
    }





    public void AdjustSpeed(float newSpeed)
    {
        PlayerPrefs.SetFloat("currentSensitivity", cameraSpeed.mouseSensitivity);
        PlayerPrefs.Save();
        cameraSpeed.mouseSensitivity = newSpeed * 10;
        if (PlayerPrefs.HasKey("currentSensitivity"))
        {
            print(cameraSpeed.mouseSensitivity);
        }
    }




    /*
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    Mathf.Log10(volume)*20
    */
}
