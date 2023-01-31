using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log(collision.name);
        if(collision.name == "Player")
        {
            // Loads pause scene
            SceneManager.LoadScene(0);
        }
    }
}
