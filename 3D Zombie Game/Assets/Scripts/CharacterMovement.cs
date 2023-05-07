using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Jump
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;
    
    private bool canjump=true;

    private void Start()
    {
        //anim = GetComponent<Animator>(); 
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime );
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        controller.Move(Vector3.up * gravity * Time.deltaTime);
        if (direction != Vector3.zero)
        {
            Run();
        }
        else
        {
            Idle();
        }

        //Jump
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded)
        {
            direction.y = 0f;
            if(Input.GetButton("Jump"))
            {
                 if (canjump)
                 {

                    //direction.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
                    direction.y = jumpHeight;
                    anim.SetBool("run", false);
                    anim.SetTrigger("jump");
                    canjump=false;
                }
             StartCoroutine(Stopjumping(2f));
            }
        }
        direction.y = gravity * Time.deltaTime;
    }
    private void Run()
    {
        anim.SetBool("run", true);
    }
      private void Idle()
    {
        anim.SetBool("run", false);
    }
       IEnumerator Stopjumping(float time) {
        canjump= false;
        yield return new WaitForSeconds(time);
        canjump = true;
    }

    
    
}
