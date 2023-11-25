using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] UnityEngine.UI.Text[] txtCount = null;
    [SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] UnityEngine.UI.Text txtMaxCombo = null;

    [SerializeField] UnityEngine.UI.Image ABCImage = null;
    [SerializeField] Sprite[] ABCSprite = null;

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

    public void ShowResult(bool isDead)
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

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }
        txtScore.text = string.Format("{0:#,##0}", t_score);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxcombo);

        if(isDead)
        {
            ABCImage.sprite = ABCSprite[6];
        }
        else
        {
            if (int.Parse(txtCount[3].text) < 5)
            {
                ABCImage.sprite = ABCSprite[0];
            }
            else if (int.Parse(txtCount[3].text) < 10)
            {
                ABCImage.sprite = ABCSprite[1];
            }
            else if (int.Parse(txtCount[3].text) < 20)
            {
                ABCImage.sprite = ABCSprite[2];
            }
            else if (int.Parse(txtCount[3].text) < 30)
            {
                ABCImage.sprite = ABCSprite[3];
            }
            else if (int.Parse(txtCount[3].text) < 40)
            {
                ABCImage.sprite = ABCSprite[4];
            }
            else if (int.Parse(txtCount[3].text) < 50)
            {
                ABCImage.sprite = ABCSprite[5];
            }
            else
            {
                ABCImage.sprite = ABCSprite[6];
            }
        }      
    }
}
