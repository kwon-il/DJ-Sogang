using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GoogleData
{
    public string order, result, msg, value;
}

public class Login : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbweZuLIWLFQTAMtVHXjPbnJKR7HBoH-BTJ17cY9wGXhHlE-XVSNeL1axqHE-WgM8MUENQ/exec";
    string id, pass;

    public GoogleData GD;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField ValueInput;
    public Button loginButton;
    public Button RegisterButton; // ���� �̸� ����
    public Text loginStatusText;

    //private string correctUsername = "your_username"; // ���� ����� �̸����� ����
    //private string correctPassword = "your_password"; // ���� ��й�ȣ�� ����

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

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            if (www.isDone) Response(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }
    }

    void Response(string json)
    {
        
        if (string.IsNullOrEmpty(json)) return;
        GD = JsonUtility.FromJson<GoogleData>(json);
        if (GD.result == "ERROR")
        {
            print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);
            return;
        }

        print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);
        SceneManager.LoadScene("Main");
        if (GD.order == "getValue")
        {
            ValueInput.text = GD.value;
        }
    }

}