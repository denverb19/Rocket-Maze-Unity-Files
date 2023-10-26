using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody thisRigidbody;
    AudioSource thisAudioSource;
    [SerializeField] float thrustValue = 2000f;
    [SerializeField] float steerValue = 250f;
    [SerializeField] AudioClip engineSoundClip;
    [SerializeField] public ParticleSystem mainEngineParticles;
    [SerializeField] public ParticleSystem boosterOneParticles;
    [SerializeField] public ParticleSystem boosterTwoParticles;
    [SerializeField] public ParticleSystem boosterThreeParticles;
    [SerializeField] public ParticleSystem boosterFourParticles;
    Vector3 thrustVector3;
    //private Collisions collisionScript;
    //bool collisionsDisabled = false;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAudioSource = GetComponent<AudioSource>();
        //collisionScript = GetComponent<Collisions>();
    }

    void Update()
    {
        //ProcessCheatCommands();
        ProcessThrust();
        ProcessSteer();
    }

    /*
    void ProcessCheatCommands()
    {
        if (Input.GetKey(KeyCode.C) && !collisionsDisabled)
        {
            collisionScript.enabled = false;
            Debug.Log("Collisions disabled");
            collisionsDisabled = true;
            /*if (!collisionScript.enabled)
            {
                collisionScript.enabled = true;
            }
            else
            {
                collisionScript.enabled = false;
            }
        }
        else
        {
            return;
        }
        */
        /*
        else if (Input.GetKey(KeyCode.V))
        {
            collisionScript.enabled = true;
        }
    }*/
    
    void ProcessThrust()
    {
        thrustVector3 = Vector3.up;
        if (Input.GetKey(KeyCode.Space))
        {
            BeginThrust();
        }
        else
        {
            EndThrust();
        }
    }

    void ProcessSteer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!boosterFourParticles.isPlaying)
            {
                boosterFourParticles.Play();
            }
            StopSteeringThrusters(4);
            /*
            boosterOneParticles.Stop();
            boosterTwoParticles.Stop();
            boosterThreeParticles.Stop();
            */
            ExecuteSteer(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!boosterThreeParticles.isPlaying)
            {
                boosterThreeParticles.Play();
            }
            StopSteeringThrusters(3);
            /*
            boosterOneParticles.Stop();
            boosterTwoParticles.Stop();
            boosterFourParticles.Stop();
            */
            ExecuteSteer(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (!boosterOneParticles.isPlaying)
            {
                boosterOneParticles.Play();
            }
            StopSteeringThrusters(1);
            /*
            boosterTwoParticles.Stop();
            boosterThreeParticles.Stop();
            boosterFourParticles.Stop();
            */
            ExecuteSteer(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (!boosterTwoParticles.isPlaying)
            {
                boosterTwoParticles.Play();
            }
            StopSteeringThrusters(2);
            /*
            boosterOneParticles.Stop();
            boosterThreeParticles.Stop();
            boosterFourParticles.Stop();
            */
            ExecuteSteer(Vector3.right);
        }
        else
        {
            StopSteeringThrusters(0);
            /*
            boosterOneParticles.Stop();
            boosterTwoParticles.Stop();
            boosterThreeParticles.Stop();
            boosterFourParticles.Stop();
            */
        }
    }

    void StopSteeringThrusters(int boosterToKeep)
    {
        switch(boosterToKeep)
        {
            case 0:
                boosterOneParticles.Stop();
                boosterTwoParticles.Stop();
                boosterThreeParticles.Stop();
                boosterFourParticles.Stop();
                break;
            case 1:
                boosterTwoParticles.Stop();
                boosterThreeParticles.Stop();
                boosterFourParticles.Stop();
                break;
            case 2:
                boosterOneParticles.Stop();
                boosterThreeParticles.Stop();
                boosterFourParticles.Stop();
                break;
            case 3:
                boosterOneParticles.Stop();
                boosterTwoParticles.Stop();
                boosterFourParticles.Stop();
                break;
            case 4:
                boosterOneParticles.Stop();
                boosterTwoParticles.Stop();
                boosterThreeParticles.Stop();
                break;
            default:
                break;
        }
    }
    private void BeginThrust()
    {
        thisRigidbody.AddRelativeForce(thrustVector3 * thrustValue * Time.deltaTime);
        if (!thisAudioSource.isPlaying)
        {
            thisAudioSource.PlayOneShot(engineSoundClip);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

private void EndThrust()
    {
        thisAudioSource.Stop();
        mainEngineParticles.Stop();
        /*
        boosterOneParticles.Stop();
        boosterTwoParticles.Stop();
        boosterThreeParticles.Stop();
        boosterFourParticles.Stop();*/
    }

    void ExecuteSteer(Vector3 thisVector3)
    {
        thisRigidbody.freezeRotation = true;//this prevents conflict between the input controls and the physics system
        transform.Rotate(thisVector3 * steerValue * Time.deltaTime);
        thisRigidbody.freezeRotation = false;//this prevents conflict between the input controls and the physics system

    }
}
