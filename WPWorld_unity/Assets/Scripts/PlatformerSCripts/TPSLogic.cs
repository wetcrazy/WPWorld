﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour
{

    [SerializeField]
    private float JumpSpeed;
    [SerializeField]
    private bool IsGrounded = false;
    private bool RestrictJump = false;
    private bool Colliding = false;
    [SerializeField]
    private string JumpSFX;

    [SerializeField]
    private string DeathSFX;

    private int PrevPoints;

    [SerializeField]
    private int CurrPoints = 0;

    [SerializeField]
    private int DeathCounter = 0;

    private Rigidbody RigidRef;
    private PlayerMovement MovementRef;
    private SoundSystem SoundSystemRef;

    [SerializeField]
    private float AirborneMovementSpeed;
    private float OrgSpeed;

    private List<CollectOnCollide> ListOfCoins = new List<CollectOnCollide>();
    private List<DestroyOnCollide> ListOfBricks = new List<DestroyOnCollide>();
    private List<ShowOnCollide> ListOfTrolls = new List<ShowOnCollide>();
    private List<SpawnOnCollide> ListOfSpawns = new List<SpawnOnCollide>();
    private List<Enemy> ListOfEnemies = new List<Enemy>();

    // Use this for initialization
    void Start()
    {
        RigidRef = GetComponent<Rigidbody>();
        MovementRef = GetComponent<PlayerMovement>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        OrgSpeed = MovementRef.GetMovementSpeed();

        Physics.gravity = new Vector3(0, -5 * transform.parent.parent.lossyScale.y, 0);

        ListOfCoins.AddRange(FindObjectsOfType(typeof(CollectOnCollide)) as CollectOnCollide[]);
        ListOfBricks.AddRange(FindObjectsOfType(typeof(DestroyOnCollide)) as DestroyOnCollide[]);
        ListOfTrolls.AddRange(FindObjectsOfType(typeof(ShowOnCollide)) as ShowOnCollide[]);
        ListOfSpawns.AddRange(FindObjectsOfType(typeof(SpawnOnCollide)) as SpawnOnCollide[]);
        ListOfEnemies.AddRange(FindObjectsOfType(typeof(Enemy)) as Enemy[]);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (IsGrounded)
        {
            MovementRef.SetMovementSpeed(OrgSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // If none of the raycast is hitting the ground, automatically convert grounded to false
            if (!Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                IsGrounded = false;
            }
            else
            {
                // If one of the hit is currently hitting something, check if it is invisible or even has a renderer component
                if (!hit.transform.GetComponent<Renderer>() || !hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if(!hit.transform.name.Contains("Invisible"))
                        IsGrounded = false;
                }
            }
        }
        else
        {
            MovementRef.SetMovementSpeed(AirborneMovementSpeed);

            RaycastHit hit2, hit3;

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                if (hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if (Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, -transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f)
                        || Physics.Raycast(transform.position, transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f))
                    {
                        if ((Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit3, transform.lossyScale.y * 1.5f))
                            || (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit3, transform.lossyScale.y * 1.5f)))
                        {
                            if ((hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible) &&
                                (hit2.transform.GetComponent<Renderer>() && hit2.transform.GetComponent<Renderer>().isVisible) &&
                                (hit3.transform.GetComponent<Renderer>() && hit3.transform.GetComponent<Renderer>().isVisible) &&
                                (hit.transform.name == hit2.transform.name && hit.transform.name == hit3.transform.name))
                                IsGrounded = true;
                        }
                    }
                    else
                    {
                        if(Colliding)
                            IsGrounded = true;
                    }
                }
                else
                {
                    if(hit.transform.name.Contains("Invisible"))
                        IsGrounded = true;
                }
            }
        }
    }

    public void Jump()
    {
        if (!IsGrounded || RestrictJump)
            return;

        if (JumpSFX != "")
            SoundSystemRef.PlaySFX("Jump");
        PushUp();
    }

    public void PushUp()
    {
        RigidRef.velocity = Vector3.zero;
        RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
        IsGrounded = false;
    }

    public void SetPoints(int n_Points)
    {
        CurrPoints = n_Points;
    }

    public int GetPoints()
    {
        return CurrPoints;
    }

    public int GetDeaths()
    {
        return DeathCounter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Colliding = false;
    }

    public void Death()
    {
        DeathCounter++;
        if (DeathSFX != "")
            SoundSystemRef.PlaySFX(DeathSFX);

        GetComponent<PlayerMovement>().Respawn();
        RigidRef.velocity = Vector3.zero;

        HealthPopup DeathUI = FindObjectOfType<HealthPopup>() as HealthPopup;
        if (DeathUI != null)
            DeathUI.ShowDisplay();

        foreach(CollectOnCollide CoinRef in ListOfCoins)
            CoinRef.Reset();

        foreach(DestroyOnCollide BrickRef in ListOfBricks)
            BrickRef.Reset();

        foreach(ShowOnCollide TrollRef in ListOfTrolls)
            TrollRef.Reset();

        foreach (SpawnOnCollide SpawnRef in ListOfSpawns)
            SpawnRef.Reset();

        foreach (Enemy EnemyRef in ListOfEnemies)
            EnemyRef.Reset();

        foreach(Enemy ClonedEnemy in FindObjectsOfType(typeof(Enemy)) as Enemy[])
        {
            if (ClonedEnemy.name.Contains("Clone"))
                Destroy(ClonedEnemy.gameObject);
        }
    }

    public void Win()
    {
        // Show off Win Screen
    }

    public bool GetJumpRestrict()
    {
        return RestrictJump;
    }

    public void SetJumpRestrict(bool n_Restrict) // True = Cannot Jump, False = Can Jump
    {
        RestrictJump = n_Restrict;
    }

    private void GetJumpButtonInput()
    {
        Jump();
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }
}
