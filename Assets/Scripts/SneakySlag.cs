using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySlag : MonoBehaviour
{

    public GameObject [] Waypoints;
    public Rigidbody2D RBody;
    public AudioSource Audio;

    public AudioClip Moving;
    public AudioClip Eating;

    public float Speed;

    private int waypointIndex;
    private bool atWaypoint;

    private Vector2 velocity;

    private float directionX = 0f;
    private float directionY = 0f;

    void Start ()
    {
        RBody = GetComponent<Rigidbody2D> ();
        atWaypoint = true;
        Audio = GetComponent<AudioSource> ();
        Audio.clip = Moving;
        Audio.Play ();
    }

    void Update ()
    {
        directionX = Waypoints [waypointIndex].transform.position.x - transform.position.x;
        directionY = Waypoints [waypointIndex].transform.position.y - transform.position.y;

        if (atWaypoint)
        {
            atWaypoint = false;
            velocity = new Vector2 (directionX, directionY).normalized * Speed;
        }

        if (Mathf.Abs (directionX) <= 1f && Mathf.Abs (directionY) <= 1f)
        {
            // At waypoint so stop moving
            RBody.velocity = new Vector2 (0, 0);
            Debug.Log ("AT WAYPOINT ");
            if (waypointIndex < Waypoints.Length - 1)
            {
                waypointIndex++;
            }

            atWaypoint = true;
        }

        RBody.velocity = velocity;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject);
            Debug.Log ("Dead homie");
            Audio.clip = Eating;
            Audio.Play ();
        }

        if (other.gameObject.tag == "Fam")
        {
            Destroy (other.gameObject);
        }
    }
}
