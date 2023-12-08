//using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    double speed = 0d;
    double speedSP = 0d;
    float speedLONG = 0f;
    public float longNote = 0f;
    double currentTime = 0d;
    double[] longTime = {0d, 0d, 0d, 0d};
    double[] longTime2 = { 0d, 0d, 0d, 0d };
    double interval = 0.2d;
    int idx = 0;
    bool noteActive = true;
    int END_FLAG = 0;

    [SerializeField] Transform[] tfNoteAppear = null;
    [SerializeField] Transform[] tfNoteAppearSP = null;
    //[SerializeField] GameObject goNote = null;    
    [SerializeField] TextAsset noteTime = null;
    [SerializeField] TextAsset noteTimeNEW = null;

    [SerializeField] public Animator[] noteHitAnim = null;
    public string noteHit = "Note1";

    string[] timesALL = null;
    string[][] timesALLt = null;
    string[] times = null;
    string[][] timest = null;
    string currentMusicName = GlobalData.musicName;
    int level = GlobalData.levelIndex;
    UnityEngine.UI.Image tailImage;

    HpManager theHpManager;
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    Result theResult;
    AudioManager theAudioManager;
    ScoreManager theScoreManager;

    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theHpManager = FindObjectOfType<HpManager>();
        theResult = FindObjectOfType<Result>();
        theAudioManager = FindObjectOfType<AudioManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        // Resources/Photo 폴더에서 씬 이름과 일치하는 이미지를 불러옵니다
        //TextAsset imsi = Resources.Load<TextAsset>("NoteTime/" + currentMusicName);

        string levelStr;
        if (level == 0)
            levelStr = "easy";
        else if (level == 1)
            levelStr = "medium";
        else
            levelStr = "hard";


        int speedIDX = GlobalData.speedIndex;
        if (speedIDX == 0)
        {
            speed = 0.5d;
            speedSP = 0.8d;
            speedLONG = 0.76f;
        }
        else if (speedIDX == 1)
        {
            speed = 1.42d;
            speedSP = 1.58d;
            speedLONG = 1.5f;
        }
        else
        {
            speed = 1.76d;
            speedSP = 1.90d;
            speedLONG = 2.26f;
        }


        TextAsset imsi2 = Resources.Load<TextAsset>("NoteTime/" + currentMusicName+ "/" + levelStr);

        times = imsi2.text.Split("\n");
        timest = new string[times.Length][];
        for (int i = 0; i < times.Length; i++)
        {
            timest[i] = times[i].Split(" ");
        }

        for(int j = 0; j < 4; j++)
        {
            int idx = -1;
            int FLAG = 0;
            for (int i = 0; i < times.Length; i++)
            {
                if (FLAG == 0 && int.Parse(timest[i][j + 2]) == 1)
                {
                    idx = i;
                    FLAG = 1;
                }

                else if(FLAG == 1 && int.Parse(timest[i][j + 2]) != 1)
                {
                    double temp = Double.Parse(timest[i - 1][0]) - Double.Parse(timest[idx][0]);
                    //Debug.Log(temp);
                    timest[idx][j + 2] = temp.ToString();
                    FLAG = 0;
                }

                else if(FLAG == 1 && int.Parse(timest[i][j + 2]) == 1 && i == times.Length - 1)
                {
                    timest[i][j + 2] = "0";
                    double temp = Double.Parse(timest[i][0]) - Double.Parse(timest[idx][0]);
                    timest[idx][j + 2] = temp.ToString();
                    FLAG = 0;
                }
                else if(FLAG == 1 && int.Parse(timest[i][j + 2]) == 1)
                {
                    timest[i][j + 2] = "0";
                }
            }
        }

        /*
        for(int i = 0; i < timest.Length; i++)
        {
            Debug.Log(timest[i][0] + " " + timest[i][1] + " " + timest[i][2] + " " + timest[i][3] + " " + timest[i][4] + " " + timest[i][5]);
        }*/
        
        /*
        timesALL = imsi.text.Split("\n");
        timesALLt = new string[timesALL.Length][];
        for (int i = 0; i < timesALL.Length; i++) 
        {
            timesALLt[i] = timesALL[i].Split(" ");
        }*/

        theAudioManager.audioPlay();
    }

    // Update is called once per frame
    void Update()
    {
        if(noteActive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (theTimingManager.LongFLAG[i] == 1)
                {
                    Transform childTransform = theTimingManager.LongNoteList[i].transform.Find("tail");
                    Vector3 currentScale = childTransform.transform.localScale;
                    double p_Y = currentScale.y * 5 + theTimingManager.LongNoteList[i].transform.localPosition.y;

                    if (p_Y < theTimingManager.Center[i].localPosition.y)
                        theTimingManager.LongFLAG[i] = 0;

                    else
                    {
                        longTime[i] += Time.deltaTime;

                        if (longTime[i] >= interval)
                        {
                            theEffectManager.NoteHitEffect(i);
                            theEffectManager.JudgementHitEffect(0);
                            theTimingManager.sound.Play();
                            noteHitAnim[i].SetTrigger(noteHit);

                            theHpManager.IncreaseHp((int)(10 * theTimingManager.weight[0]));
                            theTimingManager.judgementRecord[0]++;
                            theScoreManager.IncreaseScore(0);

                            longTime[i] = 0d;
                        }
                    }
                }
                if (theTimingManager.LongFLAG[i] == 2)
                {
                    Transform childTransform = theTimingManager.LongNoteList[i].transform.Find("tail");
                    Vector3 currentScale = childTransform.transform.localScale;
                    double n_Y = theTimingManager.LongNoteList[i].transform.localPosition.y;
                    double p_Y = currentScale.y * 5 + theTimingManager.LongNoteList[i].transform.localPosition.y;

                    if (p_Y < -313.4d)
                        theTimingManager.LongFLAG[i] = 0;
                    else if (n_Y < -313.4d && p_Y > -313.4d)
                    {
                        longTime2[i] += Time.deltaTime;

                        if (longTime2[i] >= interval)
                        {
                            theEffectManager.JudgementHitEffect(3);
                            theComboManager.ResetCombo();

                            theTimingManager.judgementRecord[3]++;
                            theHpManager.DecreaseHp(20);

                            longTime2[i] = 0d;
                        }
                    }
                }
            }
        }        

        if (noteActive)
        {
            currentTime += UnityEngine.Time.deltaTime;

            if (idx < timest.Length)
            {
                int temp = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (Double.Parse(timest[idx][i + 1]) != 0)
                    {
                        temp++;
                    }
                }
                if(temp == 0)
                {
                    idx++;
                }
                else
                {
                    //Debug.Log("idx: "+idx+ "   temp: " + temp);
                    if (temp != 0)
                    {
                        if (Double.Parse(timest[idx][1]) == -1)
                        {
                            if (Double.Parse(timest[idx][0]) + speedSP <= currentTime)
                            {
                                GameObject t_noteSP = ObjectPool.instance.noteQueueSP.Dequeue();
                                GameObject t_noteSP2 = ObjectPool.instance.noteQueueSP.Dequeue();
                                //Debug.Log(timesALLt[idx][1]);
                                t_noteSP.transform.position = tfNoteAppearSP[0].position;
                                t_noteSP.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                t_noteSP.SetActive(true);
                                t_noteSP2.transform.position = tfNoteAppearSP[1].position;
                                t_noteSP2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                t_noteSP2.SetActive(true);
                                theTimingManager.boxNoteListSP.Add(t_noteSP);
                                theTimingManager.boxNoteListSP.Add(t_noteSP2);

                                temp--;
                                if (temp == 0)
                                    idx++;
                            }
                        }
                    }
                    if (temp != 0)
                    {
                        if (Double.Parse(timest[idx][2]) != 0)
                        {
                            if (Double.Parse(timest[idx][0]) + speed <= currentTime)
                            {
                                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                                //Debug.Log(timesALLt[idx][1]);
                                t_note.transform.position = tfNoteAppear[0].position;
                                Transform childTransform = t_note.transform.Find("tail");
                                childTransform.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                t_note.SetActive(true);

                                if(Double.Parse(timest[idx][2]) > 0)
                                {
                                    tailImage = childTransform.GetComponent<UnityEngine.UI.Image>();
                                    tailImage.enabled = true;
                                    Vector3 currentScale = childTransform.transform.localScale;
                                    childTransform.transform.localScale = new Vector3(currentScale.x * 1.0f, float.Parse(timest[idx][2]) * longNote * speedLONG, currentScale.z * 1.0f);

                                    Collider2D cCollider = childTransform.GetComponent<Collider2D>();
                                    //Collider2D pCollider = t_note.transform.GetComponent<Collider2D>();

                                    cCollider.enabled = true;
                                    //pCollider.enabled = false;
                                }

                                //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                                //t_note.transform.SetParent(this.transform);
                                theTimingManager.boxNoteList.Add(t_note);

                                temp--;
                                if (temp == 0)
                                    idx++;
                            }
                        }
                    }
                    if (temp != 0)
                    {
                        if (Double.Parse(timest[idx][3]) != 0)
                        {
                            if (Double.Parse(timest[idx][0]) + speed <= currentTime)
                            {
                                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                                //Debug.Log(timesALLt[idx][1]);
                                t_note.transform.position = tfNoteAppear[1].position;
                                Transform childTransform = t_note.transform.Find("tail");
                                childTransform.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                t_note.SetActive(true);

                                if (Double.Parse(timest[idx][3]) > 0)
                                {
                                    tailImage = childTransform.GetComponent<UnityEngine.UI.Image>();
                                    tailImage.enabled = true;
                                    Vector3 currentScale = childTransform.transform.localScale;
                                    childTransform.transform.localScale = new Vector3(currentScale.x * 1.0f, float.Parse(timest[idx][3]) * longNote * speedLONG, currentScale.z * 1.0f);

                                    Collider2D cCollider = childTransform.GetComponent<Collider2D>();
                                   // Collider2D pCollider = t_note.transform.GetComponent<Collider2D>();

                                    cCollider.enabled = true;
                                    //pCollider.enabled = false;
                                }
                                //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                                //t_note.transform.SetParent(this.transform);
                                theTimingManager.boxNoteList.Add(t_note);

                                temp--;
                                if (temp == 0)
                                    idx++;
                            }
                        }
                    }
                    if (temp != 0)
                    {
                        if (Double.Parse(timest[idx][4]) != 0)
                        {
                            if (Double.Parse(timest[idx][0]) + speed <= currentTime)
                            {
                                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                                //Debug.Log(timesALLt[idx][1]);
                                t_note.transform.position = tfNoteAppear[2].position;
                                Transform childTransform = t_note.transform.Find("tail");
                                childTransform.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                t_note.SetActive(true);

                                if (Double.Parse(timest[idx][4]) > 0)
                                {
                                    tailImage = childTransform.GetComponent<UnityEngine.UI.Image>();
                                    tailImage.enabled = true;
                                    Vector3 currentScale = childTransform.transform.localScale;
                                    childTransform.transform.localScale = new Vector3(currentScale.x * 1.0f, float.Parse(timest[idx][4]) * longNote * speedLONG, currentScale.z * 1.0f);

                                    Collider2D cCollider = childTransform.GetComponent<Collider2D>();
                                    //Collider2D pCollider = t_note.transform.GetComponent<Collider2D>();

                                    cCollider.enabled = true;
                                    //pCollider.enabled = false;
                                }
                                //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                                //t_note.transform.SetParent(this.transform);
                                theTimingManager.boxNoteList.Add(t_note);

                                temp--;                             
                                if (temp == 0)
                                    idx++;
                            }
                        }
                    }
                    if (temp != 0)
                    {
                        if (Double.Parse(timest[idx][5]) != 0)
                        {
                            if (Double.Parse(timest[idx][0]) + speed <= currentTime)
                            {
                                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                                //Debug.Log(timesALLt[idx][1]);
                                t_note.transform.position = tfNoteAppear[3].position;
                                Transform childTransform = t_note.transform.Find("tail");
                                childTransform.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                t_note.SetActive(true);

                                if (Double.Parse(timest[idx][5]) > 0)
                                {
                                    tailImage = childTransform.GetComponent<UnityEngine.UI.Image>();
                                    tailImage.enabled = true;
                                    Vector3 currentScale = childTransform.transform.localScale;
                                    childTransform.transform.localScale = new Vector3(currentScale.x * 1.0f, float.Parse(timest[idx][5]) * longNote * speedLONG, currentScale.z * 1.0f);

                                    Collider2D cCollider = childTransform.GetComponent<Collider2D>();
                                    //Collider2D pCollider = t_note.transform.GetComponent<Collider2D>();

                                    cCollider.enabled = true;
                                   // pCollider.enabled = false;
                                }
                                //GameObject t_note = Instantiate(goNote, tfNoteAppear[dfjk].position, Quaternion.identity);
                                //t_note.transform.SetParent(this.transform);
                                theTimingManager.boxNoteList.Add(t_note);

                                temp--;
                                if (temp == 0)
                                    idx++;
                            }
                        }
                    }

                    //Debug.Log("idx: " + idx + "   temp: " + temp);
                }                                    
            }

            else
            {
                if(END_FLAG == 0)
                {
                    Invoke("playResult", 4);
                    END_FLAG = 1;
                }
                
                //noteActive = false;
                //theResult.ShowResult();
            }
        }     
    }

    void playResult()
    {
        noteActive = false;
        StartCoroutine(theResult.ShowResult(theHpManager.isDead));
    }

    private void DelayedFunction()
    {
        Debug.Log("Ready");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            Transform childTransform = collision.GetComponent<Note>().transform.Find("tail");
            Collider2D cCollider = childTransform.GetComponent<Collider2D>();

            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementHitEffect(3);
                theComboManager.ResetCombo();

                theTimingManager.judgementRecord[3]++;
                theHpManager.DecreaseHp(20);

                if (cCollider.enabled == true)
                {
                    Transform paTs = collision.GetComponent<Note>().transform;

                    if (theTimingManager.timingBoxsX[0].x <= paTs.localPosition.x && paTs.localPosition.x <= theTimingManager.timingBoxsX[0].y)
                    {
                        theTimingManager.LongFLAG[0] = 2;
                        theTimingManager.LongNoteList[0] = collision.gameObject;
                    }
                    else if(theTimingManager.timingBoxsX[1].x <= paTs.localPosition.x && paTs.localPosition.x <= theTimingManager.timingBoxsX[1].y)
                    {
                        theTimingManager.LongFLAG[1] = 2;
                        theTimingManager.LongNoteList[1] = collision.gameObject;
                    }
                    else if (theTimingManager.timingBoxsX[2].x <= paTs.localPosition.x && paTs.localPosition.x <= theTimingManager.timingBoxsX[2].y)
                    {
                        theTimingManager.LongFLAG[2] = 2;
                        theTimingManager.LongNoteList[2] = collision.gameObject;
                    }
                    else if (theTimingManager.timingBoxsX[3].x <= paTs.localPosition.x && paTs.localPosition.x <= theTimingManager.timingBoxsX[3].y)
                    {
                        theTimingManager.LongFLAG[3] = 2;
                        theTimingManager.LongNoteList[3] = collision.gameObject;
                    }
                }
            }          

            if(cCollider.enabled == false)
            {
                theTimingManager.boxNoteList.Remove(collision.gameObject);
                ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        
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

        if (collision.CompareTag("tail"))
        {
            Transform parentTransform = collision.transform.parent;

            theTimingManager.boxNoteList.Remove(parentTransform.gameObject);
            ObjectPool.instance.noteQueue.Enqueue(parentTransform.gameObject);
            parentTransform.gameObject.SetActive(false);

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
