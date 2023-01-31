using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Animator anim;
    //Turn off Menu at the beginning of the game
    void Start()
    {
        gameObject.SetActive(false);
        
        anim.SetBool("Dead?", true);
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);

    }

    public void UntoggleEndMenu()
    {
        gameObject.SetActive(false);
    }

    //Reload the Scene you are currently on
    public void Restart()
    {
     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }
}
