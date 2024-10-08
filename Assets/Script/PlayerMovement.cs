using System;
using System.Drawing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector3 velocity = Vector3.zero;

    public float playerX;
    public float playerY;

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
        animator.SetFloat("SpeedY", rb.velocity.y);
    }

    void HorizontalMovePlayer(float _horizontalMovement)
    {
        rb.velocity = new Vector2(_horizontalMovement, rb.velocity.x);
        animator.SetFloat("SpeedX", rb.velocity.x);
    }

    void GetPlayerCoords()
    {
        this.playerX = this.transform.position.x;
        this.playerY = this.transform.position.y;

    }
}

