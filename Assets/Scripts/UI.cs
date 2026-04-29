using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    public static UI instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
       timerText.text = Time.time.ToString("F2") + "s";
    }

    public void EnableGameOverUI()
    {
        Time.timeScale = .5f; //// slow down(stops)
        gameOverUI.SetActive(true);
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }



}


