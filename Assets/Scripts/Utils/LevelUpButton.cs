
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public TMP_Text attackName;
    public TMP_Text attackDescription;
    public Image attackIcon;

    public Attack assignedAttack;

    public void ActivateButton(Attack attack)
    {
        assignedAttack = attack;

        if (assignedAttack == null)
        {
            Debug.LogWarning("No attack assigned to this button!");
            return;
        }

        attackName.text = assignedAttack.name;
        attackDescription.text =
            assignedAttack.stats[assignedAttack.attackLevel].description;

        attackIcon.sprite = assignedAttack.attackImage;
    }

    public void SelectUpgrade()
    {
        
        if (assignedAttack == null)
        {
            
            Debug.LogWarning("No upgrade was assigned to this button!");
            return;
            
        }

        assignedAttack.LevelUp();

        UIController.Instance.LevelUpPanelClose();
    }
}
