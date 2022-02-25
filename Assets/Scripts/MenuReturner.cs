using UnityEngine;

public class MenuReturner : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<GoToScene>().LoadScene("Main Menu");
        }
    }
}
