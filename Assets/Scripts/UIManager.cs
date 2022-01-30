using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");

        hidePaused();
        hideFinished();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        //shows finish gameobjects if player is dead and timescale = 0
		if (Time.timeScale == 0 && playerController.alive == false){
			showFinished();
		}
    }

    public void Quit()
    {
        Application.Quit();
    }


    //Reloads the Level
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnFinish tag
	public void showFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnFinish tag
	public void hideFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(false);
		}
	}

    //loads inputted level
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    void OnShowMenu(InputValue movementValue)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0 && playerController.alive == true)
        {
            Debug.Log("high");
            Time.timeScale = 1;
            hidePaused();
        }
    }
}
