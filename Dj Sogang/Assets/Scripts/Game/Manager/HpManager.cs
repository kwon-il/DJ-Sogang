using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    int maxHP = 360;
    int currentHP = 360;

    [SerializeField] RectTransform hpImage = null;

    Animator myAnim;
    string animHp = "Hp";

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    public void DecreaseHp(int p_num)
    {
        currentHP -= p_num;

        hpImage.sizeDelta = new Vector2(currentHP, 27);
        myAnim.SetTrigger(animHp);

        if (currentHP <= 0)
        {
            Debug.Log("»ç¸Á");
        }
    }
}
