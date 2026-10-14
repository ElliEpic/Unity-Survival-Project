using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  public int attackLevel;
  public List<attackStats> stats;
  public Sprite attackImage;

  public void LevelUp()
    {
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

        //how many enemies to strike at that upgrade level.
    public int targetCount;

    public string description;
}
