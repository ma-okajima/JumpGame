using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectionSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Sprite targetClearSprite;
    [SerializeField] string keyword;
    [SerializeField] Sprite keywordSprite;
    [SerializeField] bool condition;
    [SerializeField] Sprite itemSprite;
    [SerializeField] Sprite coverSprite;
    

    [SerializeField] int num;

    public string Name { get => name;}
    public string Keyword { get => keyword;}
    public bool Condition { get => condition; set => condition = value; }
    public Sprite Sprite { get => itemSprite; }
    
    public int Num { get => num;  }
    public Sprite Coversprite { get => coverSprite;}
    
    public Sprite KeywordSprite { get => keywordSprite;}
    
    public Sprite TargetClearSprite { get => targetClearSprite; }
}

