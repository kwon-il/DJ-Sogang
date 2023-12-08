[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value, nickname, rank, id;
    public int cnt, score;
    public PlayerData[] data;
}
[System.Serializable]
public class PlayerData
{    
    public string id;
    public int score;
    public string rank;
}
