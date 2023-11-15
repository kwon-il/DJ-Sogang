using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] UnityEngine.UI.Text txtCombo = null;

    int currentCombo = 0;

    Animator myAnim;
    string animCombo = "Combo";

    private void Start()
    {
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    // Start is called before the first frame update
    public void IncreaseCombo(int p_num = 1)
    {
        myAnim = GetComponent<Animator>();
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if(currentCombo > 1)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);

            myAnim.SetTrigger(animCombo);
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }
}
