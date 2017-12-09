using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour {

    public void OnMouseUp()
    {
        SceneManager.LoadScene("main menu");
    }
}
