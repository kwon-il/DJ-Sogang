using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class selectMusic : MonoBehaviour
{
    public ScrollRect scrollRect; // Inspector에서 할당할 ScrollRect
    public float scrollSpeed = 0.1f; // 스크롤 속도 조절
    public Color normalColor = new Color(185f / 255f, 189f / 255f, 163f / 255f, 1f);
    public Color selectedColor = Color.yellow; // 선택된 아이템의 배경색

    private List<GameObject> items; // 스크롤 뷰 안의 아이템 목록
    private int selectedIndex = 0; // 현재 선택된 아이템의 인덱스

    void Start()
    {
        if (scrollRect == null)
        {
            // ScrollRect 컴포넌트가 있는지 자동으로 찾는 코드를 추가할 수 있음
            scrollRect = GetComponent<ScrollRect>();

            // 만약 여전히 null 이면, 문제를 로그합니다.
            if (scrollRect == null)
            {
                Debug.LogError("ScrollRect가 할당되지 않았습니다!");
                return; // 이후의 코드는 실행하지 않음
            }
        }

        items = new List<GameObject>();

        // 스크롤 뷰의 아이템들을 리스트에 추가
        foreach (Transform item in scrollRect.content)
        {
            items.Add(item.gameObject);
            item.GetComponent<Image>().color = normalColor; // 초기 색상 설정
        }

        // 첫 번째 아이템을 선택상태로 설정
        if (items.Count > 0) SelectItem(0);
    }

    void Update()
    {
        // 위 화살표 키가 눌리면 위쪽 아이템 선택
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedIndex > 0)
            {
                SelectItem(selectedIndex - 1);
            }
        }
        // 아래 화살표 키가 눌리면 아래쪽 아이템 선택
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
        // 이전 선택 아이템의 배경색을 일반 색상으로 변경
        items[selectedIndex].GetComponent<Image>().color = normalColor;

        // 새로 선택된 아이템의 인덱스 업데이트 및 배경색 변경
        selectedIndex = index;
        items[selectedIndex].GetComponent<Image>().color = selectedColor;

        // 선택된 아이템으로 스크롤 뷰 이동
        float normalizePosition = 1 - ((float)selectedIndex / (items.Count - 1));
        scrollRect.verticalNormalizedPosition = normalizePosition;
    }
}