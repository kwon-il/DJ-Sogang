using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    GoogleData myData = new GoogleData();
    string URL = GlobalData.GoogleScriptUrl;
    string id, pass;

    public GoogleData GD;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField ValueInput;
    public Button loginButton;
    public Button RegisterButton; // ���� �̸� ����
    public Text loginStatusText;

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Tab))
    {
        if (usernameInput.isFocused)
        {
            passwordInput.Select();
        }
        else if (passwordInput.isFocused)
        {
            usernameInput.Select();
        }
    }
}
    bool SetIDPass()
    {
        id = usernameInput.text.Trim();
        pass = passwordInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }
    
    public void Register()
    {
        //print("asdf");
        if (!SetIDPass())
        {
            print("is NULL");
            return;
        }
        //print("asdf");
        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    public void Log_in()
    {
        if (!SetIDPass())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    void Response(string json)
    {
        
        if (string.IsNullOrEmpty(json)) return;
        GD = JsonUtility.FromJson<GoogleData>(json);
        if (GD.result == "ERROR")
        {
            print(GD.order + "login failed : " + GD.msg);
            return;
        }

        print(GD.order + "log in success : " + GD.msg);
        GlobalData.myID = id;
        //print(GlobalData.myID);

        SceneManager.LoadScene("Main");
        if (GD.order == "getValue")
        {
            ValueInput.text = GD.value;
        }
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) Response(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }
    }    
}