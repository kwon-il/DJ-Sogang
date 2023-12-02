using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuButtion : MonoBehaviour
{
    // Start is called before the first frame update
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
        GotoMain();
    }
    void GotoMain()
    {
        // ���� ���� �̸��� �����ͼ� �ε��մϴ�.
        SceneManager.LoadScene("Main");
    }
}
