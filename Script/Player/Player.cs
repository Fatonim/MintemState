using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isSlide = false;
    private bool flipRight = false;
    private int kolTriggerIce, kolTriggerWater;
    private Vector2 MoveVelocity;
    private Vector2 movementInput;

    public float speed;
    public Vector3 difference;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public Rigidbody2D rb;
    public Joystick joystick;
    public rot_Weapons hand;
    public GameObject foots;
    public Animator anim;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private void Update()
    {
        movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        anim.SetBool("isRunning", isRun());

        if (joystick.Horizontal < 0 && !flipRight) Flip();
        else if (joystick.Horizontal > 0 && flipRight) Flip();

        if (isSlide)
        {
            if ((rb.velocity + movementInput * speed * 0.4f).magnitude <= 4) rb.AddForce(movementInput * speed * 0.4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            if (kolTriggerWater == 0)
            {
            }
            speed = 1;
            kolTriggerWater++;
        }
        else if (collision.tag == "Ice")
        {
            isSlide = true;
            kolTriggerIce++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            kolTriggerWater--;
            if (kolTriggerWater == 0)
            {
                speed = 2.5f;
            }
        }
        else if (collision.tag == "Ice")
        {
            kolTriggerIce--;
            if (kolTriggerIce == 0)
            {
                isSlide = false;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Flip()
    {
        flipRight = !flipRight;
        Vector3 Scaler_p = transform.localScale;
        Scaler_p.x *= -1;
        transform.localScale = Scaler_p;
        offsetAngle();
    }

    private void Move()
    {
        if (movementInput != Vector2.zero && !isSlide)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            speed * Time.deltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            return true;
        }
        else
            return false;
    }

    private bool isRun()
    {
        if (movementInput != new Vector2(0, 0))
        {
            difference = movementInput * speed;
            return true;
        }
        else return false;
    }

    public void offsetAngle()
    {
        Vector3 Scaler_p = transform.localScale;
        if (Scaler_p.x == 1) hand.offset = hand.offset1 - hand.offsetAngle;
        else hand.offset = hand.offset2 + hand.offsetAngle;
    }
}
