using UnityEngine;

public class ParkingArea : MonoBehaviour
{
    [Header("References")]
    public CarSpeedProvider car;   // 車Root（CarSpeedProviderが付いてるやつ）
    public ParkingUI ui;

    [Header("Stop detection")]
    public float stopSpeedThreshold = 0.25f; // m/s（少し緩め推奨）
    public float requiredStopTime = 1.0f;    // seconds

    [Header("Full-inside check")]
    public Collider parkingTrigger;          // 駐車枠のTrigger（自分のColliderでOK）
    public Collider carMainCollider;         // 車のメインCollider（BoxCollider推奨）
    public float insideEpsilon = 0.02f;      // ちょい余裕（めり込み/誤差対策）

    [Header("Debug")]
    public bool debugLogs = false;

    float stillTimer = 0f;
    bool completed = false;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
        parkingTrigger = col;
    }

    void Start()
    {
        if (parkingTrigger == null) parkingTrigger = GetComponent<Collider>();

        if (car != null && carMainCollider == null)
        {
            // 車Root/子から「一番それっぽい」Colliderを拾う（手動設定推奨）
            carMainCollider = car.GetComponentInChildren<Collider>();
        }
    }

    void Update()
    {
        if (completed) return;
        if (car == null || ui == null || parkingTrigger == null || carMainCollider == null) return;

        bool fullyInside = IsFullyInside(parkingTrigger.bounds, carMainCollider.bounds, insideEpsilon);

        if (!fullyInside)
        {
            stillTimer = 0f;
            ui.SetHint("Please enter the parking space completely");
            return;
        }

        // ここから「完全に入ってる」状態
        float speed = car.Speed;

        if (speed <= stopSpeedThreshold)
        {
            stillTimer += Time.deltaTime;
            float remain = Mathf.Max(0f, requiredStopTime - stillTimer);
            ui.SetHint($"Parking… {remain:0.0}s");

            if (stillTimer >= requiredStopTime)
            {
                completed = true;
                ui.ClearHint();
                ui.ShowResult(ui.CurrentTime);
                if (debugLogs) Debug.Log("[ParkingArea] COMPLETE");
            }
        }
        else
        {
            stillTimer = 0f;
            ui.SetHint("Please park within the parking space");
        }
    }

    // === Bounds完全内判定 ===
    static bool IsFullyInside(Bounds outer, Bounds inner, float eps)
    {
        // innerの8頂点が outer の中に全部入ってるか
        Vector3 c = inner.center;
        Vector3 e = inner.extents;

        // 誤差対策で少し縮める（innerを小さくする）
        e = new Vector3(
            Mathf.Max(0f, e.x - eps),
            Mathf.Max(0f, e.y - eps),
            Mathf.Max(0f, e.z - eps)
        );

        Vector3[] corners = new Vector3[8]
        {
            c + new Vector3( e.x,  e.y,  e.z),
            c + new Vector3( e.x,  e.y, -e.z),
            c + new Vector3( e.x, -e.y,  e.z),
            c + new Vector3( e.x, -e.y, -e.z),
            c + new Vector3(-e.x,  e.y,  e.z),
            c + new Vector3(-e.x,  e.y, -e.z),
            c + new Vector3(-e.x, -e.y,  e.z),
            c + new Vector3(-e.x, -e.y, -e.z),
        };

        for (int i = 0; i < corners.Length; i++)
        {
            if (!outer.Contains(corners[i])) return false;
        }
        return true;
    }
}
