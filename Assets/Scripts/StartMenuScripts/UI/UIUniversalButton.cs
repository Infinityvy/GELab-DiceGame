using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIUniversalButton : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start() {
        SoundAssets.AddSFX(audioSource);
    }

    public void GotoMenu(UIMenu toMenu) {
        if (toMenu != null)
            toMenu.Init();
        GetComponentInParent<UIMenu>().Exit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadDeckLibrary() {
        SceneManager.LoadScene("DeckLibrary");
    }

    public void LoadGame() {
        SceneManager.LoadScene("Game");
    }

    public void Continue() {
        Time.timeScale = 1.0f;
        GetComponentInParent<UIMenu>().Exit();
    }


    public void QuitGame() {
        Application.Quit();
    }

    public void MoveDie(Transform die) {
        if (die != null) {
            EventSystem.current.SetSelectedGameObject(gameObject);
            float objectWidth = GetComponent<RectTransform>().rect.width * GetComponentInParent<Canvas>().transform.localScale.x;
            float newX = transform.position.x - objectWidth / 2;
            float newY = transform.position.y;
            die.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, 10)) + Vector3.left;
        }
    }

    public void EnableSelector(GameObject selector) {
        EventSystem.current.SetSelectedGameObject(gameObject);
        selector.SetActive(true);
        
    }

    public void DisableSelector(GameObject selector) {
        selector.SetActive(false);
    }

    public void PlaySound() {
        if (audioSource != null) {
            audioSource.Play();
        }
    }
}
