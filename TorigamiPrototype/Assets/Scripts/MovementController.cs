using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugMovement = false;
    [Header("Debug Gizmos")]
    [SerializeField] private bool debugStepGizmos = true;
    [SerializeField] private Color gizmoColor = Color.cyan;


    [Header("References")]
    [SerializeField] private CombatStateTracker state;
    [SerializeField] private AnimatorDriver animatorDriver;

    [Header("Movement Data")]
    [SerializeField] private MovementData movementData;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (!state.CanMove) return;

        if (Input.GetKeyDown(KeyCode.W))
            StartStep(Vector3.forward, "StepForward");

        if (Input.GetKeyDown(KeyCode.S))
            StartStep(Vector3.back, "StepBackward");

        if (Input.GetKeyDown(KeyCode.A))
            StartStep(Vector3.left, "StepLeft");

        if (Input.GetKeyDown(KeyCode.D))
            StartStep(Vector3.right, "StepRight");
    }

    void StartStep(Vector3 direction, string animationTrigger)
    {
        CombatDebug.Log(debugMovement, "Movement", $"Step input detected → {animationTrigger}");
        StartCoroutine(ExecuteStep(direction, animationTrigger));
    }

    IEnumerator ExecuteStep(Vector3 direction, string trigger)
    {
        state.StartStep();
        animatorDriver.PlayStep(trigger, debugMovement);

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + transform.TransformDirection(direction) * movementData.stepDistance;

        float elapsed = 0f;

        while (elapsed < movementData.stepDuration)
        {
            float t = elapsed / movementData.stepDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        CombatDebug.Log(debugMovement, "Movement", "Step completed → Recovery");

        yield return new WaitForSeconds(movementData.recoveryTime);

        state.EndStep();
    }

    void OnDrawGizmosSelected()
    {
        if (!debugStepGizmos || movementData == null) return;

        Gizmos.color = gizmoColor;

        Vector3 origin = transform.position;

        DrawStepGizmo(origin, transform.forward);
        DrawStepGizmo(origin, -transform.forward);
        DrawStepGizmo(origin, transform.right);
        DrawStepGizmo(origin, -transform.right);
    }

    void DrawStepGizmo(Vector3 start, Vector3 direction)
    {
        Vector3 end = start + direction.normalized * movementData.stepDistance;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }

}
