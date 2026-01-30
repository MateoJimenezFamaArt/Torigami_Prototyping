using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Dodge Data")]
public class DodgeData : ScriptableObject
{
    public float dodgeDuration = 0.4f;
    public float recoveryTime = 0.3f;

    [Header("Fold")]
    public Vector3 foldedScale = new Vector3(1f, 0.2f, 1f);
}
