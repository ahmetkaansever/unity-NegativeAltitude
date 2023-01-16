using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip crashingSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashingParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioComp;

    bool isTransitioning;

    void Start() 
    {
        audioComp = GetComponent<AudioSource>();
        isTransitioning = false;
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        //todo add SFX upon crash
        //todo add particle effect upon crash)
        crashingParticles.Play();
        isTransitioning = true;
        audioComp.Stop();
        audioComp.PlayOneShot(crashingSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartSuccessSequence()
    {
        //todo add SFX upon crash
        //todo add particle effect upon crash
        successParticles.Play();
        isTransitioning = true;
        audioComp.Stop();
        audioComp.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
