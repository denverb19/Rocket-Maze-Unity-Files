using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody thisRigidbody;
    AudioSource thisAudioSource;
    [SerializeField] float thrustValue = 1f;
    [SerializeField] float steerValue = 1f;
    [SerializeField] AudioClip engineSoundClip;
    Vector3 thrustVector3;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessSteer();
    }

    void ProcessThrust()
    {
        thrustVector3 = Vector3.up;
        if (Input.GetKey(KeyCode.Space))
        {
            thisRigidbody.AddRelativeForce(thrustVector3 * thrustValue * Time.deltaTime);
            if(!thisAudioSource.isPlaying)
            {
                thisAudioSource.PlayOneShot(engineSoundClip);
            }
        }
        else
        {
            thisAudioSource.Stop();
        }
    }

    void ProcessSteer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ExecuteSteer(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ExecuteSteer(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            ExecuteSteer(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ExecuteSteer(Vector3.right);
        }
    }
    void ExecuteSteer(Vector3 thisVector3)
    {
        thisRigidbody.freezeRotation = true;//this prevents conflict between the input controls and the physics system
        transform.Rotate(thisVector3 * steerValue * Time.deltaTime);
        thisRigidbody.freezeRotation = false;//this prevents conflict between the input controls and the physics system

    }
}
