using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RBody;
    public Animator Anim;

    public float JumpAmount;
    public float MoveForce;
    public float AnimationSpeedScaler;

    // The max velocity the player can achieve through button presses
    private const float MAX_VELOCITY_POS = 4f;
    private Vector2 movementDirection;
    float angle;

    private bool hasJump = true;
    private bool isOnGround = true;
    private int isFacingRight = 1;

    void Start ()
    {
        RBody = GetComponent<Rigidbody2D> ();
        Anim = GetComponent<Animator> ();
        movementDirection = new Vector2 (0, 1f);
    }

    private void Update ()
    {
        Anim.speed = Mathf.Min (RBody.velocity.magnitude * AnimationSpeedScaler, 1.7f);
      //  //Debug.Log (RBody.velocity);
    }

    void FixedUpdate ()
    {
        RaycastHit2D [] hits = new RaycastHit2D [2];
        int h = Physics2D.RaycastNonAlloc (transform.position, -Vector2.up, hits); //cast downwards
        if (h > 1)
        { //if we hit something do stuff
          // //Debug.Log (hits [1].normal);
            movementDirection = new Vector2 (hits [1].normal.y, -hits [1].normal.x); // Note y is flipped
                                                                                     //  angle = Mathf.Abs (Mathf.Atan2 (hits [1].normal.x, hits [1].normal.y) * Mathf.Rad2Deg); //get angle
                                                                                     ////Debug.Log ("Moving in: " + movementDirection);

        }

        if (Input.GetButton ("Jump") && hasJump)
        {
            RBody.AddForce (new Vector2 (0f, JumpAmount), ForceMode2D.Impulse);
            hasJump = false;
        }

        float horizontal = Input.GetAxisRaw ("Horizontal");

        // Moving Right.
        if (horizontal > 0 && isOnGround && RBody.velocity.x < MAX_VELOCITY_POS)
        {
            RBody.AddForce (movementDirection * MoveForce, ForceMode2D.Impulse);
            //Debug.Log ("Adding force: " + movementDirection * MoveForce);
            isFacingRight = 1;
        }

        // Moving left.
        else if (horizontal < 0 && isOnGround && RBody.velocity.x > MAX_VELOCITY_POS * -1)
        {
            RBody.AddForce (-movementDirection * MoveForce, ForceMode2D.Impulse);
            //Debug.Log ("Adding force: " + movementDirection * MoveForce);
            isFacingRight = -1;
        }

        if (RBody.velocity.x > 0)
            Anim.SetBool ("IsMovingRight", true);
        else
            Anim.SetBool ("IsMovingRight", false);


    }

    IEnumerator SporeBlastReset ()
    {
        yield return new WaitForSeconds (1f);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        //Debug.Log (collision); //Debug.Log ("Leaving ground ");
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            hasJump = true;
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
          //  isOnGround = false;
        }
    }
}