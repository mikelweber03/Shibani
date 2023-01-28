using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //Turn off Menu at the beginning of the game
    void Start()
    {   
        gameObject.SetActive(false);
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
    }
    //Reload the Scene you are currently on
    public void Restart()
    {
     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Save position of all existing character and objects in Game manager and re instantiate them after the players death
        //Position of latest checkpoint is saved when player crosses checkpoint
        

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("LucianosMainMenu");
        
    }
}
