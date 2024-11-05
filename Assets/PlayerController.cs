using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{

    //dependencies
    public CharacterController characterController;

    //movement
    public float speed = 7f;
    public float runMultiplier = 1.5f;

    // public float jumpSpeed = 0.2f;
    // public float gravity = 0.01f;

    // public float Sensitivity = 1.15f;
    // public float deltaX;

    //control
    public string state = "grounded";
    // public int stateProgress = 0;
    // public bool movementLocked = false;

    //computational

    public Vector3 moveDirection = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {


        characterController.Move(velocity * Time.deltaTime);
        
        // if(state == "grounded" || state == "inair") {
        //     state = characterController.isGrounded ? "grounded" : "inair";
        // }



        //update this here instead because fast

        //We put user-critical things here, things the user controls. This has a higher refresh rate
        switch (state) {
            case "grounded":
                wasd();
                break;
            default:
                break;
        }
    }

    void wasd() {
        moveDirection = Vector3.zero; //.x = 0;

        if (Input.GetKey("w")) {
            moveDirection.x += transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            moveDirection.x += -transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += -transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            moveDirection.x += transform.right.x*speed*Time.deltaTime;
            moveDirection.z += transform.right.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            moveDirection.x += -transform.right.x*speed*Time.deltaTime;
            moveDirection.z += -transform.right.z*speed*Time.deltaTime;
        }
        
        if(Input.GetKey("left shift")) {
            moveDirection.x *= 1.5f;
            moveDirection.z *= 1.5f;
        }

        RaycastHit hit;

        Debug.DrawRay(transform.position, (moveDirection+new Vector3(0, -speed*Time.deltaTime, 0))*100, Color.green, 0f, false);
        
        if(Physics.Raycast(transform.position, moveDirection + new Vector3(0, -2f*speed*Time.deltaTime, 0), out hit, 3f, 255)) {
            characterController.Move(moveDirection);
        }
    }
}
