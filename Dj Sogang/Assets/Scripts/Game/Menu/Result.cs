using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;
    [SerializeField] UnityEngine.UI.Text[] txtCount = null;
    [SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] UnityEngine.UI.Text txtMaxCombo = null;
    [SerializeField] UnityEngine.UI.Image ABCImage = null;
    [SerializeField] Sprite[] ABCSprite = null;
    GoogleData myData = new GoogleData();
    string URL = GlobalData.GoogleScriptUrl;

    ScoreManager theScore = null;
    ComboManager theCombo = null;
    TimingManager theTiming = null;

    Animator myAnim;
    string animABC = "ABC";

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
    }

    public IEnumerator ShowResult(bool isDead)
    {
        goUI.SetActive(true);
        myAnim.SetTrigger(animABC);

        for(int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }

        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_score = theScore.GetCurrentScore();
        int t_maxcombo = theCombo.GetMaxCombo();
        char rank;

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }
        txtScore.text = string.Format("{0:#,##0}", t_score);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxcombo);

        if(isDead)
        {
            ABCImage.sprite = ABCSprite[6];
            rank = 'F';
        }
        else
        {
            if (int.Parse(txtCount[3].text) < 5)
            {
                ABCImage.sprite = ABCSprite[0];
                rank = 'S';
            }
            else if (int.Parse(txtCount[3].text) < 10)
            {
                ABCImage.sprite = ABCSprite[1];
                rank = 'A';
            }
            else if (int.Parse(txtCount[3].text) < 20)
            {
                ABCImage.sprite = ABCSprite[2];
                rank = 'B';
            }
            else if (int.Parse(txtCount[3].text) < 30)
            {
                ABCImage.sprite = ABCSprite[3];
                rank = 'C';
            }
            else if (int.Parse(txtCount[3].text) < 40)
            {
                ABCImage.sprite = ABCSprite[4];
                rank = 'D';
            }
            else if (int.Parse(txtCount[3].text) < 50)
            {
                ABCImage.sprite = ABCSprite[5];
                rank = 'E';
            }
            else
            {
                ABCImage.sprite = ABCSprite[6];
                rank = 'F';
            }
        }

        GlobalData.score = t_score;
        GlobalData.rank = rank;


        WWWForm form = new WWWForm();
        form.AddField("order", "savescore");
        form.AddField("id", GlobalData.myID);
        form.AddField("musicname", GlobalData.musicName);
        form.AddField("score", GlobalData.score);
        form.AddField("rank", GlobalData.rank.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                UnityEngine.Debug.LogError("Error: " + www.error);
            }
            else
            {
                savescore(www.downloadHandler.text);
            }
        }
    }
    void savescore(string jsonResponse)
    {   
        UnityEngine.Debug.Log("Raw JSON Response: " + jsonResponse);

        GoogleData responseData = JsonUtility.FromJson<GoogleData>(jsonResponse);

        if (responseData.result == "OK")
        {
            // Handle successful response
            UnityEngine.Debug.Log("Nickname received: ");
        }
        else
        {
            // Handle actual error response
            UnityEngine.Debug.LogError("Error: " + responseData.msg);
        }
    }
}