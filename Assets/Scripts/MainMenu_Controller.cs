using UnityEngine;
using UnityEngine.InputSystem.Editor;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    public void StartGame ()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
