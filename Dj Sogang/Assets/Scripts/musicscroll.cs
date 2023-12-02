using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class musicscroll : MonoBehaviour
{
    public GameObject itemPrefab; // Inspector에서 프리팹 할당
    public Transform contentPanel; // Inspector에서 Content GameObject 할당
    public float itemSpacing = 20f; // 아이템 간의 간격을 설정합니다.
    public ScrollRect scrollRect; // Inspector에서 ScrollRect를 할당합니다.
    private List<GameObject> items = new List<GameObject>(); // 아이템들의 리스트
    private int selectedIndex = 0; // 현재 선택된 아이템의 인덱스
    public Image mainSceneImage; // Inspector에서 메인 씬의 Image UI 요소 할당
    public AudioSource audioSource;

    void Start()
    {   
        GameObject canvasObject = GameObject.Find("Canvas"); 
        audioSource = canvasObject.GetComponent<AudioSource>();
        LoadMusic();
        if (!string.IsNullOrEmpty(GlobalData.musicName))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == GlobalData.musicName)
                {
                    selectedIndex = i;
                    break;
                }
            }
        }

        if (items.Count > 0)
        {
            HighlightItem(); // Highlight the item at startup.
            ScrollToSelected();
        }
    }

    void Update()
{
    // 사용자의 키보드 입력을 확인합니다.
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        // 맨 위에서 위쪽 화살표를 누른 경우, 맨 아래로 이동합니다.
        if (selectedIndex <= 0)
        {
            selectedIndex = items.Count - 1;
        }
        else
        {
            selectedIndex--;
        }

        HighlightItem();
        ScrollToSelected();  
    }
    else if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        // 맨 아래에서 아래쪽 화살표를 누른 경우, 맨 위로 이동합니다.
        if (selectedIndex >= items.Count - 1)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex++;
        }

        HighlightItem();
        ScrollToSelected();
    }
    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
    {
        LoadSelectedMusicScene();
    }
    if (Input.GetKeyDown(KeyCode.Tab))
    {
        StartCoroutine(PreviewMusic());
    }
}
    
    void LoadMusic()
    {
        AudioClip[] musicTracks = Resources.LoadAll<AudioClip>("Music");
        float contentHeight = musicTracks.Length * itemSpacing;
        ((RectTransform)contentPanel).sizeDelta = new Vector2(((RectTransform)contentPanel).sizeDelta.x, contentHeight);

        for (int i = 0; i < musicTracks.Length; i++)
        {
            AudioClip track = musicTracks[i];
            Sprite photo = Resources.Load<Sprite>("Photo/" + track.name);
            TextAsset textAsset = Resources.Load<TextAsset>("Text/" + track.name);

            GameObject newItem = Instantiate(itemPrefab, contentPanel);
            newItem.name = track.name;
            items.Add(newItem);
            Image imageComponent = newItem.GetComponentInChildren<Image>();

            // Image 컴포넌트에 사진을 할당합니다.
            if (imageComponent != null && photo != null)
            {
                imageComponent.sprite = photo;
            }
            else
            {
                Debug.LogError("Image 컴포넌트 또는 사진이 로드되지 않았습니다: " + track.name);
            }

            
            GameObject textPrefab = Resources.Load<GameObject>("Text/" + track.name);
            if (textPrefab != null)
            {
                // 로드된 텍스트 프리팹에서 Text 컴포넌트를 찾습니다.
                Text textComponent = textPrefab.GetComponentInChildren<Text>();
                if (textComponent != null)
                {
                    // 현재 아이템의 Text 컴포넌트를 찾아 텍스트를 설정합니다.
                    Text itemTextComponent = newItem.GetComponentInChildren<Text>();
                    if (itemTextComponent != null)
                    {
                        itemTextComponent.text = textComponent.text;
                    }
                }
                else
                {
                    Debug.LogError("프리팹 내의 Text 컴포넌트를 찾을 수 없습니다: " + track.name);
                }
            }
            else
            {
                Debug.LogError("텍스트 프리팹을 로드할 수 없습니다: " + track.name);
            }
            
        }
        
    }
    IEnumerator PreviewMusic()
    {
        if (selectedIndex >= 0 && selectedIndex < items.Count)
        {
            AudioClip clip = Resources.Load<AudioClip>("Music/" + items[selectedIndex].name);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();

                yield return new WaitForSeconds(20); // Preview for 20 seconds

                audioSource.Stop();
            }
            else
            {
                Debug.LogError("Audio clip not found for: " + items[selectedIndex].name);
            }
        }
        yield return null;
    }
    void HighlightItem()
    {
    // 모든 아이템을 반복합니다.
    for (int i = 0; i < items.Count; i++)
    {   
        if (i == selectedIndex)
        {
            // 메인 씬의 이미지를 업데이트합니다.
            if (mainSceneImage != null && items[selectedIndex].GetComponentInChildren<Image>().sprite != null)
            {
                mainSceneImage.sprite = items[selectedIndex].GetComponentInChildren<Image>().sprite;
            }
        }
        // 각 아이템에서 'Background'라는 이름의 패널을 찾습니다.
        Transform backgroundPanel = items[i].transform.Find("Background");

        if (backgroundPanel != null)
        {
            Image backgroundImage = backgroundPanel.GetComponent<Image>();
            if (backgroundImage != null)
            {
                // 선택된 아이템이면 파란색으로, 아니면 기본 색상으로 설정합니다.
                backgroundImage.color = (i == selectedIndex) ? Color.green : Color.grey;
            }
            else
            {
                Debug.LogError("Background 패널에 Image 컴포넌트가 없습니다: " + items[i].name);
            }
        }
        else
        {
            Debug.LogError("아이템 내에 'Background'라는 이름의 자식이 없습니다: " + items[i].name);
        }
        
    }
    }

    void ScrollToSelected()
{
    // scrollRect의 null 체크를 추가합니다.
    if (scrollRect != null)
    {
        float normalizePosition = (float)selectedIndex / (items.Count - 1);
        scrollRect.verticalNormalizedPosition = 1 - normalizePosition;
    }
    else
    {
        Debug.LogError("ScrollRect가 할당되지 않았습니다.");
    }
}

void LoadSelectedMusicScene()
{
    // 선택된 음악의 이름을 가져옵니다.
    AudioClip selectedTrack = Resources.Load<AudioClip>("Music/" + items[selectedIndex].name);
    if (selectedTrack != null)
    {
        // AudioClip의 이름을 사용하여 동일한 이름의 씬을 로드합니다.
        // 프로젝트의 구조와 명명 규칙에 따라 적절히 수정하십시오.
        GlobalData.musicName = selectedTrack.name; 
        SceneManager.LoadScene("Game");
    }
    else
    {
        Debug.LogError("선택된 트랙이 null이거나 해당 트랙에 대한 씬을 찾을 수 없습니다: " + items[selectedIndex].name);
    }
}



}
