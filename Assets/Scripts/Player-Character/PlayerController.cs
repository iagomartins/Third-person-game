using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{    
    public CharacterController controller;
    public Animator animPlayer;
    public float speed = 3f, distanceRolling; 
    float horizontal, vertical;
    Vector3 move, finalRolling;

    public static PlayerController instancePlayer;

    void Awake()
    {
        instancePlayer = this;   
    }
    void Start()
    {
        AddLife(maxLife);
        AddStamina(maxStamina);        
    }    
    void Update()
    {
        switch(stateCharacter)
        {
            case CharacterState.NORMAL:
                MovementPlayer();
            break;
            case CharacterState.ROLLING:
                Rolling();
            break;
        }
        AnimationsPlayer();
    }

    #region Movement
    void MovementPlayer()
    {        
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        if(currentStamina >= staminaCost)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(currentStamina <= 0)
                {
                    stateCharacter = CharacterState.NORMAL;
                }
                else
                {
                    stateCharacter = CharacterState.START_ROLLING;
                }            
            }
        }    
    }
    #endregion

    #region Rolling
    void Rolling()
    {   
        if(finalRolling == Vector3.zero)
        {
            finalRolling = transform.position + (transform.forward * distanceRolling);
        }
        else
        {
        transform.position = Vector3.MoveTowards(transform.position, finalRolling, 5 * Time.deltaTime); 
        
            if(finalRolling == transform.position)
            {
                finalRolling = Vector3.zero;
                stateCharacter = CharacterState.NORMAL;
            }          
        }                     
    }
    public void StartRolling()
    {
        stateCharacter = CharacterState.ROLLING;
        UseStamina(staminaCost);     
    }
    public void FinalRolling()
    {
        animPlayer.SetBool("rolling", false);
    }
    #endregion
    
    #region Animations
    void AnimationsPlayer()
    {
        animPlayer.SetFloat("vertical", vertical);
        animPlayer.SetFloat("horizontal", horizontal);

        #region Walking animation
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animPlayer.SetBool("walk", true);
        }
        else if(!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
        {
            animPlayer.SetBool("walk", false);
        }
        #endregion

        #region Walking backward animation
        if(Input.GetKeyDown(KeyCode.S))
        {
            animPlayer.SetBool("walking backward", true);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            animPlayer.SetBool("walking backward", false);
        }
        #endregion

        #region Walking sideways animation
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animPlayer.SetBool("walking left/right", true);
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animPlayer.SetBool("walking left/right", false);
        }
        #endregion

        #region Running animation
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(!Input.GetKey(KeyCode.LeftControl))
            {
                speed = 5f;
                animPlayer.SetBool("running", true);
            }
            else
            {
                speed = 3f;
            }            
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3f;
            animPlayer.SetBool("running", false);
        }
        #endregion

        #region Running backward animation
        if(Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            animPlayer.SetBool("running", true);
            animPlayer.SetBool("walking backward", true);
        }
        else if(Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            animPlayer.SetBool("running", false);
            animPlayer.SetBool("walking backward", false);
        }
        #endregion

        #region Crouching animation
        animPlayer.SetBool("can crouch", false);
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            animPlayer.SetBool("crouch", true);
            animPlayer.SetBool("can crouch", true);
            animPlayer.SetBool("walk", false);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            animPlayer.SetBool("crouch", false);
        }
        #endregion

        #region Rolling animation
        if(stateCharacter == CharacterState.START_ROLLING)
        {            
            animPlayer.SetBool("rolling", true);
        }
        #endregion

        #region Dead animation          
        animPlayer.SetBool("dead", false);
        if(stateCharacter == CharacterState.DEAD)
        {
            animPlayer.SetBool("dead", true);
            stateCharacter = CharacterState.DISABLED; 
        }
        #endregion
    }
    #endregion
}
