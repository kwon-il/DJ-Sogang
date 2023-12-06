using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
     public static string musicName;
     public static string myID="aa";
     public static int score;
     public static char rank;
     public static int levelIndex=0;


     public static string _nickname;
     public const string GoogleScriptUrl = "https://script.google.com/macros/s/AKfycbzWRdu0-zcfAHlLe282mu_s_TMSvpqmZza8m_CzdqU8ttIqcKgjIbU2kBICqWhXupB8Aw/exec";
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