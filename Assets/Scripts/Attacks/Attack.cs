using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackLevel;


    public PlayerStats stats;
    public Sprite attackImage;

    public static int PermanentDamageBonus { get; private set; } = 0;


     void Awake()
     {
          stats = new PlayerStats();
     }
     public static void AddPermanentDamageBonus(int amount)
    {
        PermanentDamageBonus += amount;
    }

    public virtual void LevelUp()
    {

        attackLevel++;
        stats.MultiplyStats(attackLevel);
    }

}


