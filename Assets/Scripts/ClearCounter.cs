using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private ClearCounter _secondClearCounter;

    private KitchenObject _kitchenObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_kitchenObject)
            {
                _kitchenObject.SetClearCounter(_secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (!_kitchenObject)
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            _kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            _kitchenObject.SetClearCounter(this);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HaveKitchenObject()
    {
        return _kitchenObject;
    }
}
