using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
     public static string musicName;
     public static string myID="ff";
     public static int score;
     public static char rank;
     public static int levelIndex = 1;
     public static int speedIndex = 2;
     public static string _nickname;
     public const string GoogleScriptUrl = "https://script.google.com/macros/s/AKfycbw3KTtoWoJO8DMsyC5IKN2OP-WL8_WXMvoMPWIA1FOkhe_kUBgg37U0LDgwyQ-FvtRHow/exec";
     public delegate void NicknameChangedHandler(string newNickname);
     public static event NicknameChangedHandler OnNicknameChanged;

    // 닉네임 프로퍼티
    public static string nickname
     {
        get { return _nickname; }
        set
        {
            if (_nickname != value)
            {
                _nickname = value;
                OnNicknameChanged?.Invoke(_nickname);
            }
        }
    }
}