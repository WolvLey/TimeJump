using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
