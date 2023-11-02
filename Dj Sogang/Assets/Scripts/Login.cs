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
    const string URL = "https://script.google.com/macros/s/AKfycbxNtodzt4JZGqkJ6Np3m1gehJWnYf5qKbng0caJmWBj30ZxC-h_csxjxlEI3SeRoo_vKA/exec";
    string id, pass;

    public GoogleData GD;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField ValueInput;
    public Button loginButton;
    public Button RegisterButton; // 변수 이름 수정
    public Text loginStatusText;

    private string correctUsername = "your_username"; // 실제 사용자 이름으로 변경
    private string correctPassword = "your_password"; // 실제 비밀번호로 변경

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

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //print("asderf");
            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
            //print("asdfqqwerqwerqwe");
        }
        print("asdf");
    }

    void Response(string json)
    {
        
        if (string.IsNullOrEmpty(json)) return;
        //print("asdfqqwerqqwerqwerqwerwerqwe");
        GD = JsonUtility.FromJson<GoogleData>(json);
        //print("asdfqqwerqqwerqwerqwerwerqwe");
        if (GD.result == "ERROR")
        {
            print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
            return;
        }

        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

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

        // 실제 로그인 로직을 구현하고 검증하는 곳입니다.
        // 예를 들어, 사용자 이름과 비밀번호를 확인하고 로그인 성공 시 다음 화면으로 이동합니다.
        if (username == correctUsername && password == correctPassword)
        {
            loginButton.gameObject.SetActive(false); // 로그인 버튼 비활성화
            RegisterButton.gameObject.SetActive(false); // 회원 가입 버튼 비활성화 (변수 이름 수정)
            usernameInput.gameObject.SetActive(false); // 아이디 입력 필드 활성화
            passwordInput.gameObject.SetActive(false); // 비밀번호 입력 필드 활성화
            loginStatusText.text = "로그인 성공!";

            // 다음 화면으로 이동하거나 게임을 시작하는 로직을 추가하세요.
        }
        else
        {
            loginButton.gameObject.SetActive(false); // 로그인 버튼 비활성화
            RegisterButton.gameObject.SetActive(false); // 회원 가입 버튼 비활성화 (변수 이름 수정)
            usernameInput.gameObject.SetActive(false); // 아이디 입력 필드 활성화
            passwordInput.gameObject.SetActive(false); // 비밀번호 입력 필드 활성화
            loginStatusText.text = "로그인 실패. 사용자 이름 또는 비밀번호가 일치하지 않습니다.";
        }
    }

    */


}