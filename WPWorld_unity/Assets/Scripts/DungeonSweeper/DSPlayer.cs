using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlayer : MonoBehaviour
{
    public float JumpSpeed;
    //public float MAX_UPSPEED;
    public GameObject Manager;
    public bool isPlayedOnce = false;

    private bool isInAir = false;
    private bool isGrounded = true;
    private bool isDoubleJUmp = false;
    private Rigidbody Rb;
 
    SoundSystem ss;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        ss = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }

    /// <summary>
    /// Physic Update
    /// </summary>
    private void FixedUpdate()
    {
        // Keyboard Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Raycast the block below the player 
        if (isDoubleJUmp)
        {
            //var _playerScript = gameObject.GetComponent<PlayerMovement>();
            //_playerScript.GetDPadInput(Vector3.zero);
            // Check the Object below
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out _hit))
            {
                // Block checking
                if (_hit.transform.gameObject.tag != "Blocks")
                {
                    return;
                }               

                // Debuging line
                Debug.DrawLine(transform.position, _hit.transform.position, Color.red, 5.0f);

                // If the distance is small enough, trigger it             
                if (_hit.distance <= _hit.transform.localScale.x / 10)
                {
                    var _hitedObjScript = _hit.transform.gameObject.GetComponent<Blocks>();
                                
                    // When the block is number and not triggered, reset the timer
                    if(_hitedObjScript.m_BlockType == Dungeonsweeper2.BlockType.NUMBERED && !_hitedObjScript.m_isTriggered)
                    {                     
                        var ManagerScript = Manager.GetComponent<Dungeonsweeper2>();
                        ManagerScript.currTimer = ManagerScript.TimerBar.maxValue;
                    }                  

                    _hitedObjScript.m_isTriggered = true;
                }               
            }
        }
        
        // Send the player flying after losing
        if(Manager.GetComponent<Dungeonsweeper2>().is_lose)
        {          
            if (!isPlayedOnce)
            {              
                Explosion();
                ss.PlaySFX("Explosion");
                isPlayedOnce = true;
            }
        }
        else
        {                    
            isPlayedOnce = false;
        }
      
        //// Speed bumper
        //if (Rb.velocity.y > MAX_UPSPEED || Rb.velocity.y < -MAX_UPSPEED)
        //{
        //    Rb.velocity = Vector3.zero;
        //    Rb.velocity.Set(0, MAX_UPSPEED, 0);
        //}
    }

    // 000000000000000000000000000000000000000000
    //              Private METHOD
    // 000000000000000000000000000000000000000000

    /// <summary>
    /// Jumping
    /// </summary>
    private void Jump()
    {
        if(Manager.GetComponent<Dungeonsweeper2>().is_lose)
        {
            return;
        }

        // 1st Jump 
        if (isGrounded)
        {
            isGrounded = false;
            isInAir = true;
            Rb.velocity = Vector3.zero;
            Rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
            ss.PlaySFX("Jump");
        }
        // 2nd Jump (ground pound)
        else
        {
            if (isInAir)
            {
                this.transform.GetComponent<GridBaseMovement>().SetIsDisable(true);
                Rb.velocity = Vector3.zero;
                Rb.AddForce(-transform.up * JumpSpeed, ForceMode.Impulse);
                isDoubleJUmp = true;
            }
        }
    }

    /// <summary>
    /// For button jumping (phone)
    /// </summary>
    private void GetJumpButtonInput()
    {
        Jump();
    }

    /// <summary>
    /// Resets variables when hit the ground 
    /// </summary>   
    private void OnCollisionEnter(Collision other)
    {
        this.transform.GetComponent<GridBaseMovement>().SetIsDisable(false);
        isInAir = false;
        isGrounded = true;
        isDoubleJUmp = false;          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killbox")
        {
            this.gameObject.GetComponent<GridBaseMovement>().Respawn();
        }
    }

    /// <summary>
    /// Explosion
    /// </summary>
    private void Explosion()
    {
        Vector3 _pos = new Vector3();
        RaycastHit _ray;
        if (Physics.Raycast(transform.position, -transform.up, out _ray))
        {
            _pos = _ray.transform.position;
        }

        Rb.AddExplosionForce(10.0f, _pos, 5.0f, 5.0f, ForceMode.Impulse);
    }
}