    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class play : MonoBehaviour
{
    public Image canvasImage; // Inspector에서 Image 컴포넌트를 할당하세요
    private AudioSource audioSource;
    string currentMusicName = GlobalData.musicName;
    //public AudioMixerGroup mixerGroup;

    // Start is called before the first frame update
    void Start()
    {   
        GameObject canvasObject = GameObject.Find("AudioManager"); 
        audioSource = canvasObject.GetComponent<AudioSource>();

        //audioSource.outputAudioMixerGroup = mixerGroup;
        Debug.Log("Mixer Group assigned: " + audioSource.outputAudioMixerGroup);

        // Resources/Photo 폴더에서 씬 이름과 일치하는 이미지를 불러옵니다
        Sprite sceneImage = Resources.Load<Sprite>("Photo/" + currentMusicName);

        // 이미지를 찾았고 canvasImage가 할당되었다면 스프라이트를 설정합니다
        if (canvasImage != null && sceneImage != null)
        {
            canvasImage.sprite = sceneImage;
            // 이미지를 캔버스 크기에 맞게 조정하고 싶다면 아래 코드를 사용합니다
            //canvasImage.SetNativeSize(); // 원본 크기를 유지하면서 설정
            // 이미지가 너무 크거나 작을 수 있으니, 캔버스 크기에 맞게 스케일 조정이 필요할 수 있습니다
        }
        else
        {
            Debug.LogError("Image 컴포넌트 또는 씬 이미지를 찾을 수 없습니다.");
        }
        StartCoroutine(PlayMusicWithDelay(2.25f));
    }

    // Update is called once per frame
    void Update()
    {
        // 필요한 경우 업데이트 로직
    }

    IEnumerator PlayMusicWithDelay(float delay)
    {
        // 지정된 시간(초) 동안 대기
        yield return new WaitForSeconds(delay);

        // Resources/Music 폴더에서 씬 이름과 일치하는 음악 파일을 불러옵니다
        AudioClip musicClip = Resources.Load<AudioClip>("Music/" + currentMusicName);

        // 음악 클립을 찾았다면 재생합니다
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
            Debug.Log("노래 재생: " + musicClip.name); // 노래의 이름을 출력
        }
        else
        {
            Debug.LogError("음악 클립을 찾을 수 없습니다: " + currentMusicName);
        }
    }
}