using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class Yeti : MonoBehaviour {

	Animator anim;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		StartCoroutine (fade ());
		audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator fade(){
		yield return new WaitForSeconds(1.5f);
		anim.SetTrigger ("fadeDone");
		audioSource.Play();
		yield return new WaitForSeconds(2.5f);
		Application.LoadLevel("MainMenu");
	}
}
