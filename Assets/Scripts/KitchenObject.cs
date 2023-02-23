using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => _kitchenObjectSO;
    private ClearCounter _clearCounter;

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (!_clearCounter)
        {
            _clearCounter.ClearKitchenObject();
        }
        _clearCounter = clearCounter;
        if(_clearCounter.HaveKitchenObject())
        {
            Debug.LogError("Counter already have kitchen object");
        }
        _clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}
