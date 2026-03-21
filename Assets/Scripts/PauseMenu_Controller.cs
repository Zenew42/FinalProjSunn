using UnityEngine;

public class PauseMenu_Controller : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
