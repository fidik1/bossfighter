using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Rigidbody2D rb;

    public delegate void PlayerEvent();
    public static PlayerEvent isDashed;

    private void FixedUpdate()
    {
        if (!player.isAlive)
            return;

        Movement();
    }

    private void Update()
    {
        if (!player.isAlive)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void Movement()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * player.speed * 10 * Time.fixedDeltaTime, rb.velocity.y);
    }

    private int currentJump;
    private void Jump()
    {
        if (player.isGrounded && currentJump == 0 || !player.isGrounded && currentJump < player.maxJumps)
        {
            currentJump++;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * player.jumpForce * 4, ForceMode2D.Impulse);
        }
    }

    #region Dash
    public bool dashOnCooldown { get; private set; }
    [field: SerializeField] public float dashTime { get; private set; }
    [SerializeField] private float cooldownDash;
    private void Dash()
    {
        if (!dashOnCooldown)
        {
            player.ChangeSpeed(player.speed * 4);
            StartCoroutine(DashCooldown());
            isDashed?.Invoke();
        }
    }

    private IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashTime);
        player.ChangeSpeed(player.speed / 4);
        yield return new WaitForSeconds(cooldownDash - dashTime);
        dashOnCooldown = false;
    }
    #endregion Dash

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            player.isGrounded = true;
            currentJump = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            player.isGrounded = false;
    }
}
