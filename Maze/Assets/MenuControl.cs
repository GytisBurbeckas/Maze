using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;    
        SceneManager.LoadScene("Level1");     
            
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
