using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour {

	public float speed = 0.25f, gravity = 1000;
	public Rigidbody2D player2Body;
    
	private Rigidbody2D player1Body;
	private bool onFloor, isDead;
    float flip;

    public int livesRemaining = 5;
    public double invulnerability = 0;
    public float timeHit = 0;
    public float cantMove = 0;
    public float currentScale = 1.2f;

    void Start()
    {
		onFloor = false;
		isDead = false;
		player1Body = GetComponent<Rigidbody2D>();
		Physics2D.IgnoreCollision(player2Body.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}

    private void Update()
    {
        if (invulnerability > 0)
            invulnerability -= Time.deltaTime;
        if (invulnerability < 0)
        {
            invulnerability = 0;
            Color color = this.gameObject.GetComponent<Renderer>().material.color;
            this.gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 1);
        }
        if (invulnerability != 0)
        {
            Color color = this.gameObject.GetComponent<Renderer>().material.color;
            if (timeHit > 0)
                this.gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 0.75f + 0.25f * Mathf.Cos((Time.time - timeHit) * 180 / Mathf.PI));
        }
        if (cantMove > 0)
            cantMove -= Time.deltaTime;
        if (cantMove < 0)
        {
            cantMove = 0;
        }
    }

    void FixedUpdate()
    {
        if (livesRemaining > 0 && cantMove == 0)
        {
            Player1Controls();
        }
        else if (livesRemaining == 0)
        {
            GetComponent<Animator>().SetBool("dead", true);
        }
    }

	void Player1Controls()
    {
        bool A = Input.GetKey(KeyCode.A);
        bool D = Input.GetKey(KeyCode.D);
        bool W = Input.GetKey(KeyCode.W);
        bool S = Input.GetKey(KeyCode.S);
        if (S)
        {
            GetComponent<Animator>().SetTrigger("taunt");
        }
        if (A || D)
        {
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (W && onFloor)
        {
			player1Body.AddForce(new Vector2(0, speed * gravity));
            onFloor = false;
            GameObject.Find("Jumping").GetComponent<AudioSource>().Play();
        }
        if (A)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            transform.Translate(speed, 0, 0);
		}
		if (D)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(speed, 0, 0);
        }
        GetComponent<Animator>().SetBool("onFloor", onFloor);
		resetPosition();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
		if (other.collider.gameObject.CompareTag("Floor") || other.collider.gameObject.CompareTag("Platform Floor"))
        {
            onFloor = true;
        }

        if (other.collider.gameObject.CompareTag("hazard") && invulnerability == 0)
        {
            livesRemaining -= 1;
            invulnerability = 3;
            cantMove = 2f;
            timeHit = Time.time;
            other.gameObject.GetComponent<Animator>().SetBool("isSplat", true);
            Destroy(other.gameObject);
            GetComponent<Animator>().SetTrigger("hit");
            GameObject.Find("Damage Taken").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("hazard1") && invulnerability == 0)
        {
            livesRemaining -= 1;
            invulnerability = 3;
            player1Body.AddForce(new Vector2(0, speed * gravity * 5f));
            timeHit = Time.time;
            other.gameObject.GetComponent<Animator>().SetTrigger("contact");
            Destroy(other.gameObject);
            GetComponent<Animator>().SetTrigger("hit");
            GameObject.Find("Damage Taken").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("powerup"))
        {
            currentScale *= 1.2f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            Destroy(other.gameObject);
			GameObject.Find("Power Up").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("powerup1"))
        {
            speed += 0.25f;
            gravity += 1000;
            Destroy(other.gameObject);
			GameObject.Find("Power Up").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("powerup2"))
        {
            ShiftScript shiftScript = GameObject.Find("Player Controllers").GetComponent<ShiftScript>();
            shiftScript.leftCount += 1;
            Destroy(other.gameObject);
			GameObject.Find("Power Up").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("powerup3"))
        {
            Vector2 temp = transform.position;
            GameObject player2 = GameObject.Find("Player 2");
            ShiftScript shiftScript = GameObject.Find("Player Controllers").GetComponent<ShiftScript>();
            transform.position = player2.transform.position;
            player2.transform.position = temp;

            shiftScript.swapLine.SetPosition(0, player2.transform.position);
            shiftScript.swapLine.SetPosition(1, transform.position);
            shiftScript.swapLine.enabled = true;
            shiftScript.swapLineTimer = (float)0.2;
            Destroy(other.gameObject);
			GameObject.Find("Shifting").GetComponent<AudioSource>().Play();
            BoardManager.instance.changePlayer();
        }
        if (other.collider.gameObject.CompareTag("powerup4"))
        {
            ShiftScript shiftScript = GameObject.Find("Player Controllers").GetComponent<ShiftScript>();
            shiftScript.rightDeny = 5;
            Destroy(other.gameObject);
            GameObject.Find("Power Up").GetComponent<AudioSource>().Play();
        }
        if (other.collider.gameObject.CompareTag("finger"))
        {
			if (!isDead) {
				GameObject.Find ("Death").GetComponent<AudioSource> ().pitch = 1.5f;
				GameObject.Find("Death").GetComponent<AudioSource>().Play();
				isDead = true;
			}

			livesRemaining = 0;
			GetComponent<Animator>().SetBool("dead", true);
            GameObject.Find("Finger").GetComponent<EnemyController>().isDead = true;
		}
    }

    private void OnCollisionExit2D(Collision2D other)
    {
		if (other.collider.gameObject.CompareTag("Floor") || other.collider.gameObject.CompareTag("Platform Floor"))
        {
            onFloor = false;
            GetComponent<Animator>().SetTrigger("jump");
            GameObject.Find("Jumping").GetComponent<AudioSource>().Play();
        }
    }

    public Vector2 getCurrentPosition() {
		return transform.position;
	}

	public void resetPosition() {
		if (player1Body.position.y < -11 || player1Body.position.x > 38 || player1Body.position.x < -38)
			player1Body.position = new Vector3(-20, -6.5f, 0);
	}
}
