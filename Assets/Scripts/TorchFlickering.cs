using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlickering : MonoBehaviour
{
    [Range(0.5f, 3f)]
    public float intensityMin;
    [Range(0.5f, 3f)]
    public float intensityMax;
    private float intensity;
    private float timer;
    private float t;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0.2f, 0.5f);
        // StartCoroutine(Flicker(timer));
       // GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = intensity;
    }

    // Update is called once per frame
    
    private void Update()
    {

        intensity = Mathf.Lerp(intensityMin, intensityMax,t);
        t += 0.5f * Time.deltaTime;
        GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = intensity;
        if (intensity == intensityMax)
        {
            float temp = intensityMax;
            intensityMax = intensityMin;
            intensityMin = temp;
            t = 0f;
        
        }
    }

    IEnumerator Flicker(float time)
    {

       
        timer = Random.Range(0.2f, 0.5f); 
        yield return new WaitForSeconds(time);
        GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = intensity;
        intensity = Random.Range(intensityMin, intensityMax);
        StartCoroutine(Flicker(timer));
    }

}
