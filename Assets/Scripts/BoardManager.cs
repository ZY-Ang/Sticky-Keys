using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

	//need a timer
	//need a reference to which player is being targeted
	public static BoardManager instance = null;
	public int targetedPlayer;
	public int time = 10; //initial time set to 100 seconds
    public bool chaosMode = false;

	private readonly int PLAYER_ONE_SELECTED = 0;
	private readonly int PLAYER_TWO_SELECTED = 1;

	public GameObject hazard, hazard1;
    public GameObject powerup;
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    public GameObject powerup4;
    //private Vector3 hazardVector3;
    //private Vector3 powerUpVector3;
    private readonly int HAZARD_CAP = 3;
	private readonly int POWERUP_CAP = 3;
	private float SPAWN_MAX;
	private float SPAWN_MIN;
	private float SPAWN_HEIGHT;

	//UI elements
	private Text timerText;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}

		timerText = GameObject.Find("Timer").GetComponent<Text>();

		targetedPlayer = Random.Range (0, 2); //choose a random player to be targeted first

		//setup spawning
		SPAWN_MAX = GameObject.Find ("Right Wall").transform.position.x + (GameObject.Find ("Right Wall").GetComponent<BoxCollider2D>().size.x / 2f);
		SPAWN_MIN = GameObject.Find ("Left Wall").transform.position.x + (GameObject.Find("Left Wall").GetComponent<BoxCollider2D>().size.x / 2f);
		SPAWN_HEIGHT = (GameObject.Find ("Right Wall").GetComponent<BoxCollider2D> ().size.y / 2f) - 1.3f;

		UpdateUI();
		InvokeRepeating("UpdateTimer", 0f, 1f);
		InvokeRepeating ("SpawnObjects", 0f, 2f);
	}

	// Update is called once per frame
	void Update () {

	}

	//called when a user presses the shift key
	public void changePlayer() {
		targetedPlayer = (targetedPlayer == PLAYER_ONE_SELECTED) ? PLAYER_TWO_SELECTED : PLAYER_ONE_SELECTED;
		UpdateUI ();
	}

	void UpdateTimer() {
        //if (time > 0)
		    time--;
		if (time <= 30)
			timerText.color = Color.yellow;
		if (time <= 5)
			timerText.color = Color.red;
        if (time <= 0)
        {
            if (chaosMode == false)
                InvokeRepeating("SpawnObjects", 0f, 0.25f);
            chaosMode = true;
        }
		UpdateUI();
	}

	private void UpdateUI ()
    {
        if (time > 0)
            timerText.text = time.ToString();
		if (time <= 0) {
			timerText.text = "Panic!";
			GameObject.Find ("Soundtrack").GetComponent<AudioSource>().pitch = 2.0f;
		}
	}

	void SpawnObjects () {
		float hazardX = Random.Range ((float)SPAWN_MIN + 1, (float)SPAWN_MAX - 1);
		float powerUpX = Random.Range((float)SPAWN_MIN + 1, (float)SPAWN_MAX - 1);

		Vector3 hazardVector3 = new Vector3(hazardX, SPAWN_HEIGHT, 0f);
		Vector3 powerUpVector3 = new Vector3(powerUpX, SPAWN_HEIGHT, 0f);
        int powerType = Random.Range(0, 5);
        
        switch (powerType)
        {
            case 0: Instantiate(powerup, powerUpVector3, Quaternion.identity); break;
            case 1: Instantiate(powerup1, powerUpVector3, Quaternion.identity); break;
            case 2: Instantiate(powerup2, powerUpVector3, Quaternion.identity); break;
            case 3: Instantiate(powerup3, powerUpVector3, Quaternion.identity); break;
            case 4: Instantiate(powerup4, powerUpVector3, Quaternion.identity); break;
        }

        int hazardType = Random.Range(0, 2);
        if (hazardType == 0)
            Instantiate(hazard, hazardVector3, Quaternion.identity);
        else
            Instantiate(hazard1, hazardVector3, Quaternion.identity);
    }
}