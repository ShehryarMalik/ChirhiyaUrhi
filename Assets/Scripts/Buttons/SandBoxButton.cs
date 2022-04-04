using UnityEngine;
using UnityEngine.SceneManagement;

public class SandBoxButton : MonoBehaviour
{
    public void loadSandBoxScene()
    {
        SceneManager.LoadScene("SandBoxScene");
    }
}
