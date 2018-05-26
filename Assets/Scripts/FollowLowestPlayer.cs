using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLowestPlayer : MonoBehaviour
{
    public GameObject [] Players;
    public GameObject PlayerToFollow;

    void Start ()
    {
        Players = GameObject.FindGameObjectsWithTag ("Player");
        PlayerToFollow = Players [0];
        gameObject.transform.SetParent (PlayerToFollow.transform);
    }

    void LateUpdate ()
    {
        float largestX = -100f;
        foreach (GameObject player in Players)
        {
            if (largestX < player.transform.position.x)
            {
                largestX = player.transform.position.x;
                PlayerToFollow = player;
            }
        }
       // gameObject.transform.position = PlayerToFollow.transform.position;
        gameObject.transform.SetParent (PlayerToFollow.transform);
    }
}