using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TransitionLoader transition;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;

    private Resolution[] resolutions;

    public static MainMenu instanceMainMenu;

    void Awake()
    {
        instanceMainMenu = this;
    }

    void Start()
    {
        StartQuality();
        GetResolutions();
    }

    public void PlayGame()
    {
        StartCoroutine(transition.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeMaster", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void StartQuality()
    {
        QualitySettings.SetQualityLevel(2);
        qualityDropdown.value = 2;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void GetResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int index = 0; index < resolutions.Length; index++)
        {
            string option = resolutions[index].width + " X " + resolutions[index].height;
            options.Add(option);

            if (resolutions[index].width == Screen.currentResolution.width &&
                resolutions[index].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = index;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}