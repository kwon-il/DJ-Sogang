using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject> ();
    public List<GameObject> boxNoteListSP = new List<GameObject>();

    public int[] judgementRecord = new int[4];

    [SerializeField] RectTransform[] Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    [SerializeField] RectTransform[] noteBox = null;
    [SerializeField] float[] weight = null;

    [SerializeField] RectTransform[] timingRectSP = null;
    [SerializeField] RectTransform[] noteBoxSP = null;

    Vector2[] timingBoxs = null;
    Vector2[] timingBoxsX = null;

    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;
    HpManager theHpManager;
    AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager =  FindObjectOfType<ComboManager>();
        theHpManager = FindObjectOfType<HpManager>();
        sound = GetComponent<AudioSource>();

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center[i].localPosition.y - timingRect[i].rect.height / 2,
                              Center[i].localPosition.y + timingRect[i].rect.height / 2);
        }

        timingBoxsX = new Vector2[noteBox.Length];

        for (int i = 0; i < noteBox.Length; i++)
        {
            timingBoxsX[i].Set(noteBox[i].localPosition.x - Center[i].rect.width / 2,
                              noteBox[i].localPosition.x + Center[i].rect.width / 2);
        }
    }

    public void CheckTiming(KeyCode KC)
    {
        if(KC == KeyCode.D || KC == KeyCode.F || KC == KeyCode.J || KC == KeyCode.K)
        {
            for (int i = 0; i < boxNoteList.Count; i++)
            {
                float t_notePosY = boxNoteList[i].transform.localPosition.y;
                float t_notePosX = boxNoteList[i].transform.localPosition.x;


                if (KC == KeyCode.D && timingBoxsX[0].x <= t_notePosX && t_notePosX <= timingBoxsX[0].y)
                {
                    for (int x = 0; x < timingBoxs.Length; x++)
                    {
                        if (timingBoxs[x].x <= t_notePosY && t_notePosY <= timingBoxs[x].y)
                        {
                            boxNoteList[i].GetComponent<Note>().HideNote();
                            theEffect.NoteHitEffect(0);
                            boxNoteList.RemoveAt(i);
                            theEffect.JudgementHitEffect(x);
                            sound.Play();

                            theHpManager.IncreaseHp((int)(10 * weight[x]));
                            judgementRecord[x]++;
                            theScoreManager.IncreaseScore(x);
                            return;
                        }
                    }
                }
                else if (KC == KeyCode.F && timingBoxsX[1].x <= t_notePosX && t_notePosX <= timingBoxsX[1].y)
                {
                    for (int x = 0; x < timingBoxs.Length; x++)
                    {
                        if (timingBoxs[x].x <= t_notePosY && t_notePosY <= timingBoxs[x].y)
                        {
                            boxNoteList[i].GetComponent<Note>().HideNote();
                            theEffect.NoteHitEffect(1);
                            boxNoteList.RemoveAt(i);
                            theEffect.JudgementHitEffect(x);
                            sound.Play();

                            theHpManager.IncreaseHp((int)(10 * weight[x]));
                            judgementRecord[x]++;
                            theScoreManager.IncreaseScore(x);
                            return;
                        }
                    }
                }
                else if (KC == KeyCode.J && timingBoxsX[2].x <= t_notePosX && t_notePosX <= timingBoxsX[2].y)
                {
                    for (int x = 0; x < timingBoxs.Length; x++)
                    {
                        if (timingBoxs[x].x <= t_notePosY && t_notePosY <= timingBoxs[x].y)
                        {
                            boxNoteList[i].GetComponent<Note>().HideNote();
                            theEffect.NoteHitEffect(2);
                            boxNoteList.RemoveAt(i);
                            theEffect.JudgementHitEffect(x);
                            sound.Play();

                            theHpManager.IncreaseHp((int)(10 * weight[x]));
                            judgementRecord[x]++;
                            theScoreManager.IncreaseScore(x);
                            return;
                        }
                    }
                }
                else if (KC == KeyCode.K && timingBoxsX[3].x <= t_notePosX && t_notePosX <= timingBoxsX[3].y)
                {
                    for (int x = 0; x < timingBoxs.Length; x++)
                    {
                        if (timingBoxs[x].x <= t_notePosY && t_notePosY <= timingBoxs[x].y)
                        {
                            boxNoteList[i].GetComponent<Note>().HideNote();
                            theEffect.NoteHitEffect(3);
                            boxNoteList.RemoveAt(i);
                            theEffect.JudgementHitEffect(x);
                            sound.Play();

                            theHpManager.IncreaseHp((int)(10 * weight[x]));
                            judgementRecord[x]++;
                            theScoreManager.IncreaseScore(x);
                            return;
                        }
                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < boxNoteListSP.Count; i++)
            {      
                float t_noteScale = boxNoteListSP[i].transform.localScale.x;
                float t_notePosX = boxNoteListSP[i].transform.localPosition.x;


                if ((KC == KeyCode.S || KC == KeyCode.L) && noteBoxSP[0].localPosition.x + 1 >= t_notePosX && noteBoxSP[0].localPosition.x - 1 <= t_notePosX)
                {
                    int p = timingRectSP.Length / 2 - 1;
                    int q = timingRectSP.Length / 2;
                    for (int x = 0; x < timingRectSP.Length / 2; x++)
                    {
                        if (t_noteScale >= timingRectSP[p].localScale.x && t_noteScale <= timingRectSP[q].localScale.x)
                        {
                            boxNoteListSP[i].GetComponent<NoteSP>().HideNote();
                            theEffect.NoteHitEffect(4);
                            theEffect.NoteHitEffect(5);
                            boxNoteListSP.RemoveAt(i);
                            theEffect.JudgementHitEffect(x);
                            sound.Play();

                            theHpManager.IncreaseHp((int)(10 * weight[x]));
                            judgementRecord[x]++;
                            theScoreManager.IncreaseScore(x);
                            return;
                        }
                        p--;
                        q++;
                    }
                }              
            }
        }

        //theComboManager.ResetCombo();
       // theEffect.JudgementHitEffect(3);
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }
}
