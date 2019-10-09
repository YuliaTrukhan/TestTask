using UnityEngine;

public static class InputManager 
{
    public static float HorizontalAxos =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetAxis("Horizontal");
#endif

    public static float VerticalAxis =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetAxis("Vertical");
#endif

    public static bool Attack =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetButtonDown("Fire1");
#endif

    public static bool StrongAttack =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetButtonDown("Fire2");
#endif

    public static float MouseX =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetAxis("Mouse X");
#endif

    public static float MouseY =>
#if UNITY_EDITOR || UNITY_STANDALONE
        Input.GetAxis("Mouse Y");
#endif
}
