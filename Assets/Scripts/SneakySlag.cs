using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySlag : MonoBehaviour
{

    public GameObject [] Waypoints;
    public Rigidbody2D RBody;

    public float Speed;

    private int waypointIndex;
    private bool atWaypoint;

    private float velocityX = 0f;
    private float velocityY = 0f;

    private float directionX = 0f;
    private float directionY = 0f;

    void Start ()
    {
        RBody = GetComponent<Rigidbody2D> ();
        atWaypoint = true;
    }

    void Update ()
    {
        directionX = Waypoints [waypointIndex].transform.position.x - transform.position.x;
        directionY = Waypoints [waypointIndex].transform.position.y - transform.position.y;

        if (atWaypoint)
        {
            atWaypoint = false;
            velocityX = directionX;
            velocityY = directionY;
        }

        if (Mathf.Abs (directionX) <= 0.5f && Mathf.Abs (directionY) <= 0.5f)
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

        RBody.velocity = new Vector2 (velocityX * Speed, velocityY * Speed);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Fam")
        {
            //Destroy(collision.gameObject);
            Debug.Log ("Dead homie");
        }
    }

}
