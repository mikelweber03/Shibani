using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator anim;

    public SceneTransition transition;

    public float transitionTime = 2f;

    public Scene newGameScene;

    public GameObject wipes;

    void Start()
    {
        wipes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        transition.shouldSwitch = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}

