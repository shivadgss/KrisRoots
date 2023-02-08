using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TimeTravel : MonoBehaviour
{
    public GameObject PastObjects;
    public GameObject PresentObjects;
    public bool InPresent = true;
    public GameObject Camera;
    
    public float timeInPast = 10f;
    public float timeLeft;

    public Image time;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeInPast;
    }

    // Update is called once per frame
    void Update()
    {
        if (InPresent)
        {            
            PastObjects.SetActive(false);
            PresentObjects.SetActive(true);
            Camera.GetComponent<PostProcessVolume>().enabled = false;
            if (timeLeft < timeInPast)
            {
                timeLeft += Time.deltaTime/2;
            }
        }
        if (!InPresent)
        { 
            PastObjects.SetActive(true);
            PresentObjects.SetActive(false);
            Camera.GetComponent<PostProcessVolume>().enabled = true;
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || timeLeft < 0)
        {
            InPresent = !InPresent;
        }
        time.fillAmount = timeLeft * 0.1f;
    }
}
