using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftscroll : MonoBehaviour {
    public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x >= -59.0)
            transform.position = new Vector2(transform.position.x - speed, 0);
        else
            transform.position = new Vector2(59.0f, 0);
    }
}
