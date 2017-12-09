using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftScript : MonoBehaviour {
    public float leftCD = 0, rightCD = 0, swapLineTimer = 0, leftTimer = 0, rightTimer = 0, rightDeny = 0, leftDeny = 0;
    public GameObject player1, player2;
    public int leftCount=0, rightCount=0;
    public Text leftText, rightText;
    public LineRenderer swapLine;

    void Start ()
    {
        leftCD = 10;
		rightCD = 10;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        swapLine = GameObject.Find("Line").GetComponent<LineRenderer>();

        leftText = GameObject.Find("Player 1 Cooldown").GetComponent<Text>();
        rightText = GameObject.Find("Player 2 Cooldown").GetComponent<Text>();
		
        swapLineTimer = 0;
        swapLine.positionCount = 2;
        swapLine.enabled = false;
        swapLine.sortingOrder = 2;
        swapLine.startWidth = (float) 0.08;
        swapLine.endWidth = (float) 0.08;
    }

    void Update()
    {
        if (swapLine.enabled)
        {
            swapLineTimer -= Time.deltaTime;
            if (swapLineTimer <= 0)
            {
                swapLine.enabled = false;
                swapLineTimer = 0;
            }
        }
        if (leftCD < 0)
            leftCD = 0;
        if (leftCD > 0)
        {
            leftCD -= Time.deltaTime;
            leftText.text = "Shift Ready In " + Mathf.Max((int)(leftCD + 1), (int)leftDeny+1);
			leftText.color = Color.gray;
        }
        if (rightCD < 0)
            rightCD = 0;
        if (rightCD > 0)
        {
            rightCD -= Time.deltaTime;
            rightText.text = "Shift Ready In " + Mathf.Max((int)(rightCD + 1), (int)rightDeny + 1);
			rightText.color = Color.gray;
        }
        if ((leftCD == 0 || leftCount > 0) && leftDeny == 0)
        {
            leftText.text = "Shift Ready!";
			leftText.color = new Color(0, 0.392f, 0, 1);
        }
        if ((rightCD == 0 || rightCount > 0) && rightDeny == 0)
        {
            rightText.text = "Shift Ready!";
			rightText.color = new Color(0, 0.392f, 0, 1);
        }
        if (rightTimer < 0)
            rightTimer = 0;
        if (rightTimer > 0)
        {
            rightTimer -= (float)Time.deltaTime;
        }
        if (rightDeny < 0)
            rightDeny = 0;
        if (rightDeny > 0)
        {
            rightDeny -= (float)Time.deltaTime;
            rightText.text = "Shift Ready In " + Mathf.Max((int)(rightCD + 1), (int)rightDeny + 1);
			rightText.color = Color.gray;
        }
        if (leftTimer < 0)
            leftTimer = 0;
        if (leftTimer > 0)
        {
            leftTimer -= (float)Time.deltaTime;
        }
        if (leftDeny < 0)
            leftDeny = 0;
        if (leftDeny > 0)
        {
            leftDeny -= (float)Time.deltaTime;
            leftText.text = "Shift Ready In " + Mathf.Max((int)(leftCD + 1), (int)leftDeny + 1);
			leftText.color = Color.gray;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (leftCD == 0 || leftCount > 0) && leftTimer == 0 && leftDeny == 0) {
            Vector2 temp = player1.transform.position;
            leftTimer = 0.2f;
            player1.transform.position = player2.transform.position;
            player2.transform.position = temp;
            if (leftCD == 0)
                leftCD = 5;
            else
                leftCount--;

            swapLine.SetPosition(0, player1.transform.position);
            swapLine.SetPosition(1, player2.transform.position);
            swapLine.enabled = true;
            swapLineTimer = (float) 0.2;
            print(player1.transform.position);
            print(player2.transform.position);
			GameObject.Find("Shifting").GetComponent<AudioSource>().Play();
            BoardManager.instance.changePlayer();
        }
        if (Input.GetKey(KeyCode.RightShift) && (rightCD == 0 || rightCount > 0) && rightTimer == 0 && rightDeny == 0) {
            Vector2 temp = player1.transform.position;
            rightTimer = 0.2f;
            player1.transform.position = player2.transform.position;
            player2.transform.position = temp;
            if (rightCD == 0)
                rightCD = 5;
            else
                rightCount--;

            swapLine.SetPosition(0, player2.transform.position);
            swapLine.SetPosition(1, player1.transform.position);
            swapLine.enabled = true;
            swapLineTimer = (float) 0.2;
            print(player1.transform.position);
            print(player2.transform.position);
			GameObject.Find("Shifting").GetComponent<AudioSource>().Play();
            BoardManager.instance.changePlayer();
        }
    }
}
