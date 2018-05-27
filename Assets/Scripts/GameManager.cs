using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject [] AllPlayers;
    public AudioSource Audio;
    public bool Debug;


    private void Awake ()
    {
    }

    private void Start ()
    {
        Audio = GetComponent<AudioSource> ();
        AllPlayers = GameObject.FindGameObjectsWithTag ("Player");
    }

    void LateUpdate ()
    {
        if (Debug)
        {
            if (Input.GetKeyDown ("r"))
            {
                foreach (GameObject player in AllPlayers)
                {
                    player.transform.position = new Vector3 (0f, 0f, 0f);
                    player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
                }
            }
        }
    }

    public void StartGame ()
    {
        Audio.Play ();
        // Send event for starting game (Maybe?)
    }
}
