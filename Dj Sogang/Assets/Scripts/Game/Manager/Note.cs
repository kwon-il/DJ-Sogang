using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    float noteSpeed = 0;

    UnityEngine.UI.Image noteImage;
    UnityEngine.UI.Image tailImage;

    void OnEnable()
    {
        int speedIDX = GlobalData.speedIndex;

        if (speedIDX == 0)
        {
            noteSpeed = 300;
        }
        else if (speedIDX == 1)
        {
            noteSpeed = 600;
        }
        else
        {
            noteSpeed = 900;
        }

        if (noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();

        Transform childTransform = transform.Find("tail");
        if (tailImage == null)
            tailImage = childTransform.GetComponent<UnityEngine.UI.Image>();

        tailImage.enabled = false;
        noteImage.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.down * noteSpeed * UnityEngine.Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool isTail()
    {
        return tailImage.enabled;
    }

    public void tailColorUP()
    {
        Color color = new Color();
        color = tailImage.color;
        
        ColorUtility.TryParseHtmlString("#6CDCDE", out color);
        color.a = 0.3f;
        tailImage.color = color;
    }

    public void tailColorDown()
    {
        Color color = new Color();
        color = tailImage.color;

        ColorUtility.TryParseHtmlString("#CACACA", out color);
        color.a = 0.3f;
        tailImage.color = color;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
