using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text scoreText;
    public Slider bossHpSlider;
    public int score = 0;
    Player player;
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject resetUI;
    void Start()
    {
        player = FindObjectOfType<Player>();
        hpText.SetText(player.maxHp.ToString());
        scoreText.SetText($"SCORE : ");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<UIController>().resetUI.gameObject.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<MoonBehaviour>().gameObject.GetComponent<MoonBehaviour>().enabled = true;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().enabled = true;
        Cursor.visible = false;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<MoonBehaviour>().gameObject.GetComponent<MoonBehaviour>().enabled = false;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().enabled = false;
        isPaused = true;
    }

    public void UpdateScore()
    {
        scoreText.SetText($"SCORE : {score}");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
