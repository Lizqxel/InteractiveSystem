using UnityEngine;

public class DrunkCameraFX : MonoBehaviour
{
    [Range(0f, 1f)] public float strength = 1f;

    Vector3 initialLocalPos;
    Quaternion initialLocalRot;
    float initialFov;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        initialLocalPos = transform.localPosition;
        initialLocalRot = transform.localRotation;
        if (cam != null) initialFov = cam.fieldOfView;
    }

    void LateUpdate()
    {
        float t = Time.time;

        // Ç‰Ç¡Ç≠ÇËïsãKë•Ç…óhÇÍÇÈ
        float nx = (Mathf.PerlinNoise(t * 0.35f, 1.1f) - 0.5f) * 2f;
        float ny = (Mathf.PerlinNoise(t * 0.35f, 2.2f) - 0.5f) * 2f;

        float posAmp = Mathf.Lerp(0f, 0.15f, strength);
        transform.localPosition = initialLocalPos + new Vector3(nx, ny * 0.6f, 0f) * posAmp;

        // ÉçÅ[ÉãÅiåXÇ´Åj
        float roll = Mathf.Sin(t * 0.7f) * Mathf.Lerp(0f, 6f, strength);
        transform.localRotation = initialLocalRot * Quaternion.Euler(0f, 0f, roll);

        // FOVÇ‰ÇÁÇ¨
        if (cam != null)
        {
            float wobble = Mathf.Sin(t * 0.9f) * 4f + (Mathf.PerlinNoise(t * 0.25f, 3.3f) - 0.5f) * 6f;
            cam.fieldOfView = initialFov + wobble * strength;
        }
    }

    void OnDisable()
    {
        transform.localPosition = initialLocalPos;
        transform.localRotation = initialLocalRot;
        if (cam != null) cam.fieldOfView = initialFov;
    }
}
