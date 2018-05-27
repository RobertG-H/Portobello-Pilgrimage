using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumsplosion : MonoBehaviour {

	private CircleCollider2D playerCollider;
	private bool didITouchHim;
	private bool canCumsplode;
	private bool radiusLrg;
	private IEnumerator coroutine;
	private IEnumerator coroutine2;
	
	// Use this for initialization
	void Start () {
		didITouchHim = false;
		canCumsplode = true;
		radiusLrg = false;
		playerCollider = GetComponent<CircleCollider2D>();
		playerCollider.radius = 0.5f;

		coroutine = WaitAndPrint(2.0f);
		coroutine2 = radius(0.1f);
        StartCoroutine(coroutine);
		StartCoroutine(coroutine2);
	}

	private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            canCumsplode = true;
        }
    }
	private IEnumerator radius(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
			if ( radiusLrg ){
				radiusLrg = !radiusLrg;
				Debug.Log("read");
				playerCollider.radius = 0.5f;
			}
			
        }
    }

	// Update is called once per frame
	void Update () {
		if ( gameObject.tag == "Fam" && Input.GetButtonDown("Cumsplode") && didITouchHim && canCumsplode ) {
			canCumsplode = false;
			Vector2 velocity = ( gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(10, 10) ) * 3 ;
			gameObject.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);
		} else if ( gameObject.tag == "Player" && canCumsplode && Input.GetButtonDown("Cumsplode")){
			playerCollider.radius = 4.0f;
			canCumsplode = false;
			radiusLrg = true;
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
