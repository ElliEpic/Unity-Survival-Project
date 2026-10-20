using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackLevel;
    public List<attackStats> stats;
    public Sprite attackImage;

    public static int PermanentDamageBonus { get; private set; } = 0;

    public static void AddPermanentDamageBonus(int amount)
    {
        PermanentDamageBonus += amount;
    }

    public virtual void LevelUp()
    {
        if(stats == null || attackLevel >= stats.Count - 1)
        {
            return;
        }
        
        attackLevel++;
    }

}

[System.Serializable]
public class attackStats
{
    public float cooldown;
    public float duration;
    public float damage;
    public float range;
    public float speed;
    public int targetCount; //how many enemies to strike at that upgrade level.
    public float damageBuff = 0;
    public string description;
}
