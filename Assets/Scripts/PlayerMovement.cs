using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody _rigbod;
    [SerializeField] private Animator _animator;
    private Vector3 _movement;

    void Update()
    {
        // Input
        _movement.x = Input.GetAxis("Horizontal");
        _movement.z = Input.GetAxis("Vertical");

        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.z);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
        _rigbod.velocity = Vector3.zero;
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
   {
        //Movement
        _rigbod.MovePosition(_rigbod.position + _movement * moveSpeed * Time.deltaTime);
    }


}
