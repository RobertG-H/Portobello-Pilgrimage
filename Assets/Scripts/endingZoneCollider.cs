using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingZoneCollider : MonoBehaviour {

	public GameObject endingText; // Assign in inspector
    private bool isShowing = false;
    public GameManager Gm;
    public AudioSource Audio;

	// Use this for initialization
	void Start () {
        Gm = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        endingText.SetActive(isShowing);
        Audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isShowing = true;
            endingText.SetActive(isShowing);
            Gm.StopMusic ();
            Audio.Play ();
        }
    }
}
