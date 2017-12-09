using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {

	public float slowMoveTime = 1f;
	public float fastMoveTime = 0.3f;
	public static EnemyController instance = null;

	private Player1Controller p1c;
	private Player2Controller p2c;

	private Rigidbody2D rb2d;
	private bool locked = false;

	private Vector2 vector2p1;
	private Vector2 vector2p2;

	public bool isFingering, isDead;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}
		Physics2D.IgnoreCollision(GameObject.Find("Ceiling").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Left Wall").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Right Wall").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Floor").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Floor 1").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Ceiling 1").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Floor 2").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Ceiling 2").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Floor 3").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Ceiling 3").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(GameObject.Find("Platform Ceiling 3").GetComponent<Collider2D>(), GetComponent<Collider2D>());

		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.gameObject.SetActive(true);

		isFingering = true;

		p1c = GameObject.Find("Player 1").GetComponent<Player1Controller>();
		p2c = GameObject.Find("Player 2").GetComponent<Player2Controller>();
		vector2p1 = new Vector2(p1c.getCurrentPosition().x + 2.1f, 31.2f);
		vector2p2 = new Vector2(p2c.getCurrentPosition().x + 2.1f, 31.2f);
		rb2d.MovePosition (vector2p1);
	}
	void Update() {
		if (isFingering && !isDead) {
			if ((BoardManager.instance.time + 10000000) % 10 == 1) {
				rb2d.transform.Translate (0, -0.55f, 0);
				locked = true;
			} 

			//tracks the current targeted player by getting their vector position and moving towards it.
			if (BoardManager.instance.targetedPlayer == 0 && !locked) {
				vector2p1.Set (p1c.getCurrentPosition ().x + 2.1f, 31.2f);
				rb2d.MovePosition (vector2p1);
			} else if (BoardManager.instance.targetedPlayer == 1 && !locked) {
				vector2p2.Set (p2c.getCurrentPosition ().x + 2.1f, 31.2f);
				rb2d.MovePosition (vector2p2);
			}

		} else if (!isDead) {
			rb2d.transform.Translate (0, 0.6f, 0);
			if (rb2d.transform.position.y > 31) {
				isFingering = true;
			}
			locked = false;
		}
		if (rb2d.transform.position.y < 4.0 && !isDead) {
			GameObject.Find("Finger Explosion").GetComponent<AudioSource>().Play();
			CameraShake.shakeDuration = 2.0f;
			isFingering = false;
		}
	}

	void OnCollisionEnter2D(Collision2D player) {
        if (player.collider.gameObject.CompareTag("player"))
        {
		}
	}

}