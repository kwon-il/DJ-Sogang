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
    public InputField nicknameInput;
    public Button submitButton; 
    public Button exitButton;
    public string tempname;
    void Start()
    {
        inputWindow.SetActive(false);
            // inputWindow의 Canvas 설정
    Canvas canvas = inputWindow.GetComponent<Canvas>();
    if (canvas == null) {
        canvas = inputWindow.AddComponent<Canvas>();
    }
    canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 항상 최상위에 표시
    canvas.sortingOrder = 100; // 높은 순서 값

    // Canvas Group 추가
    CanvasGroup canvasGroup = inputWindow.GetComponent<CanvasGroup>();
    if (canvasGroup == null) {
        canvasGroup = inputWindow.AddComponent<CanvasGroup>();
    }
    canvasGroup.blocksRaycasts = true;

        // Modify submitButton listener to only handle submission
        submitButton.onClick.AddListener(SubmitNicknameChange);

        // Add listener for exit button
        exitButton.onClick.AddListener(CloseInputWindow);
    }

    void Update()
    {
        // Toggle window visibility on F4 key press
        if (Input.GetKeyDown(KeyCode.F4))
        {
            inputWindow.SetActive(!inputWindow.activeSelf);
        }

        // Check for ESC key to close the window
        if (Input.GetKeyDown(KeyCode.Escape) && inputWindow.activeSelf)
        {
            CloseInputWindow();
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
}
