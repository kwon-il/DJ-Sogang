using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
     public static string musicName;
     public static string myID="aa";
     public static string _nickname;
     public const string GoogleScriptUrl = "https://script.google.com/macros/s/AKfycbzm3NuHTCUh6PO7CkHLp9vyECP7QtLsMma1Cfmmz8QVhbSo1gubIIQwDOm0mctDofHG/exec";
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