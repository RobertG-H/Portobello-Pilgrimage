using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumsplosion : MonoBehaviour {

	private CircleCollider2D playerCollider;
	private bool didITouchHim;
	private bool canCumsplode;
	private IEnumerator coroutine;
	public float bounciness;
	// Use this for initialization
	void Start () {
		didITouchHim = false;
		canCumsplode = true;
		playerCollider = GetComponent<CircleCollider2D>();
		playerCollider.radius = 0.5f;

		coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);
	}

	private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            canCumsplode = true;
        }
    }

	// Update is called once per frame
	void Update () {
		if ( gameObject.tag == "Fam" && Input.GetButtonDown("Cumsplode") && didITouchHim && canCumsplode ) {
			canCumsplode = false;
			Vector2 velocity = ( gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(6, 6) ) * 3 ;
			gameObject.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);
		} else if ( gameObject.tag == "Player" && canCumsplode ){
			canCumsplode = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
			didITouchHim = true;
		}
    }

	void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
			didITouchHim = false;
		}
    }

}
