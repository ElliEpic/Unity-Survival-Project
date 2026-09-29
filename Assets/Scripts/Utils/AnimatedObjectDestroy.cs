using UnityEngine;

public class AnimatedObjectDestroy : MonoBehaviour
{
  [SerializeField] private Animator animator;

  //Checks how long the animation is and then destroys it since it's attatch to the effect
    void Start()
    {
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
