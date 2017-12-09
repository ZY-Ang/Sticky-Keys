using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ceiling"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        else
        {
            StartCoroutine(DestroyAfterFive());
        }
		if ((collision.collider.CompareTag ("Floor") || collision.collider.CompareTag ("Platform Floor")) && GetComponent<Animator>().enabled)
		{
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}
    }

    IEnumerator DestroyAfterFive()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        GameObject.Find("Item Delete").GetComponent<AudioSource>().Play();
    }
}