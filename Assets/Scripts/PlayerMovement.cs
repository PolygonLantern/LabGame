using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private CharacterController controller;
   [SerializeField] private Animator animator;
   [SerializeField] private Player player;
   
   private float horizontalMovement = 0f;
   [SerializeField] private float maxSpeed = 30f;
   private bool jump;
   private bool isAttacking;
   private int currentState = 0;
 

  private void FixedUpdate()
   {
      horizontalMovement = Input.GetAxisRaw("Horizontal") * maxSpeed;
   
         if (Input.GetButtonDown("Jump"))
         {
            Jumps();
         }
   
         if (Input.GetButtonDown("Fire1"))
         {
            isAttacking = true;
            Attack();
         }

      Movement();
      jump = false;
      
   }

   void Jumps()
   {
      jump = true;
      animator.SetBool("_jumping", true);
     
   }

   public void OnLand()
   {
      animator.SetBool("_jumping", false);
   }
   void Movement()
   {
      //move the character
      controller.Move(horizontalMovement * Time.fixedDeltaTime, false, jump);
      animator.SetFloat("_movement", Mathf.Abs(horizontalMovement));

   }

 
   void Attack()
   {
      //play animation
      if (isAttacking)
      {
         currentState++;

         if (currentState > 3) currentState = 1;
         
         animator.SetTrigger("Attack" + currentState);

         player.DealDamage(player.maxDamage);
         
         isAttacking = false;
         player.attackPoint.gameObject.SetActive(true);
      }
      else
      {
         player.attackPoint.gameObject.SetActive(false);
      }
      
     
      //reset
   }

   private void OnDrawGizmosSelected()
   {
      if (player.attackPoint == null) return;
   
      Gizmos.DrawWireSphere(player.attackPoint.position, player.range);
   }
}
