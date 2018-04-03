using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	Rigidbody rigidBody;
	AudioSource thrustAudio;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		thrustAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();
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
		Scene scene = SceneManager.GetActiveScene();
		print ("active scene is" + scene.buildIndex);
		switch (collision.gameObject.tag)
		{
			case "Friendly":
			print("OK");
			break;
			case "Finish":
			print("Hit Finish!");
			SceneManager.LoadScene(scene.buildIndex + 1);
			//print (SceneManager.GetActiveScene());
			break;
			default:
			print("dead");
			SceneManager.LoadScene(scene.buildIndex);
			break;
		}
	}
}
  