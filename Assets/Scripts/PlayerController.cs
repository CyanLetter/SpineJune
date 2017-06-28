
using UnityEngine;
using Spine.Unity;


public class PlayerController : MonoBehaviour {

	[Header("Controls")]
	public string XAxis = "Horizontal";
	public string YAxis = "Vertical";
	public string JumpButton = "Jump";

	[Header("Moving")]
	public float runSpeed = 15;
	public float gravity = 65;

	[Header("Jumping")]
	public float jumpSpeed = 25;

	[Header("Graphics")]
	public Transform graphicsRoot;
	public SkeletonAnimation skeletonAnimation;

	[Header("Animation")]
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string runName = "Run";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string idleName = "Idle";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string jumpName = "Jump";

	[Header("Audio")]
	public AudioSource jumpAudioSource;
	public AudioSource footstepAudioSource;
	public ParticleSystem dustParticles;

	
	Rigidbody2D body;
	public Collider2D groundSensor;
	Collider2D[] groundTestResults = new Collider2D[10];
	public LayerMask groundMask;
	ContactFilter2D groundFilter;
	public bool isGrounded = true;
	Vector2 velocity = Vector2.zero;
	Quaternion flippedRotation = Quaternion.Euler(0, 180, 0);
	Vector2 checkpoint;

	void Awake () {
		body = GetComponent<Rigidbody2D>();
		groundFilter = new ContactFilter2D();
		groundFilter.SetLayerMask(groundMask);
		checkpoint = transform.position;
	}

	void Start () {

	}


	void Update () {
		//control inputs
		float x = Input.GetAxis(XAxis);
		float y = Input.GetAxis(YAxis);
		//check for force crouch
		velocity.x = 0;

		//Calculate control velocity
		if (Input.GetButtonDown(JumpButton)) {
			
			if (isGrounded) {
				//jump
				// jumpAudioSource.Stop();
				// jumpAudioSource.Play();
				velocity.y = jumpSpeed;
				isGrounded = false;
			}
			
		}
		
		if (x != 0) {
			//walk or run
			velocity.x = runSpeed;
			velocity.x *= Mathf.Sign(x);
		}

		//apply gravity F = mA (Learn it, love it, live it)
		velocity.y -= gravity * Time.deltaTime;
		
		if (isGrounded) {
			//cancel out Y velocity if on ground
			if (x == 0) {
				velocity.y = 0f;
			} else if (body.GetContacts(groundTestResults) > 0) {
				velocity.y = -gravity * Time.deltaTime;
			} else {
				int groundsTouched = groundSensor.OverlapCollider(groundFilter, groundTestResults);
				if (groundsTouched <= 0) {
					isGrounded = false;
				}
				velocity.y = -gravity * 0.2f;
			}
			
			// velocity.y = -slideSpeed;
		} else {
			int groundsTouched = groundSensor.OverlapCollider(groundFilter, groundTestResults);
			if (groundsTouched > 0 && velocity.y <= 0) {
				isGrounded = true;
			}
		}

		//move
		body.velocity = new Vector3(velocity.x, velocity.y, 0);
		
		//graphics updates
		if (isGrounded) {
			skeletonAnimation.loop = true;
			if (x == 0) //idle
				skeletonAnimation.AnimationName = idleName;
			else //move
				skeletonAnimation.AnimationName = runName;
		} else {
			skeletonAnimation.loop = false;
			skeletonAnimation.AnimationName = jumpName;
		}

		//flip left or right
		if (x > 0)
			graphicsRoot.localRotation = Quaternion.identity;
		else if (x < 0)
			graphicsRoot.localRotation = flippedRotation;
	}

	// void OnTriggerEnter2D(Collider2D other)
	// {
	// 	if(((1<<other.gameObject.layer) & groundFilter.layerMask) != 0 && body.velocity.y <= 0) {
	// 		isGrounded = true;
	// 	}
	// }

	// void OnTriggerExit2D(Collider2D other)
	// {
	// 	int groundsTouched = groundSensor.OverlapCollider(groundFilter, groundTestResults);
	// 	if (groundsTouched > 0) {
	// 		isGrounded = true;
	// 	} else {
	// 		isGrounded = false;
	// 	}
	// }

	public void Checkpoint() {
		checkpoint = transform.position;
	}

	public void Respawn() {
		transform.position = checkpoint;
		velocity = Vector2.zero;
	}

}