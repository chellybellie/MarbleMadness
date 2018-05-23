using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollower : MonoBehaviour {
    public GameObject player;
    private Vector3 Offset;

    void start()
    {
        Offset = player.transform.position - transform.position ;
    }

	void FixedUpdate ()
    {
        transform.position = player.transform.position + Offset;	
	}
}
