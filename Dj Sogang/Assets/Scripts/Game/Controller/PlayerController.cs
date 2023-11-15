using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    Color[] color = new Color[4];
      

    [SerializeField] UnityEngine.UI.Image[] contain = null;
    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        

        for(int i = 0; i < contain.Length; i++)
        {
            color[i] = contain[i].GetComponent<Image>().color;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            ColorUtility.TryParseHtmlString("#57767B", out color[0]);
            color[0].a = 0.8f;
            contain[0].GetComponent<Image>().color = color[0];

            theTimingManager.CheckTiming(KeyCode.D);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            ColorUtility.TryParseHtmlString("#57767B", out color[1]);
            color[1].a = 0.8f;
            contain[1].GetComponent<Image>().color = color[1];

            theTimingManager.CheckTiming(KeyCode.F);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ColorUtility.TryParseHtmlString("#57767B", out color[2]);
            color[2].a = 0.8f;
            contain[2].GetComponent<Image>().color = color[2];

            theTimingManager.CheckTiming(KeyCode.J);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ColorUtility.TryParseHtmlString("#57767B", out color[3]);
            color[3].a = 0.8f;
            contain[3].GetComponent<Image>().color = color[3];

            theTimingManager.CheckTiming(KeyCode.K);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[0]);
            color[0].a = 0.8f;
            contain[0].GetComponent<Image>().color = color[0];
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[1]);
            color[1].a = 0.8f;
            contain[1].GetComponent<Image>().color = color[1];
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[2]);
            color[2].a = 0.8f;
            contain[2].GetComponent<Image>().color = color[2];
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[3]);
            color[3].a = 0.8f;
            contain[3].GetComponent<Image>().color = color[3];
        }


        

    }
}
