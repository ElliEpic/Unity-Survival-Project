
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public TMP_Text attackName;
    public TMP_Text attackDescription;
    public Image attackIcon;
    public PlayerUpgradeData assignedPlayerUpgrade;

    public Attack assignedAttack;
public void ActivateButton(Attack attack)
{
    assignedAttack = attack;
    assignedPlayerUpgrade = null;

    if (attack == null)
        return;

    attackName.text = attack.name;
    attackDescription.text = attack.stats.description;
    attackIcon.sprite = attack.attackImage;
}
    public void ActivateButton(PlayerUpgradeData upgrade)
{
    assignedAttack = null;
    assignedPlayerUpgrade = upgrade;

    if (upgrade == null)
        return;

    attackName.text = upgrade.upgradeName;
    attackDescription.text = upgrade.description;
    attackIcon.sprite = upgrade.icon;
}

    public void SelectUpgrade()
{
    if (assignedPlayerUpgrade != null)
    {
        PlayerMovement.Instance.ApplyPlayerUpgrade(
            assignedPlayerUpgrade);
    }
    else if (assignedAttack != null)
    {
        assignedAttack.LevelUp();
    }
    else
    {
        return;
    }

    UIController.Instance.LevelUpPanelClose();
}
}
