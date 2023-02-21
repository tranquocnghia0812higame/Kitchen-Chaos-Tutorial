using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    public string ObjectName;
}
