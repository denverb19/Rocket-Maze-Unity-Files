using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    [SerializeField] float delayDesired = 1f;
    [SerializeField] AudioClip crashSoundClip;
    [SerializeField] AudioClip successSoundClip;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    AudioSource thisAudioSource;
    private Movement movementScript;
    bool isTransitioning = false;
    bool collisionsDisabled = false;
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
        movementScript = GetComponent<Movement>();

    }
    void Update()
    {
        ProcessDebugKeys();
    }
    void ProcessDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            StartCoroutine(LoadNextLevel(0f));
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
            Debug.Log("Collisions toggled");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Launch Pad":
                //Debug.Log("Launch Pad collision");
                break;
            case "Landing Pad":
                //Debug.Log("Landing Pad collision");
                //StartCoroutine(LoadNextLevel(delayDesired));
                WinSequence();
                break;
            case "Obstacle":
                //Debug.Log("Obstacle collision");
                //StartCoroutine(ReloadLevel(1f));
                CrashSequence();
                break;
            case "Ground":
                //Debug.Log("Ground collision");
                //StartCoroutine(ReloadLevel(1f));
                CrashSequence();
                break;
            default:
                break;
        }
    }
    
    void CrashSequence()
    {
        if (isTransitioning || collisionsDisabled)
        {
            return;
        }
        else
        {
            isTransitioning = true;
            crashParticles.Play();
            if (movementScript.enabled == true)//disable movement and stop thruster sounds
            {
                /*GetComponent<Movement>().enabled = false;
                thisAudioSource.Stop();
                GetComponent<Movement>().mainEngineParticles.Stop();
                GetComponent<Movement>().boosterOneParticles.Stop();
                GetComponent<Movement>().boosterTwoParticles.Stop();
                GetComponent<Movement>().boosterThreeParticles.Stop();
                GetComponent<Movement>().boosterFourParticles.Stop();*/
                movementScript.enabled = false;
                thisAudioSource.Stop();
                movementScript.mainEngineParticles.Stop();
                movementScript.boosterOneParticles.Stop();
                movementScript.boosterTwoParticles.Stop();
                movementScript.boosterThreeParticles.Stop();
                movementScript.boosterFourParticles.Stop();
            }
            if(!thisAudioSource.isPlaying)//Check to avoid multiple crash sounds
            {
                thisAudioSource.PlayOneShot(crashSoundClip);
            }
            StartCoroutine(ReloadLevel(delayDesired));
        }
    }
    void WinSequence()
    {
        if (isTransitioning)
        {
            return;
        }
        else
        {
            isTransitioning = true;
            successParticles.Play();
            if (movementScript.enabled == true)//disable movement and stop thruster sounds
            {
                movementScript.enabled = false;
                thisAudioSource.Stop();
            }
            if(!thisAudioSource.isPlaying)//Check to avoid multiple success sounds
            {
                thisAudioSource.PlayOneShot(successSoundClip);
            }
            StartCoroutine(LoadNextLevel(delayDesired));
        }
    }
    IEnumerator ReloadLevel(float delayRequired)
    {
        yield return new WaitForSeconds(delayRequired);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
    IEnumerator LoadNextLevel(float delayRequired)
    {
        
        yield return new WaitForSeconds(delayRequired);
        //int currentLevel = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("current level argument:" + currentLevel);
        //int nextLevel = ++currentLevel;
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //Debug.Log("next level argument:" + nextLevel);
        if (nextLevel == SceneManager.sceneCountInBuildSettings)//Check if we are on last level
        {
            //Debug.Log("Last Level finished");
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}