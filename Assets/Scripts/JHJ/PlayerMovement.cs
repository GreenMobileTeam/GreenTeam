using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid_;

    public float power = 10;

    private void Start()
    {
        rigid_ = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        rigid_.velocity = new Vector3(H, 0, V) * power;
    }
}
