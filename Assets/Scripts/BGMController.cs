using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    
    [SerializeField] AudioClip bgm2;
    [SerializeField] AudioClip bgm3;
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;

    AudioSource audioSource;
    

    [SerializeField] float fadeDuration = 2f;
    //private bool isFading_1 = false;
    //private bool isFading_2 = false;
    private bool isFading = false;
    private float originalVolume;
    private GameManager.STAGETYPE previousStageType;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
        originalVolume = audioSource.volume;
        previousStageType = GameManager.instance.stageTYPE;
    }

    private void Update()
    {
        // ステージの状態が変更されたかどうかをチェック
        if (GameManager.instance.stageTYPE != previousStageType)
        {
            previousStageType = GameManager.instance.stageTYPE;

            if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_2 && !isFading)
            {
                StartCoroutine(CrossFade(audioSource, audioSource1, bgm2));
            }
            else if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_3 && !isFading)
            {
                StartCoroutine(CrossFade(audioSource1, audioSource2, bgm3));
            }
        }
        //if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_2)
        //{
        //    StartCoroutine(CrossFade(audioSource, audioSource1, bgm2));
        //}
        //else if (GameManager.instance.stageTYPE == GameManager.STAGETYPE.STAGE_3)
        //{
        //    StartCoroutine(CrossFade_2(audioSource1, audioSource2, bgm3));
        //}

    }
    

    private IEnumerator CrossFade(AudioSource fromSource, AudioSource toSource, AudioClip toClip)
    {
        isFading = true;
        
        toSource.clip = toClip;
        toSource.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            fromSource.volume = Mathf.Lerp(originalVolume, 0f, progress);
            toSource.volume = Mathf.Lerp(0f, originalVolume, progress);

            yield return null;
        }

        fromSource.Stop();
        fromSource.volume = originalVolume;
        isFading = false;
        //if (isFading_1 == false)
        //{
        //    isFading_1 = true;
        //    yield return new WaitForSeconds(0.5f);
        //    toSource.clip = toClip;
        //    toSource.Play();

        //    float timer = 0f;
        //    while (timer < fadeDuration)
        //    {
        //        timer += Time.deltaTime;
        //        float progress = timer / fadeDuration;

        //        fromSource.volume = Mathf.Lerp(originalVolume, 0f, progress);
        //        toSource.volume = Mathf.Lerp(0f, originalVolume, progress);

        //        yield return null;
        //    }

        //    fromSource.Stop();
        //    fromSource.volume = originalVolume;

        //}

    }
    //private IEnumerator CrossFade_2(AudioSource fromSource, AudioSource toSource, AudioClip toClip)
    //{
    //    if (isFading_2 == false)
    //    {
    //        isFading_2 = true;
    //        yield return new WaitForSeconds(0.5f);
    //        toSource.clip = toClip;
    //        toSource.Play();

    //        float timer = 0f;
    //        while (timer < fadeDuration)
    //        {
    //            timer += Time.deltaTime;
    //            float progress = timer / fadeDuration;

    //            fromSource.volume = Mathf.Lerp(originalVolume, 0f, progress);
    //            toSource.volume = Mathf.Lerp(0f, originalVolume, progress);

    //            yield return null;
    //        }

    //        fromSource.Stop();
    //        fromSource.volume = originalVolume;

    //    }

    //}
    //public void BGM_1()
    //{
    //    StartCoroutine(CrossFade(audioSource, audioSource1, bgm2));
    //}
}
