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
     public static int speedIndex = 1;
     public static string _nickname;
     public const string GoogleScriptUrl = "https://script.google.com/macros/s/AKfycbyF2zKwh7s5DxTYY6u-DpDcKqD-HQAHXLagCAXAXvrQDhQt7cnL03YJc8SbqAenOI-Qkg/exec";
     public delegate void NicknameChangedHandler(string newNickname);
     public delegate void SpeedChangedHandler(int newspeed);
     public static event NicknameChangedHandler OnNicknameChanged;
     public static event SpeedChangedHandler OnSpeedChanged;
     public static Dictionary<string, SongScore> songScores = new Dictionary<string, SongScore>();


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

    [System.Serializable]
    public class SongScore
    {
        public int score;
        public string rank;

        public SongScore(int score, string rank)
        {
            this.score = score;
            this.rank = rank;
        }
    }
}