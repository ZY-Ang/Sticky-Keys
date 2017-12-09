using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {

    public float speed = 0.25f, gravity = 1000;

    private Rigidbody2D playerBody;
    private bool isJump;
    public Animator runningAnimator;

    void Start()
    {
        isJump = false;
        playerBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PlayerControls();
    }

    private void Update()
    {

    }

    void PlayerControls()
    {
        if (isJump)
        {
            playerBody.AddForce(new Vector2(0, speed * gravity));
            isJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Floor"))
            isJump = true;
        else
            isJump = false;
    }

    public Vector2 getCurrentPosition()
    {
        return transform.position;
    }
}
