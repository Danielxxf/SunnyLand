using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    private void Start()
    {
        //GameObject.Find("Canvas/Pause").SetActive(true);
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void PauseGame()
    {
        GameObject.Find("Canvas/PauseMenu/Panel").SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        GameObject.Find("Canvas/PauseMenu/Panel").SetActive(false);
        Time.timeScale = 1;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume",value);
    }
}
