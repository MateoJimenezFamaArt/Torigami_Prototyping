using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    public string animationTrigger;

    [Header("Timing")]
    public float startupTime;
    public float activeTime;
    public float recoveryTime;

    [Header("Combo")]
    public bool opensComboWindow;
    public float comboWindowDuration;
}
