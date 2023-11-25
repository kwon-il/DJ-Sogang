using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public double speed = 0d;
    public double speedSP = 0d;
    double currentTime = 0d;
    int idx = 0;
    bool noteActive = true;

    [SerializeField] Transform[] tfNoteAppear = null;
    [SerializeField] Transform[] tfNoteAppearSP = null;
    //[SerializeField] GameObject goNote = null;
    [SerializeField] TextAsset noteTime = null;
    [SerializeField] TextAsset noteTimeNEW = null;
    string[] timesALL = null;
    string[][] timesALLt = null;

    HpManager theHpManager;
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    Result theResult;
    AudioManager theAudioManager;

    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theHpManager = FindObjectOfType<HpManager>();
        theResult = FindObjectOfType<Result>();
        theAudioManager = FindObjectOfType<AudioManager>();

        timesALL = noteTimeNEW.text.Split("\n");
        timesALLt = new string[timesALL.Length][];
        for (int i = 0; i < timesALL.Length; i++) 
        {
            timesALLt[i] = timesALL[i].Split(" ");
        }
        theAudioManager.audioPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if(noteActive)
        {
            currentTime += UnityEngine.Time.deltaTime;

            if (idx < timesALLt.Length)
            {
                if (int.Parse(timesALLt[idx][1]) == 5 || int.Parse(timesALLt[idx][1]) == 6)
                {
                    if (Double.Parse(timesALLt[idx][0]) + speedSP <= currentTime)
                    {
                        GameObject t_noteSP = ObjectPool.instance.noteQueueSP.Dequeue();
                        int dfjk = int.Parse(timesALLt[idx][1]);
                        //Debug.Log(timesALLt[idx][1]);
                        t_noteSP.transform.position = tfNoteAppearSP[dfjk - 5].position;
                        t_noteSP.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        t_noteSP.SetActive(true);
                        theTimingManager.boxNoteListSP.Add(t_noteSP);

                        idx++;
                    }
                }
                else
                {
                    if (Double.Parse(timesALLt[idx][0]) + speed <= currentTime)
                    {
                        GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                        int dfjk = int.Parse(timesALLt[idx][1]);
                        //Debug.Log(timesALLt[idx][1]);
                        t_note.transform.position = tfNoteAppear[dfjk - 1].position;
                        t_note.SetActive(true);
                        //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                        //t_note.transform.SetParent(this.transform);
                        theTimingManager.boxNoteList.Add(t_note);

                        idx++;
                    }
                }
            }

            else
            {
                Invoke("playResult", 4);
                noteActive = false;
                //theResult.ShowResult();
            }
        }     
    }

    void playResult()
    {
        theResult.ShowResult(theHpManager.isDead);
    }
    private void DelayedFunction()
    {
        Debug.Log("Ready");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if(collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementHitEffect(3);
                theComboManager.ResetCombo();

                theTimingManager.judgementRecord[3]++;
                theHpManager.DecreaseHp(20);
            }
                
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
    

            //Destroy(collision.gameObject);
        }

        if (collision.CompareTag("NoteSP"))
        {
            if (collision.GetComponent<NoteSP>().GetNoteFlag())
            {
                theEffectManager.JudgementHitEffect(3);
                theComboManager.ResetCombo();

                theTimingManager.judgementRecord[3]++;
                theHpManager.DecreaseHp(20);
            }

            theTimingManager.boxNoteListSP.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueSP.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
         

            //Destroy(collision.gameObject);
        }
    }

    public void RemoveNote()
    {
        noteActive = false;
        theAudioManager.audioStop();

        for (int i = 0; i < theTimingManager.boxNoteList.Count; i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManager.boxNoteList[i]);
        }

        for (int i = 0; i < theTimingManager.boxNoteListSP.Count; i++)
        {
            theTimingManager.boxNoteListSP[i].SetActive(false);
            ObjectPool.instance.noteQueueSP.Enqueue(theTimingManager.boxNoteListSP[i]);
        }
    }

}
