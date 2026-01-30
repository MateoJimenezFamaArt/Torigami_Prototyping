using UnityEngine;
using System.Collections;

public class DodgeController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugDodge = false;

    [Header("References")]
    [SerializeField] private CombatStateTracker state;
    [SerializeField] private AnimatorDriver animatorDriver;

    [Header("Dodge Data")]
    [SerializeField] private DodgeData dodgeData;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryDodge();
        }
    }

    void TryDodge()
    {
        if (!state.CanDodge)
        {
            CombatDebug.Warning(debugDodge, "Dodge", "Fold Dodge FAILED → State locked");
            return;
        }

        StartCoroutine(ExecuteDodge());
    }

    IEnumerator ExecuteDodge()
    {
        state.StartDodge();
        animatorDriver.PlayDodge(debugDodge);

        CombatDebug.Log(debugDodge, "Dodge", "Folding character");

        transform.localScale = dodgeData.foldedScale;

        yield return new WaitForSeconds(dodgeData.dodgeDuration);

        transform.localScale = originalScale;

        CombatDebug.Log(debugDodge, "Dodge", "Unfolding → Recovery");

        yield return new WaitForSeconds(dodgeData.recoveryTime);

        state.EndDodge();
    }
}
