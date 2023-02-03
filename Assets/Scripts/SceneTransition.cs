using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public Animator anim;
    public DeathMenu deathmenu;
    public bool shouldSwitch;

    public float transitionTime = 1f;
    public GameObject wipes;

    // Start is called before the first frame update
    void Start()
    {
        shouldSwitch = false;
        wipes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSwitch)
        {
            LoadNextLevel();
            
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            wipes.SetActive(true);
            anim.SetTrigger("Start");
        }
        

    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            wipes.SetActive(true);
            anim.SetTrigger("Start");
        }
            

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);     

        
    }




}
