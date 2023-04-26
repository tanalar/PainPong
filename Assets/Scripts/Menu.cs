using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private UnityEngine.UI.Button pauseButton;
    [SerializeField] private TextMeshProUGUI winnerInfo;
    [SerializeField] private UnityEngine.UI.Image volumeIcon;
    [SerializeField] private UnityEngine.UI.Image underlayVolumeIcon;
    [SerializeField] private UnityEngine.UI.Image crossIcon;
    [SerializeField] private UnityEngine.UI.Image underlayCrossIcon;
    [SerializeField] private GameObject pauseUnderlay;

    public static Action onButtonSound;
    public static Action onSetSound;
    public static Action onSetMusic;
    public static Action<bool> onPause;

    private void Start()
    {
        SetSoundIcon();
        SetMusicIcon();
    }

    private void OnEnable()
    {
        Player.onLoose += DeathScreen;
    }
    private void OnDisable()
    {
        Player.onLoose -= DeathScreen;
    }

    public void Singleplayer()
    {
        onButtonSound?.Invoke();
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        onButtonSound?.Invoke();
        SceneManager.LoadScene(2);
    }

    public void SetSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            onButtonSound?.Invoke();
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        SetSoundIcon();
        onSetSound?.Invoke();
    }
    private void SetSoundIcon()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            if(volumeIcon != null && underlayVolumeIcon != null)
            {
                volumeIcon.enabled = true;
                underlayVolumeIcon.enabled = true;
            }
        }
        else
        {
            if (volumeIcon != null && underlayVolumeIcon != null)
            {
                volumeIcon.enabled = false;
                underlayVolumeIcon.enabled = false;
            }
        }
    }

    public void SetMusic()
    {
        if (PlayerPrefs.GetFloat("Music") == 0.5f)
        {
            onButtonSound?.Invoke();
            PlayerPrefs.SetFloat("Music", 0);
        }
        else
        {
            onButtonSound?.Invoke();
            PlayerPrefs.SetFloat("Music", 0.5f);
        }
        SetMusicIcon();
        onSetMusic?.Invoke();
    }
    private void SetMusicIcon()
    {
        if (PlayerPrefs.GetFloat("Music") == 0.5f)
        {
            if (volumeIcon != null && underlayVolumeIcon != null)
            {
                crossIcon.enabled = false;
                underlayCrossIcon.enabled = false;
            }
        }
        else
        {
            if (volumeIcon != null && underlayVolumeIcon != null)
            {
                crossIcon.enabled = true;
                underlayCrossIcon.enabled = true;
            }
        }
    }

    private void DeathScreen(string winner)
    {
        if(deathScreen.activeInHierarchy == false)
        {
            pauseButton.interactable = false;
            deathScreen.SetActive(true);
            winnerInfo.text = $"{winner} LOOSE!";
        }
    }

    public void MainMenu()
    {
        onButtonSound?.Invoke();
        bool pause = false;
        onPause?.Invoke(pause);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        onButtonSound?.Invoke();
        bool pause = false;
        onPause?.Invoke(pause);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseMenu()
    {
        pauseButton.interactable = false;
        pauseUnderlay.SetActive(false);
        onButtonSound?.Invoke();
        bool pause = true;
        onPause?.Invoke(pause);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseButton.interactable = true;
        pauseUnderlay.SetActive(true);
        onButtonSound?.Invoke();
        bool pause = false;
        onPause?.Invoke(pause);
        pauseMenu.SetActive(false);
    }
}
