using UnityEngine;

public class DrunkToggle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.M;
    [Range(0f, 1f)] public float strength = 1f;
    public Camera targetCamera; // ãÛÇ»ÇÁ MainCamera ÇégÇ§

    DrunkCameraFX fx;
    bool isDrunk;

    void Start()
    {
        if (!targetCamera)
            targetCamera = Camera.main;

        fx = targetCamera.GetComponent<DrunkCameraFX>();
        if (!fx)
            fx = targetCamera.gameObject.AddComponent<DrunkCameraFX>();

        fx.strength = strength;
        fx.enabled = false; // ç≈èâÇÕOFF
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Debug.Log("M detected");
            isDrunk = !isDrunk;
            fx.enabled = isDrunk;
            Debug.Log("Drunk Mode: " + (isDrunk ? "ON" : "OFF"));
        }
    }
}
