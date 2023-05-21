using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarAnimController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private int state;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
            state = 1;
        }
        else{
            state = 0;
        }

        animator.SetInteger("state", state);

        if (Input.GetKey(KeyCode.LeftShift)){
            animator.speed = 1.5f;
        }
        else{
            animator.speed = 1f;
        }

    }
}
