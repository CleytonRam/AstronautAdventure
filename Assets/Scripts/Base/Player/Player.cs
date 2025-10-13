using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour //,IDamageable
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

    [Header("Health")]
    public HealthBase healthBase;


    [Header("Animation")]
    public Animator animator;
    [Header("Flash")]
    public List<FlashColor> flashColors;


    //privates
    private float vSpeed = 0f;
    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }
    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;

    }

    void Update()
    {

        if (!_alive) return;
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
    private void OnKill(HealthBase health)
    {
        if(_alive && healthBase.currentLife <= 0)
        {
            _alive = false;
            animator.SetTrigger("Death");

            Invoke(nameof(Revive), 2f);
        }
    }

    private void Revive() 
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
    }
    public void Damage(HealthBase health)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }


    [NaughtyAttributes.Button("Respawn")]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckPoint()) 
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            
        }
    }
    #endregion
}
