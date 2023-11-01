using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleData
{
    public string order, result, msg, value;
}

public class Login : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbySJG6ZCcV80x0vtBXxEnwT00FzR7XZUmbrzzX8YDBhDaNGMrKc_YXqo19J7aoOraCPgQ/exec";
    string id, pwd;

    public GoogleData GD;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField ValueInput;
    public Button loginButton;
    public Button RegisterButton; // ���� �̸� ����
    public Text loginStatusText;

    private string correctUsername = "your_username"; // ���� ����� �̸����� ����
    private string correctPassword = "your_password"; // ���� ��й�ȣ�� ����

    bool SetIDPass()
    {
        id = usernameInput.text.Trim();
        pwd = passwordInput.text.Trim();

        if (id == "" || pwd == "") return false;
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
        form.AddField("pwd", pwd);

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();
            print("asderf");
            if (www.isDone) Response(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
            print("asdfqqwerqwerqwe");
        }
        print("asdf");
    }

    void Response(string json)
    {
        
        if (string.IsNullOrEmpty(json)) return;
        print("asdfqqwerqqwerqwerqwerwerqwe");
        GD = JsonUtility.FromJson<GoogleData>(json);
        print("asdfqqwerqqwerqwerqwerwerqwe");
        if (GD.result == "ERROR")
        {
            print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);
            return;
        }

        print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

        if (GD.order == "getValue")
        {
            ValueInput.text = GD.value;
        }
    }

    /*
    private void AttemptLogin()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // ���� �α��� ������ �����ϰ� �����ϴ� ���Դϴ�.
        // ���� ���, ����� �̸��� ��й�ȣ�� Ȯ���ϰ� �α��� ���� �� ���� ȭ������ �̵��մϴ�.
        if (username == correctUsername && password == correctPassword)
        {
            loginButton.gameObject.SetActive(false); // �α��� ��ư ��Ȱ��ȭ
            RegisterButton.gameObject.SetActive(false); // ȸ�� ���� ��ư ��Ȱ��ȭ (���� �̸� ����)
            usernameInput.gameObject.SetActive(false); // ���̵� �Է� �ʵ� Ȱ��ȭ
            passwordInput.gameObject.SetActive(false); // ��й�ȣ �Է� �ʵ� Ȱ��ȭ
            loginStatusText.text = "�α��� ����!";

            // ���� ȭ������ �̵��ϰų� ������ �����ϴ� ������ �߰��ϼ���.
        }
        else
        {
            loginButton.gameObject.SetActive(false); // �α��� ��ư ��Ȱ��ȭ
            RegisterButton.gameObject.SetActive(false); // ȸ�� ���� ��ư ��Ȱ��ȭ (���� �̸� ����)
            usernameInput.gameObject.SetActive(false); // ���̵� �Է� �ʵ� Ȱ��ȭ
            passwordInput.gameObject.SetActive(false); // ��й�ȣ �Է� �ʵ� Ȱ��ȭ
            loginStatusText.text = "�α��� ����. ����� �̸� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
        }
    }

    */


}