using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public double speed = 0d;
    double currentTime = 0d;
    int idx = 0;
    int dfjk = 0;

    [SerializeField] Transform[] tfNoteAppear = null;
    [SerializeField] Transform[] tfNoteAppearSP = null;
    //[SerializeField] GameObject goNote = null;
    [SerializeField] TextAsset noteTime = null;
    string[] times = null;


    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        theComboManager =FindObjectOfType<ComboManager>();

        times = noteTime.text.Split("\n");
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += UnityEngine.Time.deltaTime;

        if(idx < times.Length)
        {
            if (Double.Parse(times[idx])+speed <= currentTime)
            {
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear[dfjk].position;
                t_note.SetActive(true);
                //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                //t_note.transform.SetParent(this.transform);
                theTimingManager.boxNoteList.Add(t_note);

                if(dfjk == 0)
                {
                    GameObject t_noteSP = ObjectPool.instance.noteQueueSP.Dequeue();
                    t_noteSP.transform.position = tfNoteAppearSP[0].position;
                    t_noteSP.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    t_noteSP.SetActive(true);                
                    //theTimingManager.boxNoteList.Add(t_noteSP);
                }

                if (dfjk == 1)
                {
                    GameObject t_noteSP = ObjectPool.instance.noteQueueSP.Dequeue();
                    t_noteSP.transform.position = tfNoteAppearSP[1].position;
                    t_noteSP.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    t_noteSP.SetActive(true);
                    //theTimingManager.boxNoteList.Add(t_noteSP);
                }

                idx++;
                dfjk++;
                if (dfjk >= tfNoteAppear.Length)
                {
                    dfjk = 0;
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if(collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementHitEffect(3);
                theComboManager.ResetCombo();
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
                //theEffectManager.JudgementHitEffect(3);
                //theComboManager.ResetCombo();
            }

            //theTimingManager.boxNoteList.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueSP.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);

            //Destroy(collision.gameObject);
        }
    }

}
