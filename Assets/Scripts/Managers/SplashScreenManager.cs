using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
