using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public AudioMixer bgmMixer;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        List<string> resolutionOptions = new List<string>();
        int currentResoIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + " + " + resolutions[i].height);
            if (Screen.currentResolution.width == resolutions[i].width &&
                Screen.currentResolution.height == resolutions[i].height)
            {
                currentResoIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResoIndex;

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(float volume)
    {
        bgmMixer.SetFloat("mainVolume", volume);
    }

    public void ChangeFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeResolution(int index)
    {
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }
}
