using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObject;
    [SerializeField] private Transform _counterTopPoint;
 
    public void Interact()
    {
        Transform tomatoTransform = Instantiate(_kitchenObject.Prefab, _counterTopPoint);
        tomatoTransform.localPosition = Vector3.zero;
    }
}
