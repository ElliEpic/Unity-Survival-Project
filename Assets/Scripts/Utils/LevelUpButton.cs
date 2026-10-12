using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public TMP_Text attackName;
    public TMP_Text attackDescription;
    public Image attackIcon;

    private Attack assignedAttack;

    public void ActivateButton(Attack attack)
    {
        attackName.text = attack.name;
        attackDescription.text = attack.stats[attack.attackLevel].description;
        attackIcon.sprite = attack.attackImage;

        assignedAttack = attack;
    }

    public void SelectUpgrade()
    {
        assignedAttack.LevelUp();
        UIController.Instance.LevelUpPanelClose();
       
    }
}