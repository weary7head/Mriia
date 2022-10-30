using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("1");
    }

    public void ToSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}