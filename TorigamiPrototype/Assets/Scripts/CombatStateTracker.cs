using UnityEngine;

public class CombatStateTracker : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugStates = false;

    public bool IsAttacking { get; private set; }
    public bool IsRecovering { get; private set; }
    public bool ComboWindowOpen { get; private set; }

    public bool IsStepping { get; private set; }

    public bool CanMove => !IsAttacking && !IsRecovering && !IsStepping;


    public bool CanAttack => !IsAttacking && !IsRecovering;
    public bool CanChainCombo => ComboWindowOpen;

    public void StartAttack()
    {
        IsAttacking = true;
        IsRecovering = false;
        ComboWindowOpen = false;

        CombatDebug.Log(debugStates, "State", "Attack started → IsAttacking = true");
    }

    public void OpenComboWindow()
    {
        ComboWindowOpen = true;
        CombatDebug.Log(debugStates, "State", "Combo Window OPEN");
    }

    public void CloseComboWindow()
    {
        ComboWindowOpen = false;
        CombatDebug.Log(debugStates, "State", "Combo Window CLOSED");
    }

    public void EndAttack()
    {
        IsAttacking = false;
        IsRecovering = true;

        CombatDebug.Log(debugStates, "State", "Attack ended → Recovery started");
    }

    public void EndRecovery()
    {
        IsRecovering = false;
        CombatDebug.Log(debugStates, "State", "Recovery ended → Back to Idle");
    }

    public void StartStep()
    {
        IsStepping = true;
        CombatDebug.Log(debugStates, "State", "Step started → IsStepping = true");
    }

    public void EndStep()
    {
        IsStepping = false;
        CombatDebug.Log(debugStates, "State", "Step ended → Back to Idle");
    }

}
