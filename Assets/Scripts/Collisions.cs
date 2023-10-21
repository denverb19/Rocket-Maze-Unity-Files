using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    [SerializeField] float delayDesired = 1f;
    [SerializeField] AudioClip crashSoundClip;
    [SerializeField] AudioClip successSoundClip;
    AudioSource thisAudioSource;
    bool isTransitioning = false;
    void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
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
        if (isTransitioning)
        {
            return;
        }
        else
        {
            isTransitioning = true;
            if (GetComponent<Movement>().enabled == true)//disable movement and stop thruster sounds
            {
                GetComponent<Movement>().enabled = false;
                thisAudioSource.Stop();
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
            if (GetComponent<Movement>().enabled == true)//disable movement and stop thruster sounds
            {
                GetComponent<Movement>().enabled = false;
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