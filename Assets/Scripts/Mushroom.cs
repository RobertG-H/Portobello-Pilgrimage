using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Rigidbody2D RBody;
    // Use this for initialization
    void Start ()
    {
        RBody = GetComponent<Rigidbody2D> ();

        StartCoroutine (AddForce ());
        StartCoroutine (RandUp ());
    }

    // Update is called once per frame
    void Update ()
    {

    }

    IEnumerator AddForce ()
    {
        while (true)
        {
            yield return new WaitForSeconds (Random.Range (0.5f, 3f));
            RBody.AddForce (new Vector2 (2f, 0), ForceMode2D.Impulse);
        }

    }

    IEnumerator RandUp ()
    {
        while (true) {
            yield return new WaitForSeconds (Random.Range (0.5f, 3f));
            RBody.AddForce (new Vector2 (0f, 3f), ForceMode2D.Impulse);
            //Debug.Log ("JUMPING");
        }

    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            RBody.AddForce (new Vector2 (2f, 0), ForceMode2D.Impulse);
        }
    }
}
