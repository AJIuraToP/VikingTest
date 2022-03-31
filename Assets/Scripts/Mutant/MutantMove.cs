using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class MutantMove : MonoBehaviour
{
    
    private float speedMove;
    private float gravityForce;
    private bool move = true;


 
    public GameObject player;
    public Collider mesh ;
    private CharacterController mutantController;
    private Vector3 moveVector;
    private Animator mutantAnimator;
    public void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        mutantAnimator = GetComponent<Animator>();
        mutantController = GetComponent<CharacterController>();
        transform.LookAt(player.transform);
        mutantAnimator.SetInteger("Health",1);
    }

    public void Update()
    {
        
        if (move && mutantAnimator.GetInteger("Health" ) != 0&& PlayerUI.playMode)
        {
            transform.LookAt(player.transform.position);
            mutantAnimator.SetBool("Move",true);
            transform.position = Vector3.Lerp(transform.position, player.transform.position,Random.Range(0.5f,0.9f) * Time.deltaTime);
        }
        else mutantAnimator.SetBool("Move",false);
        PlayerGravity();
    }
    
    public void PlayerGravity()//гравитация игрока
    {
        if (!mutantController.isGrounded) gravityForce -= 20 * Time.deltaTime;
        else gravityForce -= 1;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") move = false;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") move = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            mutantAnimator.SetInteger("Health",0);
            PlayerUI.score += 1;
            mesh.enabled = false;
            Invoke("Respawn",5f);
        }
    }

    public void Respawn()
    {
        EnemyController.countEnemy -= 1;
        Destroy(gameObject);
    }
}
