using UnityEngine;
using UnityEngine.UI;

public class idcheck : MonoBehaviour
{
    public Text idText; // Reference to the Text component
    private string lastNickname; // To keep track of the last nickname

    void Start()
    {
        GlobalData.OnNicknameChanged += UpdateNicknameText; // 이벤트 구독
        UpdateNicknameText(GlobalData.nickname); // 초기 텍스트 업데이트
    }

    void Update()
    {
        // Check if the nickname has changed
        if (GlobalData.nickname != lastNickname)
        {
            UpdateNicknameText(GlobalData.nickname);
        }
    }

    void UpdateNicknameText(string newNickname)
    {
        if (idText != null)
        {
            idText.text = newNickname;
        }
        else
        {
            Debug.LogError("Text component not assigned."); 
        }
    }
    void OnDestroy()
    {
        // 이벤트 구독 해제
        GlobalData.OnNicknameChanged -= UpdateNicknameText;
    }
}