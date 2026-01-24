using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void PlayGame1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void PlayGame2(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayGame3(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }


    public void Info()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void SecondMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void BackMain(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }
}
