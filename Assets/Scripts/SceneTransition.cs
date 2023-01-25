using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public Animator anim;

    public bool shouldSwitch;

    public float transitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        shouldSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSwitch)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
