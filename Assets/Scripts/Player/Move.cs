using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedMove;//скорость персонажа
    public float gravityForce;
    public byte health;
    public GameObject gun;
    
    private Vector3 moveVector;
    private Animator brutAnimator;
    private CharacterController bruteController;

    // Start is called before the first frame update
    void Start()
    {
        brutAnimator = GetComponent<Animator>();
        bruteController = GetComponent<CharacterController>();
        gun.SetActive(false);
    }

    private void Update()
    {
        health = PlayerUI.health;
        PlayerMove();
        PlayerGravity();
        PlayerAttack();
    }

    public void PlayerMove()
    {
        if (bruteController.isGrounded && !brutAnimator.GetBool("Dead"))
        {
            //движение
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;
            moveVector.z = Input.GetAxis("Vertical") * speedMove;
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) //врещение игрока в сторону движения 
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
            //анимация движения
            if(moveVector.x != 0 || moveVector.z != 0) brutAnimator.SetBool("Run",true);
            else brutAnimator.SetBool("Run",false);
        }

        moveVector.y = gravityForce;
        bruteController.Move(moveVector * Time.deltaTime);
    }

    public void PlayerGravity()//гравитация игрока
    {
        if (!bruteController.isGrounded) gravityForce -= 20 * Time.deltaTime;
        else gravityForce -= 1;
    }

    public void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && bruteController.isGrounded)
        {
            brutAnimator.SetTrigger("Attack");
            gun.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MutantWeapon")
        {
            Debug.Log($"HealthPoint: {PlayerUI.health}");
            if (PlayerUI.health > 0)
            {
                brutAnimator.SetTrigger("Damage");
                PlayerUI.health -= 1 ;
            }
            else brutAnimator.SetBool("Dead",true);
        }
        
    }

}
