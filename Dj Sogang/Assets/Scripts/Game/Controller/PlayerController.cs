using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    HpManager theHpManager;
    Color[] color = new Color[6];
      

    [SerializeField] UnityEngine.UI.Image[] contain = null;
    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theHpManager = FindObjectOfType<HpManager>();

        for (int i = 0; i < contain.Length; i++)
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
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.L))
        {
            ColorUtility.TryParseHtmlString("#4AE5F1", out color[4]);
            color[4].a = 1.0f;
            contain[4].GetComponent<Image>().color = color[4];

            ColorUtility.TryParseHtmlString("#4AE5F1", out color[5]);
            color[5].a = 1.0f;
            contain[5].GetComponent<Image>().color = color[5];

            theTimingManager.CheckTiming(KeyCode.S);
            theTimingManager.CheckTiming(KeyCode.L);
        }


        if (Input.GetKeyUp(KeyCode.D))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[0]);
            color[0].a = 0.8f;
            contain[0].GetComponent<Image>().color = color[0];

            if(theTimingManager.LongFLAG[0] == 1)
            {
                theTimingManager.LongFLAG[0] = 2;
                theTimingManager.LongNoteList[0].GetComponent<Note>().tailColorDown();
            }      
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[1]);
            color[1].a = 0.8f;
            contain[1].GetComponent<Image>().color = color[1];

            if (theTimingManager.LongFLAG[1] == 1)
            {
                theTimingManager.LongFLAG[1] = 2;
                theTimingManager.LongNoteList[1].GetComponent<Note>().tailColorDown();
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[2]);
            color[2].a = 0.8f;
            contain[2].GetComponent<Image>().color = color[2];

            if (theTimingManager.LongFLAG[2] == 1)
            {
                theTimingManager.LongFLAG[2] = 2;
                theTimingManager.LongNoteList[2].GetComponent<Note>().tailColorDown();
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            ColorUtility.TryParseHtmlString("#222222", out color[3]);
            color[3].a = 0.8f;
            contain[3].GetComponent<Image>().color = color[3];

            if (theTimingManager.LongFLAG[3] == 1)
            {
                theTimingManager.LongFLAG[3] = 2;
                theTimingManager.LongNoteList[3].GetComponent<Note>().tailColorDown();
            }
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.L))
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out color[4]);
            color[4].a = 1.0f;
            contain[4].GetComponent<Image>().color = color[4];

            ColorUtility.TryParseHtmlString("#FFFFFF", out color[5]);
            color[5].a = 1.0f;
            contain[5].GetComponent<Image>().color = color[5];
        }

    }
}
