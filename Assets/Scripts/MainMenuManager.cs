using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameManager Gm;
    public AudioSource Audio;

    void Start ()
    {
        Gm = FindObjectOfType<GameManager> ();
        Audio = GetComponent<AudioSource> ();
    }

    void Update ()
    {
        if (Input.GetKeyDown ("space"))
        {
            Audio.Stop ();
            Gm.StartGame ();
        }
    }
}