using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class idcheck : MonoBehaviour
{
    public Text idText; // Reference to the Text component

    void Start()
    {
        // Check if the text component is assigned
        if (idText != null)
        {
            // Assign the myID value to the text component
            idText.text = GlobalData.nickname;
            print(idText.text);
        }
        else
        {
            Debug.LogError("Text component not assigned to IDDisplay script.");
        }
    }
}
