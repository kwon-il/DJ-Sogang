using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button RegisterButton; // ���� �̸� ����
    public Text loginStatusText;

    private string correctUsername = "your_username"; // ���� ����� �̸����� ����
    private string correctPassword = "your_password"; // ���� ��й�ȣ�� ����

    private void Start()
    {
        // ��ư Ŭ�� �� �α��� �Լ� ȣ��
        loginButton.onClick.AddListener(AttemptLogin);
    }

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
}