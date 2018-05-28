using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public AudioSource Audio;

    void Start ()
    {
        Audio = GetComponent<AudioSource> ();
    }

    void Update ()
    {
        if (Input.GetKeyDown ("space"))
        {
            Application.LoadLevel ("Level1");
        }
    }
}