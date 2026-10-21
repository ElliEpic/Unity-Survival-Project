using UnityEngine;


public class NastyPlot : Attack
{
  [Header("Nasty Plot")]
    [SerializeField] private GameObject nastyPlotPrefab;
    [SerializeField] private float nastyPlotLifetime = 1.5f;

    [SerializeField] AreaAttack attackToIncrease;
    

    [Header("Nasty Plot Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip nastyPlotSound;




    private void Awake()
    {


        
   

        // Wait for the initial cooldown before the first cast.
        
    }

   



    public override void LevelUp()
{
    // Make sure the target attack exists.
    stats.MultiplyStats(attackLevel);
    // Show the buff effect and play the sound.
    UseNastyPlot();
}

public void UseNastyPlot()
{
    // Spawn the effect at the player's position.
    if (nastyPlotPrefab != null)
    {
        Vector3 effectOffset = new Vector3(0.3f, 3.2f, 0f);

        GameObject effect = Instantiate(
            nastyPlotPrefab,
            transform.position + effectOffset,
            Quaternion.identity,
            transform
        );
        //effect.transform.SetParent(transform, true);

        // Remove the visual effect after 1.5 seconds.
        Destroy(effect, nastyPlotLifetime);
    }

    // Play the sound once when Nasty Plot is selected.
    if (audioSource != null && nastyPlotSound != null)
    {
        audioSource.PlayOneShot(nastyPlotSound);
    }
}


}

     

    
       
    

      


