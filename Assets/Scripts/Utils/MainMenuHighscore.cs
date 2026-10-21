using TMPro;
using UnityEngine;

public class MainMenuHighscore : MonoBehaviour
{
    [SerializeField] private TMP_Text HighScoreText;
    
    private int highscore;
  public void HighScoreMainMenu()
    {
        if(PlayerPrefs.HasKey("SavedHighScore"))
        {
            highscore = PlayerPrefs.GetInt("SavedHighScore");
            HighScoreText.text = "High Score: " + highscore.ToString();
        }
    }

     public void Awake()
     {
          HighScoreMainMenu();
          
     } 
}
