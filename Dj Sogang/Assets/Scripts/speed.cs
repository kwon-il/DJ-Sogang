using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{   
    public Text speedText;
    private int lastspeed;

    void Start()
    {
        GlobalData.OnSpeedChanged += updatespeedtext;
        updatespeedtext(GlobalData.speedIndex);

    }
    void Update()
    {
        // Check if the nickname has changed
        if (GlobalData.speedIndex != lastspeed)
        {
            updatespeedtext(GlobalData.speedIndex);
            lastspeed = GlobalData.speedIndex;
        }
    }
    void updatespeedtext(int newspeed)
    {
        if (speedText != null && newspeed >= 0)
        {
            speedText.text = (GlobalData.speedIndex + 1).ToString();
        }
    }
        void OnDestroy()
    {
        // 이벤트 구독 해제
        GlobalData.OnSpeedChanged -= updatespeedtext;
    }

    // Optional: Add an Update method or other methods to handle dynamic changes in speed.
}