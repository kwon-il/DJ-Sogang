using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSP : MonoBehaviour
{
    UnityEngine.UI.Image noteImage;

    void OnEnable()
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<UnityEngine.UI.Image>();
        }
            
        noteImage.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f) * UnityEngine.Time.deltaTime;
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
