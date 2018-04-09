using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip loseSound;
	[SerializeField] AudioClip winSound;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem loseParticles;
	[SerializeField] ParticleSystem winParticles;
	
	Rigidbody rigidBody;
	AudioSource thrustAudio;
	enum State { Alive, Dying, Trancending }
	State state = State.Alive;

	

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		thrustAudio = GetComponent<AudioSource>();
		thrustAudio.PlayOneShot(winSound);
	}
	
	// Update is called once per frame
	void Update () {

		if (state == State.Alive)
		{
		RespondToThrustInput();
		RespondToRotateInput();
		}
	}

    void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true; // takes manual control of rotation

		
		float rotationThisFrame = rcsThrust * Time.deltaTime;

			
		if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

		rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            thrustAudio.Stop();
			mainEngineParticles.Stop();
        }
    }

    void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!thrustAudio.isPlaying)
        {
            thrustAudio.PlayOneShot(mainEngine);
        }
		mainEngineParticles.Play();
    }

    void OnCollisionEnter(Collision collision)
	{
		if (state !=State.Alive) { return; } //ignore collisions when dead
		switch (collision.gameObject.tag)
		{
			case "Friendly":
			print("OK");
			break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
	}

    private void StartDeathSequence()
    {
        print("dead");
        state = State.Dying;
        thrustAudio.Stop();
        thrustAudio.PlayOneShot(loseSound);
		loseParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        print("Hit Finish!");
        state = State.Trancending;
        thrustAudio.Stop();
        thrustAudio.PlayOneShot(winSound);
		winParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
		
    }

    private void LoadNextScene()
    {
		SceneManager.LoadScene(1);
    }
}
  