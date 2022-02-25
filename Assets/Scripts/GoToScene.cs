using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
