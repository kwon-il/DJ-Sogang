using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] UnityEngine.UI.Text txtScore = null;

    [SerializeField] int increaseScore = 10;
    int currentScore = 0;

    [SerializeField] float[] weight = null;
    [SerializeField] int comboBonusScore = 10;

    Animator myAnim;
    string animScoreUp = "Score";

    ComboManager theCombo;

    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();

        currentScore = 0;
        txtScore.text = "0";
    }

    // Update is called once per frame
    public void IncreaseScore(int p_Judgement)
    {
        theCombo.IncreaseCombo();

        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        int t_increaseScore = increaseScore + t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_Judgement]);

        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        myAnim.SetTrigger(animScoreUp);
    }
}
