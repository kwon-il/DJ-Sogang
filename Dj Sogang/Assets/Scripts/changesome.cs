using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class changesome : MonoBehaviour
{
    GoogleData myData = new GoogleData();
    string URL = GlobalData.GoogleScriptUrl;
    public GameObject inputWindow;
    public GameObject speedWindow;
    public InputField nicknameInput;
    public InputField speedInput;
    public Button submitButtonn;
    public Button submitButton; 
    public Button exitButton;
    public Button exitButtonn;
    public string tempname;
    public string tempspeed;
    void Start()
    {
        inputWindow.SetActive(false);
        speedWindow.SetActive(false);

        // Modify submitButton listener to only handle submission
        submitButton.onClick.AddListener(SubmitNicknameChange);
        submitButtonn.onClick.AddListener(changespeed);

        // Add listener for exit button
        exitButton.onClick.AddListener(CloseInputWindow);
        exitButtonn.onClick.AddListener(CloseSpeedWindow);
    }

    void Update()
    {
        // Toggle window visibility on F4 key press
        if (Input.GetKeyDown(KeyCode.F4))
        {
            inputWindow.SetActive(!inputWindow.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            speedWindow.SetActive(!speedWindow.activeSelf);
        }

        // Check for ESC key to close the window
        if (Input.GetKeyDown(KeyCode.Escape) && inputWindow.activeSelf)
        {
            CloseInputWindow();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && speedWindow.activeSelf)
        {
            CloseSpeedWindow();
        }
    }
    void changespeed()
    {
        tempspeed = speedInput.text;
        //print(tempspeed);
        speedWindow.SetActive(false);
        if (int.TryParse(speedInput.text, out int parsedSpeed))
        {
            GlobalData.speedIndex = parsedSpeed - 1;
            //print(GlobalData.speedIndex);
        }
    }
    void SubmitNicknameChange()
    {
        StartCoroutine(changenickname());
    }

    IEnumerator changenickname()
{
    inputWindow.SetActive(false);
    tempname = nicknameInput.text;
    WWWForm form = new WWWForm();
    form.AddField("order", "changename");
    form.AddField("id", GlobalData.myID);
    form.AddField("nickname", tempname);

    using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            ProcesschangenameResponse(www.downloadHandler.text);
        }
    }
}
    void ProcesschangenameResponse(string jsonResponse)
    {   
        Debug.Log("Raw JSON Response: " + jsonResponse);

        GoogleData responseData = JsonUtility.FromJson<GoogleData>(jsonResponse);

        //print(responseData.msg);
        //print(responseData.result);
        //print(responseData.nickname);
        if (responseData.result == "OK")
        {
            // Handle successful response
            print(responseData.nickname);
            GlobalData.nickname = tempname;
            Debug.Log("Nickname received: " + GlobalData.nickname);
        }
        else if (responseData.msg == "Nickname found")
        {
            // Handle specific non-error case
            Debug.Log(responseData.msg);
        }
        else
        {
            // Handle actual error response
            Debug.LogError("Error: " + responseData.msg);
        }
    }
    void CloseInputWindow()
    {
        inputWindow.SetActive(false);
    }    
    void CloseSpeedWindow()
    {
        speedWindow.SetActive(false);
    }
}