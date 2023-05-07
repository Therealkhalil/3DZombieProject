using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    public Animator anim; 

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    
    private Vector3 velocity;
    private bool canjump=true;
    
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded)
        {
            velocity.y = 0f;
            if(Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        velocity.y -= gravity * Time.deltaTime;
        
    }
    IEnumerator Stopjumping(float time) {
        canjump= false;
        yield return new WaitForSeconds(time);
        canjump = true;
    }

    private void Jump()
    {
        if (canjump){
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
        anim.SetBool("run", false);
        anim.SetTrigger("jump");
        canjump=false;
        }
        StartCoroutine(Stopjumping(2f));
        
    }
}
