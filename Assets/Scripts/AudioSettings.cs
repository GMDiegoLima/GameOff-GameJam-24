using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private KeyCode optionsKey = KeyCode.Escape;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject optionsPanel;

    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("master", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("music", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfx", 1f);

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }

    void Update()
    {
        if (Input.GetKeyDown(optionsKey))
        {
            if (optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                optionsButton.SetActive(true);
                ResumeGame();
            }
            else
            {
                optionsPanel.SetActive(true);
                optionsButton.SetActive(false);
                PauseGame();
            }

        }
    }

    void SetMasterVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("Master", value * 100f);
        PlayerPrefs.SetFloat("Master", value);
    }

    void SetMusicVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("Music", value * 100f);
        PlayerPrefs.SetFloat("Music", value);
    }

    void SetSFXVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("SFX", value * 100f);
        PlayerPrefs.SetFloat("SFX", value);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("game paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("game unpaused");
    }
}
