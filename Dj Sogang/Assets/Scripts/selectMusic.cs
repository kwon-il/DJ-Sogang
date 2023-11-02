using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class selectMusic : MonoBehaviour
{
    public ScrollRect scrollRect; // Inspector���� �Ҵ��� ScrollRect
    public float scrollSpeed = 0.1f; // ��ũ�� �ӵ� ����
    public Color normalColor = new Color(185f / 255f, 189f / 255f, 163f / 255f, 1f);
    public Color selectedColor = Color.yellow; // ���õ� �������� ����

    private List<GameObject> items; // ��ũ�� �� ���� ������ ���
    private int selectedIndex = 0; // ���� ���õ� �������� �ε���

    void Start()
    {
        if (scrollRect == null)
        {
            // ScrollRect ������Ʈ�� �ִ��� �ڵ����� ã�� �ڵ带 �߰��� �� ����
            scrollRect = GetComponent<ScrollRect>();

            // ���� ������ null �̸�, ������ �α��մϴ�.
            if (scrollRect == null)
            {
                Debug.LogError("ScrollRect�� �Ҵ���� �ʾҽ��ϴ�!");
                return; // ������ �ڵ�� �������� ����
            }
        }

        items = new List<GameObject>();

        // ��ũ�� ���� �����۵��� ����Ʈ�� �߰�
        foreach (Transform item in scrollRect.content)
        {
            items.Add(item.gameObject);
            item.GetComponent<Image>().color = normalColor; // �ʱ� ���� ����
        }

        // ù ��° �������� ���û��·� ����
        if (items.Count > 0) SelectItem(0);
    }

    void Update()
    {
        // �� ȭ��ǥ Ű�� ������ ���� ������ ����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedIndex > 0)
            {
                SelectItem(selectedIndex - 1);
            }
        }
        // �Ʒ� ȭ��ǥ Ű�� ������ �Ʒ��� ������ ����
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedIndex < items.Count - 1)
            {
                SelectItem(selectedIndex + 1);
            }
        }
    }

    private void SelectItem(int index)
    {
        // ���� ���� �������� ������ �Ϲ� �������� ����
        items[selectedIndex].GetComponent<Image>().color = normalColor;

        // ���� ���õ� �������� �ε��� ������Ʈ �� ���� ����
        selectedIndex = index;
        items[selectedIndex].GetComponent<Image>().color = selectedColor;

        // ���õ� ���������� ��ũ�� �� �̵�
        float normalizePosition = 1 - ((float)selectedIndex / (items.Count - 1));
        scrollRect.verticalNormalizedPosition = normalizePosition;
    }
}