using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject> ();

    [SerializeField] RectTransform[] Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    [SerializeField] RectTransform[] noteBox = null;
    Vector2[] timingBoxs = null;
    Vector2[] timingBoxsX = null;

    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;


    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager =  FindObjectOfType<ComboManager>();

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
        for(int i = 0;i < boxNoteList.Count;i++)
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

                        theScoreManager.IncreaseScore(x);
                        return;
                    }
                }
            }
            
        }

        theComboManager.ResetCombo();
        theEffect.JudgementHitEffect(3);
    }
}
