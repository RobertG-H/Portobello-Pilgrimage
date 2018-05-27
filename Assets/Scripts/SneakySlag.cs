using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySlag : MonoBehaviour {


	private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
		coroutine = WaitAndPrint(5.0f);
        StartCoroutine(coroutine);
	}
	private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(70, 70), ForceMode2D.Impulse);
        }
    }
	// Update is called once per frame
	void Update () {
		if ( gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 ) {
			Debug.Log("woo");
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(70, 0), ForceMode2D.Impulse);
		}
		if ( gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 ) {
			Debug.Log("woo");
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 70), ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.gameObject.tag == "Player" || collision.gameObject.tag == "Fam" )  {
			//Destroy(collision.gameObject);
			Debug.Log("Dead homie");
		}
    }

}
