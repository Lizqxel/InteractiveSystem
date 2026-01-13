using UnityEngine;
using TMPro;

public class ParkingUI : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text hintText;
    public GameObject resultPanel;
    public TMP_Text resultTitleText;
    public TMP_Text resultTimeText;

    float startTime;
    bool running = true;

    void Start()
    {
        startTime = Time.time;
        if (resultPanel) resultPanel.SetActive(false);
        if (hintText) hintText.text = "";
    }

    void Update()
    {
        if (!running) return;
        float t = Time.time - startTime;
        if (timerText) timerText.text = $"TIME {t:0.00}s";
    }

    public float CurrentTime => Time.time - startTime;

    public void SetHint(string msg)
    {
        if (hintText) hintText.text = msg;
    }

    public void ClearHint()
    {
        if (hintText) hintText.text = "";
    }

    public void ShowResult(float timeSec)
    {
        running = false;
        if (resultPanel) resultPanel.SetActive(true);
        if (resultTitleText) resultTitleText.text = "Clear!";
        if (resultTimeText) resultTimeText.text = $"TIME  {timeSec:0.00}s";
    }
}
