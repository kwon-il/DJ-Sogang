using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button RegisterButton; // 변수 이름 수정
    public Text loginStatusText;

    private string correctUsername = "your_username"; // 실제 사용자 이름으로 변경
    private string correctPassword = "your_password"; // 실제 비밀번호로 변경

    private void Start()
    {
        // 버튼 클릭 시 로그인 함수 호출
        loginButton.onClick.AddListener(AttemptLogin);
    }

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
}