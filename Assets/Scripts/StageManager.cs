using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bgPrefabs = new List<GameObject> { };
    
    public enum BACKGROUNDTYPE
    {
        BG_1,
        BG_2,
        BG_3,
        BG_4,
        BG_5,
    }

    public BACKGROUNDTYPE backgroundType = BACKGROUNDTYPE.BG_1;
    // Start is called before the first frame update
    void Start()
    {
        bgPrefabs[0].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (backgroundType)
        {
            case BACKGROUNDTYPE.BG_1:
                bgPrefabs[0].SetActive(true);
                break;
            case BACKGROUNDTYPE.BG_2:
                Stage2();
                break;
            case BACKGROUNDTYPE.BG_3:
                Stage3();
                break;
            case BACKGROUNDTYPE.BG_4:
                Stage4();
                break;
            case BACKGROUNDTYPE.BG_5:
                Stage5();
                break;
        }
        
    }
    public void Stage2()
    {
        bgPrefabs[0].SetActive(false);
        bgPrefabs[1].SetActive(true);
        
    }
    public void Stage3()
    {
        bgPrefabs[0].SetActive(true);
        bgPrefabs[1].SetActive(false);

    }
    public void Stage4()
    {
        bgPrefabs[0].SetActive(false);
        bgPrefabs[1].SetActive(true);

    }
    public void Stage5()
    {
        bgPrefabs[0].SetActive(true);
        bgPrefabs[1].SetActive(false);

    }
}
