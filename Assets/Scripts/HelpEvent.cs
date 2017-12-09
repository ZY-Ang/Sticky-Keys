using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpEvent : MonoBehaviour {
    
    public void OnMouseUp()
    {
        SceneManager.LoadScene("halp");
    }
}
