using UnityEngine;

public class AnimatorDriver : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugAnimator = false;

    [SerializeField] private Animator animator;

    public void PlayAttack(string triggerName)
    {
        animator.SetTrigger(triggerName);
        CombatDebug.Log(debugAnimator, "Animator", $"Trigger fired → {triggerName}");
    }

    public void PlayStep(string triggerName, bool debug)
    {
        animator.SetTrigger(triggerName);
        CombatDebug.Log(debug, "Animator", $"Step Trigger fired → {triggerName}");
    }

}
