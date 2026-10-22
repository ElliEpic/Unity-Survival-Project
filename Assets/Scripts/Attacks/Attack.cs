using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackLevel;


    public AttackStats stats;
    public Sprite attackImage;

    public static int PermanentDamageBonus { get; private set; } = 0;


    public void Awake()
     {
          stats = new AttackStats();
     }
     public static void AddPermanentDamageBonus(int amount)
    {
        PermanentDamageBonus += amount;
    }

    public virtual void LevelUp()
    {

        attackLevel += 1f;
        stats.MultiplyStats(attackLevel);
    }

}


