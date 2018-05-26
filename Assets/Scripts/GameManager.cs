using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject [] AllPlayers;
    public GameObject Player;
    public GameObject Floor;
    public bool Debug;

    private void Start ()
    {
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

        if (Player.transform.position.x > 22)
        {
            GameObject.Destroy (Floor);
        }

    }
}
