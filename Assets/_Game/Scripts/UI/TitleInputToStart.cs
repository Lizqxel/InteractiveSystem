using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleInputToStart : MonoBehaviour
{
    public string nextSceneName = "Parkgarage_demo-iwa";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
