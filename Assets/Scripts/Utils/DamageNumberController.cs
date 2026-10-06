using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance;
  public DamageNumber prefab;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);

        } else
        {
            Instance = this;
        }
    }

    public void CreateNumber(float value, Vector3 location)
    {
        Instantiate(prefab, location, transform.rotation, transform);
    }
  
}
