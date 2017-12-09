using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {
    private Player1Controller player1;
    private Player2Controller player2;
    private GameObject leftText, rightText;
    private Text deathText, optionsText;

    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player 1").GetComponent<Player1Controller>();
        player2 = GameObject.Find("Player 2").GetComponent<Player2Controller>();
        leftText = GameObject.Find("Player 1 Health");
        rightText = GameObject.Find("Player 2 Health");
        deathText = GameObject.Find("Death Screen").GetComponent<Text>();
		optionsText = GameObject.Find("Options Screen").GetComponent<Text>();
        deathText.enabled = false;
		optionsText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        leftText.GetComponent<Text>().text = "";
        rightText.GetComponent<Text>().text = "";
        switch (player1.livesRemaining)
        {
            case 0: leftText.transform.Find("Health1").GetComponent<Image>().enabled = false; break;
            case 1: leftText.transform.Find("Health2").GetComponent<Image>().enabled = false; break;
            case 2: leftText.transform.Find("Health3").GetComponent<Image>().enabled = false; break;
            case 3: leftText.transform.Find("Health4").GetComponent<Image>().enabled = false; break;
            case 4: leftText.transform.Find("Health5").GetComponent<Image>().enabled = false; break;
            default: break;
        }
        switch (player2.livesRemaining)
        {
            case 0: rightText.transform.Find("Health1").GetComponent<Image>().enabled = false; break;
            case 1: rightText.transform.Find("Health2").GetComponent<Image>().enabled = false; break;
            case 2: rightText.transform.Find("Health3").GetComponent<Image>().enabled = false; break;
            case 3: rightText.transform.Find("Health4").GetComponent<Image>().enabled = false; break;
            case 4: rightText.transform.Find("Health5").GetComponent<Image>().enabled = false; break;
            default: break;
        }
		if ((player1.livesRemaining == 0 || player2.livesRemaining == 0) && deathText.enabled == false) {
			deathText.enabled = true;
		}
		if (player1.livesRemaining == 0 && deathText.enabled == true && optionsText.enabled == false) {
			deathText.text = "Player 2 Wins!";
            optionsText.enabled = true;
            optionsText.text = "Press R to Restart\nPress E to End Game";
		}
		if (player2.livesRemaining == 0 && deathText.enabled == true && optionsText.enabled == false) {
			deathText.text = "Player 1 Wins!";
            optionsText.enabled = true;
            optionsText.text = "Press R to Restart\nPress E to End Game";
		}
	}

    private void FixedUpdate()
    {
        if (deathText.enabled && Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("sticky keys");
        if (deathText.enabled && Input.GetKey(KeyCode.E))
            SceneManager.LoadScene("main menu");
    }
}
