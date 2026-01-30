using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugAttacks = false;

    [Header("References")]
    [SerializeField] private CombatStateTracker state;
    [SerializeField] private AnimatorDriver animatorDriver;

    [Header("Attacks")]
    [SerializeField] private AttackData lightJab;
    [SerializeField] private AttackData heavyCross;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CombatDebug.Log(debugAttacks, "Input", "Light attack input received");
            TryLightAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            CombatDebug.Log(debugAttacks, "Input", "Heavy attack input received");
            TryHeavyAttack();
        }
    }

    void TryLightAttack()
    {
        if (!state.CanAttack)
        {
            CombatDebug.Warning(debugAttacks, "Attack", "Light Jab FAILED → Cannot attack in current state");
            return;
        }

        CombatDebug.Log(debugAttacks, "Attack", "Light Jab SUCCESS → Executing");
        StartCoroutine(ExecuteAttack(lightJab));
    }

    void TryHeavyAttack()
    {
        if (!state.CanChainCombo)
        {
            CombatDebug.Warning(debugAttacks, "Attack", "Heavy Cross FAILED → Combo window not open");
            return;
        }

        CombatDebug.Log(debugAttacks, "Attack", "Heavy Cross SUCCESS → Combo chained");
        state.CloseComboWindow();
        StartCoroutine(ExecuteAttack(heavyCross));
    }

    IEnumerator ExecuteAttack(AttackData attack)
    {
        CombatDebug.Log(debugAttacks, "Attack", $"Attack START → {attack.name}");

        state.StartAttack();
        animatorDriver.PlayAttack(attack.animationTrigger);

        CombatDebug.Log(debugAttacks, "Animator", $"Trigger SET → {attack.animationTrigger}");

        yield return new WaitForSeconds(attack.startupTime);

        CombatDebug.Log(debugAttacks, "Attack", $"Active Frames → {attack.activeTime}s");

        yield return new WaitForSeconds(attack.activeTime);

        if (attack.opensComboWindow)
        {
            CombatDebug.Log(debugAttacks, "Combo", $"Combo Window OPEN → {attack.comboWindowDuration}s");
            state.OpenComboWindow();

            yield return new WaitForSeconds(attack.comboWindowDuration);

            state.CloseComboWindow();
            CombatDebug.Log(debugAttacks, "Combo", "Combo Window EXPIRED");
        }

        state.EndAttack();
        CombatDebug.Log(debugAttacks, "Attack", "Attack finished → Recovery phase");

        yield return new WaitForSeconds(attack.recoveryTime);

        state.EndRecovery();
        CombatDebug.Log(debugAttacks, "Attack", "Recovery finished → Attack cycle COMPLETE");
    }
}
