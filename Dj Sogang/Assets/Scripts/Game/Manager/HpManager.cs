using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public bool isDead = false;

    int maxHP = 360;
    int currentHP = 360;

    [SerializeField] RectTransform hpImage = null;

    Result theResult;
    Animator myAnim;
    NoteManager theNoteManager;
    string animHp = "Hp";

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        theResult = FindObjectOfType<Result>();
        theNoteManager = FindObjectOfType<NoteManager>();
    }

    public void DecreaseHp(int p_num)
    {
        currentHP -= p_num;

        hpImage.sizeDelta = new Vector2(currentHP, 27);
        myAnim.SetTrigger(animHp);

        if (currentHP <= 0)
        {
            isDead = true;
            theNoteManager.RemoveNote();
            Invoke("playResult", 1);
        }
    }
    public void IncreaseHp(int p_num)
    {
        if(currentHP < maxHP)
        {
            currentHP += p_num;

            if(currentHP > maxHP)
            {
                currentHP = maxHP;
            }

            hpImage.sizeDelta = new Vector2(currentHP, 27);
            //myAnim.SetTrigger(animHp);
        }    
    }
    void playResult()
    {
        StartCoroutine(theResult.ShowResult(isDead));
    }

    public bool IsDead()
    {
        return isDead;
    }
}
