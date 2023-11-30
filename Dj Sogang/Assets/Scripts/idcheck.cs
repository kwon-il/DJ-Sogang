using UnityEngine;
using UnityEngine.UI;

public class idcheck : MonoBehaviour
{
    public Text idText; // Reference to the Text component
    private string lastNickname; // To keep track of the last nickname

    void Start()
    {
        UpdateNicknameText();
    }

    void Update()
    {
        // Check if the nickname has changed
        if (GlobalData.nickname != lastNickname)
        {
            UpdateNicknameText();
        }
    }

    void UpdateNicknameText()
    {
        // Check if the text component is assigned
        if (idText != null)
        {
            // Assign the myID value to the text component
            idText.text = GlobalData.nickname;
            lastNickname = GlobalData.nickname; // Update the last nickname
            print(idText.text);
        }
        else
        {
            Debug.LogError("Text component not assigned.");
        }
    }
}