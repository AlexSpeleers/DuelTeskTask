using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : MonoBehaviour
{
    public void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void LoadNextOrFirstScene() 
    {
        if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(0);
    }
}
