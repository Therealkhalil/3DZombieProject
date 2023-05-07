using System.Diagnostics;
using UnityEngine;

public class zombieDetectionFollow : MonoBehaviour
{

    float fovDist = 1000f;
    float fovAngle = 90;
    public float speed = 5.0f;
    public Transform target;
    public float r = 3;
    public float T = 2.0f;
    private Vector3 targetPosition;

    public Animator anim;


    bool visible(Transform Player)
    {
        Vector3 direction = Player.position - transform.position;
        Vector3 RAYPOS = new Vector3(this.transform.position.x, this.transform.position.y+0.8f, this.transform.position.z);
        float angle = Vector3.Angle(direction, transform.forward);
        UnityEngine.Debug.DrawRay(RAYPOS, direction);
        RaycastHit hit;
        bool v = false;
        
        if (Physics.Raycast(RAYPOS, direction, out hit) && hit.collider.tag == "Player" && direction.magnitude < fovDist && angle < fovAngle)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                v = true;
                //return true;
            }
            else
            {
                //return false;
                v = false;
            }
        }
        return v;
    }
    public Transform Player;
    void Update()
    {
        if (visible(Player))
        {
            //UnityEngine.Debug.Log("Player is visible in position" + Player.position);
            MoveEnemy();
        }
        else
        {
            //UnityEngine.
                //Debug.Log("No player in sight");
        }
    }

     private void MoveEnemy()
    {
        // Get the position of the target object
        targetPosition = target.position;

        // Calculate the distance between the player and the target
        Vector3 distance = targetPosition - transform.position;

        // If the distance is less than r, decrease the speed of the player
        if (distance.magnitude < r)
        {
            speed = speed / T;
        }
        // If the distance is greater than r, set the speed back to its original value
        else
        {
            speed = 5.0f;
        }

        // Calculate the direction in which the player needs to move
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the player towards the target
        transform.Translate(direction * Time.deltaTime * speed, Space.World);

        // Rotate the player towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
          if (direction != Vector3.zero)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}

