using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectionSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] string keyword;
    [SerializeField] bool condition;
    [SerializeField] Sprite sprite;
    [TextArea]
    [SerializeField] string text;
    [SerializeField] int num;

    public string Name { get => name;}
    public string Keyword { get => keyword;}
    public bool Condition { get => condition; set => condition = value; }
    public Sprite Sprite { get => sprite; }
    public string Text { get => text; }
    public int Num { get => num;  }
}

