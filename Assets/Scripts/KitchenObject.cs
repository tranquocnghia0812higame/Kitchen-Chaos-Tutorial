using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => _kitchenObjectSO;
}
