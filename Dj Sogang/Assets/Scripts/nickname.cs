using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class nickname : MonoBehaviour
{   
    GoogleData myData = new GoogleData();
    string URL = GlobalData.GoogleScriptUrl;

    void Start()
    {
        StartCoroutine(GetNickname());
    }

    IEnumerator GetNickname()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getname");
        form.AddField("id", GlobalData.myID);
        //print(GlobalData.myID);
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching nickname: " + www.error);
            }
            else
            {   
                ProcessNicknameResponse(www.downloadHandler.text);
            }
        }
    }

    void ProcessNicknameResponse(string jsonResponse)
    {   
        //Debug.Log("Raw JSON Response: " + jsonResponse);

        GoogleData responseData = JsonUtility.FromJson<GoogleData>(jsonResponse);

        //print(responseData.msg);
        //print(responseData.result);
        //print(responseData.nickname);
        if (responseData.result == "OK")
        {
            // Handle successful response
            GlobalData.nickname = responseData.nickname;
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
}
