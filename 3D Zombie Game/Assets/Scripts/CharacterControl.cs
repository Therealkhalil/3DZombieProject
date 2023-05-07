using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float attackDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(true);
        //wait .5 sec
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }
}
