using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBlink : MonoBehaviour
{
    public float BlinkingTime = .15f;
    public float TimeInterval = .05f;
    public PlayerHealth play;
    public SpriteRenderer nagatoSprite;
    private void Start()
    {
        play = this.GetComponent<PlayerHealth>();
        nagatoSprite = GameObject.FindWithTag("NagatoSprite").GetComponent<SpriteRenderer>();
    }
    public void tookDamage()
    {
        StartCoroutine(Flash(BlinkingTime, TimeInterval));
    }

    IEnumerator Flash(float time, float intervalTime)
    {
        //elapsedTime counts up time until the float set in BlinkingTime
        float elapsedTime = 0f;
   
        //T$$anonymous$$s repeats our coroutine until the Flas$$anonymous$$ngTime is elapsed
        while (elapsedTime < time)
        {
            // gets an array with all the renderers in our gameobject
            Renderer[] RendererArray = GetComponents<Renderer>();
            
            play.canbedamaged = false;
            //turns off all the Renderers
             foreach (Renderer r in RendererArray)
             {
             //r.enabled = false;
             nagatoSprite.enabled = false;
             }
           // playerSprite.GetComponent<Image>().enabled = false;
            
            //then add time to elapsedtime

            elapsedTime += Time.deltaTime;
            //then wait for the Timeinterval set
            yield return new WaitForSeconds(intervalTime);
            //then turn them all back on
             foreach (Renderer r in RendererArray)
             {
             //r.enabled = true;
             nagatoSprite.enabled = true;
             }
           // playerSprite.GetComponent<Image>().enabled = true;
            elapsedTime += Time.deltaTime;
            //then wait for another interval of time
            yield return new WaitForSeconds(intervalTime);
        }
       // Debug.Log("why u running");
        play.canbedamaged = true;

    }
}
