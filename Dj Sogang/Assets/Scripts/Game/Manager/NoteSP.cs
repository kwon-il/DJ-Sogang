using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSP : MonoBehaviour
{
    float noteSPSpeed = 0.0f;

    UnityEngine.UI.Image noteImage;

    void OnEnable()
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<UnityEngine.UI.Image>();
        }

        int speedIDX = GlobalData.speedIndex;

        if (speedIDX == 0)
        {
            noteSPSpeed = 0.3f;
        }
        else if (speedIDX == 1)
        {
            noteSPSpeed = 0.6f;
        }
        else
        {
            noteSPSpeed = 0.9f;
        }

        noteImage.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(noteSPSpeed, noteSPSpeed, noteSPSpeed) * UnityEngine.Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
