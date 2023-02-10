using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _animator= GetComponent<Animator>();
        _player= GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(IS_WALKING,_player.IsWalking);
    }
}
