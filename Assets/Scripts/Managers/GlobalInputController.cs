using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalInputController : MonoBehaviour
{
    public UIMenu pauseMenu; 
    void Update()
    {
        // pause/unpause game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf) pauseMenu.Exit();
            else pauseMenu.Init();
        }

        // reload scene
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
