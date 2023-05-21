using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 10;
    public float turningSpeed = 60;
    public static float vertical, horizontal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * turningSpeed*Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
        vertical = Input.GetAxis("Vertical")*movementSpeed*Time.deltaTime;
        transform.Translate(0, 0, vertical);
    }
}
