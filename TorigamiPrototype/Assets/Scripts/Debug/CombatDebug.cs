using UnityEngine;

public static class CombatDebug
{
    public static void Log(bool enabled, string source, string message)
    {
        if (!enabled) return;
        Debug.Log($"[COMBAT][{source}] {message}");
    }

    public static void Warning(bool enabled, string source, string message)
    {
        if (!enabled) return;
        Debug.LogWarning($"[COMBAT][{source}] {message}");
    }
}
