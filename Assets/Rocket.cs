using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	Rigidbody rigidBody;
	AudioSource thrustAudio;
	enum State { Alive, Dying, Trancending }
	State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		thrustAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if (state == State.Alive)
		//stop sound on death
		{
		Thrust();
		Rotate();
		}
	}

    void Rotate()
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

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }
        }
		else
        {
            thrustAudio.Stop();
        }
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
                print("Hit Finish!");
				state = State.Trancending;
				Invoke("LoadNextScene", 1f); 
                break;
            default:
                print("dead");
				state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
	}

    private static void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
  