using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RBody;
    public Animator Anim;
    public GameManager Gm;

    public AudioClip Jump;
    public AudioClip Move;
    public AudioClip [] Deaths;
    public AudioClip Cumsplosion;
    public AudioSource Audio;


    public float JumpAmount;
    public float MoveForce;
    public float AnimationSpeedScaler;

    // The max velocity the player can achieve through button presses
    private const float MAX_VELOCITY_POS = 4f;
    private Vector2 movementDirection;
    float angle;

    private bool isOnGround = true;
    private bool playerIsAtEnd = false;
    private int isFacingRight = 1;

    void Start ()
    {
        RBody = GetComponent<Rigidbody2D> ();
        Anim = GetComponent<Animator> ();
        Gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        movementDirection = new Vector2 (0, 1f);
        Audio = GetComponent<AudioSource> ();
    }

    private void Update ()
    {
        Anim.speed = Mathf.Min (RBody.velocity.magnitude * AnimationSpeedScaler, 1.7f);
        //  //Debug.Log (RBody.velocity);

        if ( playerIsAtEnd && Input.GetButton("restart") ){
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void FixedUpdate ()
    {

        RaycastHit2D [] hits = new RaycastHit2D [2];
        int h = Physics2D.RaycastNonAlloc (transform.position, -Vector2.up, hits, 0.5f); //cast downwards
        Debug.DrawRay (transform.position, -Vector2.up, Color.red);
        //Debug.Log (hits[0].normal);
        if (h > 1)
        { //if we hit something do stuff
            isOnGround = true;
            movementDirection = new Vector2 (hits [1].normal.y, -hits [1].normal.x); // Note y is flipped
            //angle = Mathf.Abs (Mathf.Atan2 (hits [1].normal.x, hits [1].normal.y) * Mathf.Rad2Deg); //get angle

        }
        else
        {
            isOnGround = false;
            Debug.Log ("IN AIR");
        }




        if (Input.GetButtonDown ("Jump") && isOnGround)
        {
            Audio.clip = Jump;
            Audio.time = 0.5f;
            Audio.Play ();
            RBody.AddForce (new Vector2 (0f, JumpAmount), ForceMode2D.Impulse);
        }

        float horizontal = Input.GetAxisRaw ("Horizontal");

        // Moving Right.
        if (horizontal > 0 && isOnGround && RBody.velocity.x < MAX_VELOCITY_POS)
        {
            RBody.AddForce (movementDirection * MoveForce, ForceMode2D.Impulse);
           // Debug.Log (movementDirection);
            Debug.DrawRay (transform.position, movementDirection, Color.green);
        }

        // Moving left.
        else if (horizontal < 0 && isOnGround && RBody.velocity.x > MAX_VELOCITY_POS * -1)
        {
            RBody.AddForce (-movementDirection * MoveForce, ForceMode2D.Impulse);
           // Debug.Log (-movementDirection);
            Debug.DrawRay (transform.position, -movementDirection, Color.green);
        }

        // Moving Right air.
        else if (horizontal > 0 && !isOnGround && RBody.velocity.x < MAX_VELOCITY_POS)
        {
           RBody.AddForce (movementDirection * MoveForce * 5.2f, ForceMode2D.Force);
        }

        // Moving left air.
        else if (horizontal < 0 && !isOnGround && RBody.velocity.x > MAX_VELOCITY_POS * -1)
        {
            RBody.AddForce (-movementDirection * MoveForce * 5.2f, ForceMode2D.Force);
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
        if (collision.gameObject.tag == "ending"){
            playerIsAtEnd = true;
        }
    }

    public void Death ()
    {
        Audio.clip = Deaths [Mathf.FloorToInt (Random.Range (0f, (float) Deaths.Length))];
        Audio.Play ();
    }
}
