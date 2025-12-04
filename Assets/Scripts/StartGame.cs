using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PlayStart()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
