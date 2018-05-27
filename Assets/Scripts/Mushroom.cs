using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Rigidbody2D RBody;
    public GameManager Gm;
    private bool touchingFloor = false;
    public Vector2 continualForce = new Vector2 (0.0f, 0.0f);
    private float movementFrequency;
    private bool stuck = false;
    bool doOnce = true;
    private Vector3 pos;
    // Use this for initialization
    void Start ()
    {
        
        RBody = GetComponent<Rigidbody2D> ();
        Gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        Vector2 initForce = new Vector2 (Random.Range (0.5f, 1.5f), Random.Range (0.5f, 1.5f));
        movementFrequency = Random.Range (0.5f, 1.0f);
        continualForce = initForce;
        pos = RBody.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {

        if(doOnce)
        {
            float force = Random.Range(0.1f, 0.2f);
            RBody.AddForce (new Vector2 (force, force), ForceMode2D.Impulse);
            StartCoroutine (AddForce ());
            StartCoroutine (RandUp ());
            StartCoroutine (minimumMovement());
            StartCoroutine (ifStuck());
            doOnce = false;
        }
        
    }

    IEnumerator AddForce ()
    {
        while (true)
        {
            yield return new WaitForSeconds (Random.Range (0.5f, 2f));
            
            if( Random.Range(0, 2) % 2 == 0){
                
                float force = Random.Range(0.5f, 2f);
                RBody.AddForce (new Vector2 (force, 0), ForceMode2D.Impulse);

            }
            
        }

    }

    IEnumerator RandUp ()
    {
        while (true) {
            yield return new WaitForSeconds (Random.Range (0.5f, 2f));

            if( Random.Range(0, 2) % 2 == 0 && touchingFloor ){
                //Debug.Log("Jumping");
                float force = Random.Range(0.5f, 2f);
                RBody.AddForce (new Vector2 (0, force), ForceMode2D.Impulse);
                touchingFloor = false;
               
            }  
        }
    }

    IEnumerator minimumMovement ()
    {
        while (true) {
            yield return new WaitForSeconds (movementFrequency);
            //Debug.Log(movementFrequency);
            RBody.AddForce (continualForce, ForceMode2D.Impulse);    
        }
    }

    IEnumerator ifStuck ()
    {
        while (true) {
            yield return new WaitForSeconds (1.5f);
            //Debug.Log(movementFrequency);
            Vector3 newPos = RBody.transform.position;
            Debug.Log(Mathf.Abs(pos.x - newPos.x));
            
            if (Mathf.Abs(pos.x - newPos.x) < 0.5 && Mathf.Abs(pos.y - newPos.y) < 0.5) {
                stuck = true;
                Debug.Log(stuck);
            }

            if (stuck) {
                RBody.AddForce (new Vector2 (0.5f, 2.0f), ForceMode2D.Impulse);
                stuck = false;
            } 
            pos = newPos;  
        }
    }

    private void OnCollisionStay2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchingFloor = true;
        }
    }
}
