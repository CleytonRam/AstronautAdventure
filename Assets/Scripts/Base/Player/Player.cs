using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [Header("Player stats")]
    public CharacterController characterController;
    public float speed = 1f;
    public float runSpeed = 1.5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpHeight = 1f;

    [Header("Player Controls")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Animation")]
    public Animator animator;
    [Header("Flash")]
    public List<FlashColor> flashColors;


    //privates
    private float vSpeed = 0f;


    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0f;
            if(Input.GetKeyDown(jumpKey))
            {
                vSpeed = jumpHeight;
            }
        }


        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;


        var isWalking = inputAxisVertical != 0;
        if (isWalking) 
        {
            if (Input.GetKey(runKey)) 
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);


        animator.SetBool("Run", inputAxisVertical != 0);

   
    }
    #region LIFE
    public void Damage(float damage)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }
    #endregion
}
