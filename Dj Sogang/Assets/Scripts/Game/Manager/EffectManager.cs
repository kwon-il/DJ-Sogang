using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator[] noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator judgementHitAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;

    public void JudgementHitEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementHitAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect(int i)
    {
        noteHitAnimator[i].SetTrigger(hit);
    }

}
