using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private Slider PlayerExperienceSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text experienceText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text highscoreGameOverText;
    [SerializeField] private TMP_Text finalScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    public GameObject levelUpPanel;
    [SerializeField] private TMP_Text timerText;
    public LevelUpButton[] levelUpButtons;
    
    public int currentScore;
    
        void Awake()
        {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }
            
        }

 public void UpdateScore(int points)
{
    currentScore += points;
    scoreText.text = "Score: " + currentScore;

    // Update the saved high score if beaten
    UpdateHighscore();
}

public void UpdateHighscore()
{
    int savedHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);

    if (currentScore > savedHighScore)
    {
        savedHighScore = currentScore;

        PlayerPrefs.SetInt("SavedHighScore", savedHighScore);
        PlayerPrefs.Save();
    }

    
    if (highscoreText != null)
    {
        highscoreText.text = "High Score: " + savedHighScore;
    }

    // Show the final score if this text is assigned
    if (finalScoreText != null)
    {
        finalScoreText.text = "Final Score: " + currentScore;
    }
       if (highscoreGameOverText != null)
    {
        highscoreGameOverText.text = "High Score: " + savedHighScore;
    }
}

void Start()
{
    // Display the saved high score as soon as the scene loads
    UpdateHighscore();
}
     public void UpdateHealthSlider()
    {
        PlayerHealthSlider.maxValue = PlayerMovement.Instance.playerMaxHealth;
        PlayerHealthSlider.value = PlayerMovement.Instance.playerHealth;
        healthText.text = PlayerHealthSlider.value + " / " + PlayerHealthSlider.maxValue;
    }
    public void UpdateExperienceSlider()
    {
        PlayerExperienceSlider.maxValue = PlayerMovement.Instance.playerLevels[PlayerMovement.Instance.currentevel - 1];
        PlayerExperienceSlider.value = PlayerMovement.Instance.experience;
        experienceText.text = PlayerExperienceSlider.value + " / " + PlayerExperienceSlider.maxValue;
    }

    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);
        timerText.text = min + ":" + sec.ToString("00");
    }
    public void LevelUpPanelOpen()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }
     public void LevelUpPanelClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
