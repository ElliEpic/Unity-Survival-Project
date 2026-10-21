
using UnityEngine;

public static class PlayerUpgradeApplier
{
    public static bool Apply(
        PlayerUpgradeData upgrade,
        Attack targetAttack = null)
    {
        if (upgrade == null)
        {
            Debug.LogWarning("Player upgrade is missing.");
            return false;
        }

        switch (upgrade.upgradeType)
        {
            case PlayerUpgradeType.PermanentDamageBonus:
                Attack.AddPermanentDamageBonus(upgrade.amount);
                return true;

            case PlayerUpgradeType.AttackDamageBonus:
                if (targetAttack == null ||
                    targetAttack.stats == null)
                {
                    Debug.LogWarning(
                        "This upgrade requires a target Attack.");
                    return false;
                }

               //  foreach (attackStats level in targetAttack.stats)
               //  {
               //      if (level != null)
               //      {
               //          level.damage += upgrade.amount;
               //      }
               //  }

                return true;

            default:
                return false;
        }
    }
}
