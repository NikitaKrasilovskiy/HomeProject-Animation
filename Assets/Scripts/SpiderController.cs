using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiderController : MonoBehaviour
{
    public Rigidbody Spider;
    public float Speed; 
    public float Jumpforce; 
    private float FlyMove = 5;
    private float DeathSpeed = 15;
    private bool Life = true;
    private Animator Animator;
    //public float SpeedRot;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Spider = GetComponent<Rigidbody>();
    }   
    void Update()
    {
        if (Life == true)
        {
            Moves();
            Gravity();
        }
        AnimationSpider();
    }
    private void Moves()
    {
        float MoveX = Input.GetAxisRaw("Horizontal") * Speed;
        float MoveZ = Input.GetAxisRaw("Vertical") * Speed;
        bool Ground = Physics.Raycast(transform.position, -transform.up, 1);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && Ground == true)
        {
            Spider.velocity = new Vector3(-MoveX, 0, -MoveZ);
        }
        //Spider.AddTorque(0, MoveZ * SpeedRot, 0 * Time.fixedDeltaTime);
        //Vector3 MoveVector = new Vector3(-MoveX, 0, -MoveZ); 
        //Spider.AddForce(MoveVector * Speed * Time.fixedDeltaTime);
        //if (Input.GetKey(KeyCode.Q))
        //{ transform.Rotate(0, -SpeedRot * SpeedRot, 0); }
        //if (Input.GetKey(KeyCode.E))
        //{ transform.Rotate(0, SpeedRot * SpeedRot, 0); }
    }
    private void Gravity()
    {
        bool Ground = Physics.Raycast(transform.position, -transform.up, 1);
        if (Input.GetButtonDown("Jump") && Ground == true)
        {Spider.velocity = new Vector3(0, Jumpforce, 0);}        
    }
     IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.7f);
        Animator.enabled = false;
    }
    private void ResetAnimator()
    {
        Animator.SetBool("Walk", false);
        Animator.SetBool("Back", false);
        Animator.SetBool("Left", false);
        Animator.SetBool("Right", false);
        Animator.SetBool("Jump", false);
        Animator.SetBool("Attack", false);
        Animator.SetBool("Shot", false);        
        Animator.SetBool("Ukus", false);
    }
    private void AnimationSpider()
    {
        bool Ground = Physics.Raycast(transform.position, -transform.up, 1);
         if (FlyMove >= 1 && Ground == false)
         {Animator.SetBool("Fall", true);}
        else
        {Animator.SetBool("Fall", false);}
        if (Life == true)
        {if (Input.GetAxisRaw("Vertical") > 0 && Ground == true)
            {Animator.SetBool("Walk", true);}
            else if (Input.GetAxisRaw("Vertical") < 0 && Ground == true)
            {Animator.SetBool("Back", true);}
            else if (Input.GetAxisRaw("Horizontal") > 0 && Ground == true)
            { Animator.SetBool("Left", true); }
            else if (Input.GetAxisRaw("Horizontal") < 0 && Ground == true)
            { Animator.SetBool("Right", true); }
            else if (Input.GetKey(KeyCode.Mouse0) && Ground == true)
            {Animator.SetBool("Attack", true);}
            else if (Input.GetKey(KeyCode.Mouse1) && Ground == true)
            { Animator.SetBool("Shot", true); }
            else if (Input.GetKey(KeyCode.Space) && Ground == true)
            { Animator.SetBool("Jump", true); }
            else if (Input.GetKey(KeyCode.R) && Ground == true)
            {Animator.SetBool("Ukus", true);}
            else if( Ground == false)
            {Animator.SetBool("Fall", true);}
            else
            {ResetAnimator();}
        }
        else
        {
            if(Ground == true)
            {
                Animator.SetBool("Death", true);
                StartCoroutine("Stop");
            }
            ResetAnimator();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.y > DeathSpeed)
        {
            Life = false;
            Debug.Log(collision.relativeVelocity.y);
        }
    }
}

    
