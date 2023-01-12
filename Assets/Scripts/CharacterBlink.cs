using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBlink : MonoBehaviour
{
    public float FlasNagatoTime = .6f;
    public float TimeInterval = .1f;

    public void tookDamage()
    {
        StartCoroutine(Flash(FlasNagatoTime, TimeInterval));
    }

    IEnumerator Flash(float time, float intervalTime)
    {
        //t$$anonymous$$s counts up time until the float set in Flas$$anonymous$$ngTime
        float elapsedTime = 0f;
        //T$$anonymous$$s repeats our coroutine until the Flas$$anonymous$$ngTime is elapsed
        while (elapsedTime < time)
        {
            //T$$anonymous$$s gets an array with all the renderers in our gameobject's c$$anonymous$$ldren
            Renderer[] RendererArray = GetComponents<Renderer>();
            //t$$anonymous$$s turns off all the Renderers
            foreach (Renderer r in RendererArray)
                r.enabled = false;
            //then add time to elapsedtime
            elapsedTime += Time.deltaTime;
            //then wait for the Timeinterval set
            yield return new WaitForSeconds(intervalTime);
            //then turn them all back on
            foreach (Renderer r in RendererArray)
                r.enabled = true;
            elapsedTime += Time.deltaTime;
            //then wait for another interval of time
            yield return new WaitForSeconds(intervalTime);
        }

    }
}
