using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private TMP_Text healthText;
    void Awake()
        {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }
            
        }

    public void UpdateHealthSlider()
    {
        PlayerHealthSlider.maxValue = PlayerMovement.Instance.playerMaxHealth;
        PlayerHealthSlider.value = PlayerMovement.Instance.playerHealth;
        healthText.text = PlayerHealthSlider.value + " / " + PlayerHealthSlider.maxValue;
    }
}
