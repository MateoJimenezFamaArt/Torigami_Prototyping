using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Movement Data")]
public class MovementData : ScriptableObject
{
    [Header("Step")]
    public float stepDistance = 1f;
    public float stepDuration = 0.25f;
    public float recoveryTime = 0.1f;
}
