using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Bomb : MonoBehaviourPun
{
    public GameObject BombFirePrefab;
    public GameObject BlockPrefab;
  
    // Bomb Properties
    private int firePower;
    private GameObject Owner;
    private Photon.Realtime.Player OwnerPUN;
    private float currTimer;
    private float MAX_TIMER = 3.0f;
    private Collider col;

    // Debugging
    private Text debug;

    private void Start()
    {
        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
        col = this.transform.GetComponent<Collider>();
        col.isTrigger = true;
        currTimer = 0.0f;
    }

    private void Update()
    {
        this.transform.eulerAngles = Vector3.zero;
        if(currTimer > MAX_TIMER)
        {
            BlowUp();
            currTimer = 0;
        }
        else
        {
            currTimer += 1.0f * Time.deltaTime;
        }
    }

    public virtual void BlowUp()
    {       
        float scalableSize = BlockPrefab.transform.localScale.x * this.transform.parent.transform.localScale.x;
        
        var newBomb = BombFirePrefab;
        Instantiate(newBomb, this.transform.position, Quaternion.identity, this.transform.parent);
       
        // + X
        RaycastHit hit;
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, Vector3.right, out hit, scalableSize * i))
            {
                if (hit.transform.gameObject.tag == "BombFire" || hit.transform.gameObject.tag == "Player")
                {
                    Instantiate(newBomb, this.transform.position + Vector3.right * (scalableSize * i), Quaternion.identity, this.transform.parent);
                }
                else if(hit.transform.gameObject.tag == "BombermanBreakable")
                {              
                    hit.transform.GetComponent<BombermanBreakable>().isDestroyed = true;  
                    if (hit.transform.GetComponent<BombermanBreakable>().NumHits == 1)
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.BreakableScore);
                    }
                    else
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.Breakable2Score);
                    }                   
                }
            }
            else
            {
                Instantiate(newBomb, this.transform.position + Vector3.right * (scalableSize * i), Quaternion.identity, this.transform.parent);
            }
        }
        // - X
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, -Vector3.right, out hit, scalableSize * i))
            {
                if (hit.transform.gameObject.tag == "BombFire" || hit.transform.gameObject.tag == "Player")
                {
                    Instantiate(newBomb, this.transform.position + -Vector3.right * (scalableSize * i), Quaternion.identity, this.transform.parent);
                }
                else if (hit.transform.gameObject.tag == "BombermanBreakable")
                {
                    hit.transform.GetComponent<BombermanBreakable>().isDestroyed = true;
                    if (hit.transform.GetComponent<BombermanBreakable>().NumHits == 1)
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.BreakableScore);
                    }
                    else
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.Breakable2Score);
                    }
                }
            }
            else
            {
                Instantiate(newBomb, this.transform.position + -Vector3.right * (scalableSize * i), Quaternion.identity, this.transform.parent);
            }
        }
        // + Z
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, Vector3.forward, out hit, scalableSize * i))
            {
                if (hit.transform.gameObject.tag == "BombFire" || hit.transform.gameObject.tag == "Player")
                {
                    Instantiate(newBomb, this.transform.position + Vector3.forward * (scalableSize * i), Quaternion.identity, this.transform.parent);
                }
                else if (hit.transform.gameObject.tag == "BombermanBreakable")
                {
                    hit.transform.GetComponent<BombermanBreakable>().isDestroyed = true;
                    if (hit.transform.GetComponent<BombermanBreakable>().NumHits == 1)
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.BreakableScore);
                    }
                    else
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.Breakable2Score);
                    }              
                }
            }
            else
            {
                Instantiate(newBomb, this.transform.position + Vector3.forward * (scalableSize * i), Quaternion.identity, this.transform.parent);
            }
        }
        // - Z
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, -Vector3.forward, out hit, scalableSize * i))
            {
                if (hit.transform.gameObject.tag == "BombFire" || hit.transform.gameObject.tag == "Player")
                {
                    Instantiate(newBomb, this.transform.position + -Vector3.forward * (scalableSize * i), Quaternion.identity, this.transform.parent);
                }
                else if (hit.transform.gameObject.tag == "BombermanBreakable")
                {
                    hit.transform.GetComponent<BombermanBreakable>().isDestroyed = true;
                    if (hit.transform.GetComponent<BombermanBreakable>().NumHits == 1)
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.BreakableScore);
                    }
                    else
                    {
                        photonView.RPC("PlayerAddPoints", OwnerPUN, BombermanManager.Breakable2Score);
                    }
                }
            }
            else
            {
                Instantiate(newBomb, this.transform.position + -Vector3.forward * (scalableSize * i), Quaternion.identity, this.transform.parent);
            }
        }

        ReduceBombCount();
        Destroy(this.gameObject);
    }

    // Setters
    public void SetBombPower(int _newPower)
    {
        firePower = _newPower;
    }

    public void SetBombTimer(int _newTime)
    {
        MAX_TIMER = _newTime;
    }

    public void SetBombOwner(GameObject _newOwner)
    {
        Owner = _newOwner;
    }

    public void SetBombOwnerPUN(Photon.Realtime.Player _newOwnerPUN)
    {
        OwnerPUN = _newOwnerPUN;
    }

    // Getters
    public GameObject GetOwner()
    {
        return Owner;
    }

    public Photon.Realtime.Player GetOwnerPUN()
    {
        return OwnerPUN;
    }

    // Collision
    // Turn off trigger collision when the player is out of the trigger box
    private void OnTriggerExit(Collider other)
    {
        col.isTrigger = false;
    }

    // Reduce Bomb Count
    private void ReduceBombCount()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            if (OwnerPUN.ActorNumber == Photon.Pun.PhotonNetwork.LocalPlayer.ActorNumber)
            {
                PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().OnBombDestoryed();
            }
        }
        else
        {
            Owner.GetComponent<BomberManPlayer>().OnBombDestoryed();
        }

        //if(OwnerPUN == null)
        //{
        //    GameObject.FindGameObjectWithTag("Debug").GetComponent<UnityEngine.UI.Text>().text = "I am null";
        //}

        //if (OwnerPUN.ActorNumber == Photon.Pun.PhotonNetwork.LocalPlayer.ActorNumber)
        //{
        //    GameObject.FindGameObjectWithTag("Debug").GetComponent<UnityEngine.UI.Text>().text = "I Came here 2 inside";
        //    BomberManPlayer.LocalPlayerInstance.GetComponent<BomberManPlayer>().OnBombDestoryed();
        //}
    }
}
