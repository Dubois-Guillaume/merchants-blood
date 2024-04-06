using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        VerticalMovePlayer(verticalMovement);
        HorizontalMovePlayer(horizontalMovement);
    }

    void VerticalMovePlayer(float _verticalMovement)
    {
        rb.velocity = new Vector2(_verticalMovement, rb.velocity.y);
    }

    void HorizontalMovePlayer(float _horizontalMovement)
    {
        rb.velocity = new Vector2(_horizontalMovement, rb.velocity.x);
    }
}

