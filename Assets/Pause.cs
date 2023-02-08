using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pause;
    public GameObject mainCanvas;
    public GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            pauseCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            Time.timeScale = 0;
        }

        if (!pause)
        {
            pauseCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void ResumeGame()
    {
        pause = !pause;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("SampleScene 4");
    }
}
