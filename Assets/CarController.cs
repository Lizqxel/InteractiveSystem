using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;      // 前後のスピード
    public float turnSpeed = 60f;     // 左右回転の速さ

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // キーボード入力を取得（↑↓→←キー）
        float move = Input.GetAxis("Vertical");   // 前後
        float turn = Input.GetAxis("Horizontal"); // 左右

        // 前後移動（物理で動かす）
        Vector3 forwardMove = transform.forward * move * moveSpeed;
        rb.MovePosition(rb.position + forwardMove * Time.fixedDeltaTime);

        // 左右回転
        Quaternion turnRotation = Quaternion.Euler(0f, turn * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
