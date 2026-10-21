using UnityEngine;

public class AttackStats
{
    
     public float cooldown = 3;
     public float duration = 3f;
     public float damage = 3f;
     public float range = 3f;
     public float speed = 3f;
     public int targetCount = 3; //how many enemies to strike at that upgrade level.
     public float damageBuff = 0;
     public string description;
    


    public void MultiplyStats(float multiplier)
     {

          multiplier = multiplier * 0.7f;
          cooldown -= 0.3f;
          duration =  duration * multiplier;
          damage = damage * multiplier ;
          range = damage * multiplier;
          speed = damage * multiplier;
          targetCount += 1; //how many enemies to strike at that upgrade level.
          
     }

}
