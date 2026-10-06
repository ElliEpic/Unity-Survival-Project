using UnityEngine; 
using UnityEngine.SceneManagement; 
 
public class GameManager : MonoBehaviour 
{ 
    public static GameManager Instance; 
    public float gameTime; 
    public bool gameActive;
 
     void Awake() 
        { 
            if (Instance != null && Instance != this){ 
                Destroy(this); 
            } else { 
                Instance = this; 
            } 
             
        } 

        void Start()
    {
        gameActive = true;
    }
 
    void Update() 
    { 
        if(gameActive)
        {        //Everytime we call this, the timetext updates
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            { 
                Pause(); 
            }
    
        } 
    } 

    public void GameOver() 
    { 
        gameActive = false; 
        UIController.Instance.gameOverPanel.SetActive(true); 
    } 
 
    public void Restart() 
    { 
        SceneManager.LoadScene("SampleScene"); 
    } 
    public void Pause() 
    { 
        if(UIController.Instance.pauseMenu.activeSelf == false && UIController.Instance.gameOverPanel.activeSelf == false) 
        { 
            UIController.Instance.pauseMenu.SetActive(true); 
            Time.timeScale = 0f; 
        } else 
        { 
            UIController.Instance.pauseMenu.SetActive(false); 
            Time.timeScale = 1f; 
        } 
    } 
}