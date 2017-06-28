
using UnityEngine;
using Spine.Unity;


public class AcrobaticPlayerController : MonoBehaviour {

	[Header("Controls")]
	public string XAxis = "Horizontal";
	public string YAxis = "Vertical";
	public string JumpButton = "Jump";

	[Header("Moving")]
	public float runSpeed = 15;
	public float gravity = 65;
	public float slideSpeed = 5f;

	[Header("Jumping")]
	public float jumpSpeed = 25;
	public float jumpDuration = 0.5f;
	public float jumpInterruptFactor = 100;

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
	public AudioSource hardfallAudioSource;

	
	Rigidbody2D body;
	BoxCollider2D torsoCollider;
	CircleCollider2D feetCollider;
	bool isGrounded = true;
	Vector2 velocity = Vector2.zero;
	float jumpEndTime = 0;
	bool jumpInterrupt = false;
	Quaternion flippedRotation = Quaternion.Euler(0, 180, 0);
	Vector2 checkpoint;

	void Awake () {
		body = GetComponent<Rigidbody2D>();
		torsoCollider = GetComponent<BoxCollider2D>();
		feetCollider = GetComponent<CircleCollider2D>();
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
			} else {
				// fast fall
				velocity.y -= jumpSpeed;
			}
			
		}
		
		if (x != 0) {
			//walk or run
			velocity.x = runSpeed;
			velocity.x *= Mathf.Sign(x);
		}

		//apply gravity F = mA (Learn it, love it, live it)
		velocity.y -= gravity * Time.deltaTime;

		//move
		body.velocity = new Vector3(velocity.x, velocity.y, 0);
		
		if (isGrounded) {
			//cancel out Y velocity if on ground
			// velocity.y = -gravity * Time.deltaTime;
			velocity.y = -slideSpeed;
		}

		
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

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "ground") {
			isGrounded = true;
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "ground" && isGrounded) {
			isGrounded = false;
			velocity.y = jumpSpeed;
		}
	}

	public void Checkpoint() {
		checkpoint = transform.position;
	}

	public void Respawn() {
		transform.position = checkpoint;
	}

}