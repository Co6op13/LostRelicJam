using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsStarted = false;
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject startedMenuUI;
    [SerializeField] private Animator animatorStartMeny;
    [SerializeField] private Animator animatorPauseMeny;
    [SerializeField] private GameObject player2;


    private void Awake()
    {

        Time.timeScale = 0f;
    }

    //private void Start()
    //{
    //    animatorStartMeny.Play("MenyStartOpen");
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & (GameIsStarted == true))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameIsStarted == false)
        {
            ActiveMenyStartGame();
        }

    }

    public void Resume()
    {

        GameIsPaused = false;
        Time.timeScale = 1f;
        animatorPauseMeny.Play("MenyPauseClose");
        StartCoroutine(CloseMenu(pauseMenuUI));

    }

    public void Pause()
    {
        Debug.Log("test");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        animatorPauseMeny.Play("MenyPauseOpen");
        GameIsPaused = true;
    }

    public void ActiveMenyStartGame()
    {

        startedMenuUI.SetActive(true);
        animatorStartMeny.Play("MenyStartOpen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartOnePlayer()
    {
        GameIsStarted = true;
        Time.timeScale = 1f;
        animatorStartMeny.Play("MenyStartClose");
        StartCoroutine(CloseMenu(startedMenuUI));
    }

    public void StartTwoPlayers()
    {
        GameIsStarted = true;
        Time.timeScale = 1f;
        animatorStartMeny.Play("MenyStartClose");
        player2.SetActive(true);
       // Instantiate(player2, new Vector3(3f, 0f, 0f), transform.rotation);
        StartCoroutine(CloseMenu(startedMenuUI));
    }

    IEnumerator CloseMenu(GameObject menu)
    {
        yield return new WaitForSeconds(1f);
        menu.SetActive(false);
    }


}
