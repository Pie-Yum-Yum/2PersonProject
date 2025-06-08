using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    public Slider megaJumpSlider;
    [SerializeField] TMP_Text megaJumpText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject winUI;
    float elapsedTime;
    bool completed;

    void Awake()
    {
        Instance = this;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        completed = true;
        winUI.SetActive(true);
    }

    void Update()
    {
        if (megaJumpSlider.value >= 1f)
        {
            megaJumpText.color = Color.red;
        }
        else megaJumpText.color = Color.white;

        if (!completed)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }
}
