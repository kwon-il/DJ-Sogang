using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class retryButtion : MonoBehaviour
{
    Button btn;

    private void Start()
    {
        btn = this.transform.GetComponent<Button>();

        if (btn != null)
        {
            // Add the OnButtonClick method to the button's click event
            btn.onClick.AddListener(OnButtonClick);
        }
    }
    public void OnButtonClick()
    {
        RestartScene();
    }
    void RestartScene()
    {
        // ���� ���� �̸��� �����ͼ� �ε��մϴ�.
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
