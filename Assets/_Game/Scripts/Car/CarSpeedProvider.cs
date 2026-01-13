using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarSpeedProvider : MonoBehaviour
{
    Rigidbody rb;
    void Awake() { rb = GetComponent<Rigidbody>(); }
    public float Speed => rb.velocity.magnitude;
}
