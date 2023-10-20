using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody thisRigidbody;
    [SerializeField] float thrustValue = 1f;
    [SerializeField] float steerValue = 1f;
    Vector3 thrustVector3;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ProcessInput();
        ProcessThrust();
        ProcessSteer();
    }

    void ProcessThrust()
    {
        thrustVector3 = Vector3.up;
        if (Input.GetKey(KeyCode.Space))
        {
            thisRigidbody.AddRelativeForce(thrustVector3 * thrustValue * Time.deltaTime);
            //Debug.Log("pressed thrust command");
        }
    }

    void ProcessSteer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ExecuteSteer(Vector3.back);
            //transform.Rotate(Vector3.back * steerValue * Time.deltaTime);
            //Debug.Log("pressed left steer command");
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ExecuteSteer(Vector3.forward);
            //Debug.Log("pressed right steer command");
            //transform.Rotate(Vector3.forward * steerValue * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            ExecuteSteer(Vector3.left);
            //transform.Rotate(Vector3.left * steerValue * Time.deltaTime);
            //Debug.Log("pressed up steer command");
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ExecuteSteer(Vector3.right);
            //transform.Rotate(Vector3.right * steerValue * Time.deltaTime);
            //Debug.Log("pressed down steer command");
        }
    }
    void ExecuteSteer(Vector3 thisVector3)
    {
        thisRigidbody.freezeRotation = true;//this prevents conflict between the input controls and the physics system
        transform.Rotate(thisVector3 * steerValue * Time.deltaTime);
        thisRigidbody.freezeRotation = false;//this prevents conflict between the input controls and the physics system

    }
}
