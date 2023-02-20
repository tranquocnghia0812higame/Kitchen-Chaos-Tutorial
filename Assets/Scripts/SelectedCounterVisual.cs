using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _visualGameObject;
    void Start()
    {
        _clearCounter = GetComponentInParent<ClearCounter>();
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
        _visualGameObject = transform.GetChild(0).gameObject;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterArg e)
    {
        if(e.SelectedCounter!= _clearCounter)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        _visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        _visualGameObject.SetActive(false);
    }
}
