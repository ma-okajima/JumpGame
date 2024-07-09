using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;

    AudioSource audioSource;
    

    [SerializeField] float fadeDuration = 3f;

    private bool isFading = false;
    private float originalVolume;
    private float secondVolume = 0.5f;
    private GameManager.STAGETYPE previousStageType;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
        originalVolume = audioSource.volume;
        previousStageType = GameManager.instance.stageTYPE;
        audioSource1.Play();
        audioSource2.Play();
    }

    private void Update()
    {
        // ステージの状態が変更されたかどうかをチェック
        if (GameManager.instance.stageTYPE != previousStageType)
        {
            previousStageType = GameManager.instance.stageTYPE;

            if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_2 && !isFading)
            {
                StartCoroutine(CrossFade(audioSource, audioSource1));
            }
            else if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_3 && !isFading)
            {
                StartCoroutine(CrossFade(audioSource1, audioSource2));
            }
        }
        

    }
    

    private IEnumerator CrossFade(AudioSource fromSource, AudioSource toSource)
    {
        isFading = true;
        
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            fromSource.volume = Mathf.Lerp(originalVolume, 0f, progress);
            toSource.volume = Mathf.Lerp(0f, secondVolume, progress);

            yield return null;
        }

        fromSource.Stop();
        fromSource.volume = originalVolume;
        
        isFading = false;
        
    }
    
}
