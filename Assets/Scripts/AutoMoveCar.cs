using UnityEngine;

public class AutoMoveCar : MonoBehaviour
{
    public float speed = 5f;
    public float resetZ = -50f;
    public float startZ = 50f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z < resetZ)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                startZ
            );
        }
    }
}
