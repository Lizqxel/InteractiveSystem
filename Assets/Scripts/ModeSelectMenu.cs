using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectMenu : MonoBehaviour
{
    [SerializeField] string gameSceneName = "Game"; // Ç†Ç»ÇΩÇÃÉQÅ[ÉÄÉVÅ[ÉìñºÇ…çáÇÌÇπÇÈ

    public void OnNormal()
    {
        GameMode.Current = GameMode.Mode.Normal;
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnDrunk()
    {
        GameMode.Current = GameMode.Mode.Drunk;
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
