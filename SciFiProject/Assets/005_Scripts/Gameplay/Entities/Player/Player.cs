using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	#region Fields

	private GameObject player;

	private Rigidbody2D rb;

	private PlayerController controller;

	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	private Vector3 RotateChange;

	private bool isGrounded;

	#endregion

	#region Properties
	public bool canControl { get; set; }

	#endregion

	#region UnityInspector

	public Transform graphics;
	public Animator animator;

	[Space]

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
    
	public float moveSpeed = 6;
	//public float jumpDistanceMultiplier = 1.5f;

	[Space]

	public Transform groundCheck;
	public float groundCheckRadius;

	#endregion

    private void Awake()
    {
		player = this.gameObject;

		rb = GetComponent<Rigidbody2D>();

		controller = GetComponent<PlayerController>();
    }

    void Start()
	{
		canControl = true;


		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
	}

    private void FixedUpdate()
    {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, controller.collisionMask);
	}

    void Update()
	{
		float characterVelocity = Mathf.Abs(velocity.x);
		
		Flip(velocity.x);
		animator.SetBool("isJumping", !isGrounded);		

		if (controller.collisions.above || controller.collisions.below)
		{
			velocity.y = 0;
		}

		Vector2 input = Vector2.zero;
		if(canControl)
        {
			input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			
		}   
		//Debug.Log(input);

		if (input.x != 0 && canControl)
		{
			animator.SetBool("run", true);
		}
		else
        {
			animator.SetBool("run", false);
		}

		if (Input.GetButtonDown("Jump") && controller.collisions.below && canControl)
		{
			Debug.Log("Jump");
			animator.SetTrigger("jump");
			velocity.y = jumpVelocity;
		}


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}

	void Flip(float _velocity)
	{
		if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.Q)))
		{
			if (canControl)
			{
				RotateChange = new Vector3(0f, 180f, 0f);

				graphics.transform.rotation = Quaternion.Euler(RotateChange);
			}
		}

		if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
		{
			if (canControl)
			{
				RotateChange = new Vector3(0f, 0f, 0f);

				graphics.transform.rotation = Quaternion.Euler(RotateChange);
			}
		}
	}

	public void PlayerDeath()
    {
		animator.SetTrigger("death");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}
}
