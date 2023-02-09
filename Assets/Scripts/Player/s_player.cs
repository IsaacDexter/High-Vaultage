using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class s_player : MonoBehaviour
{
    #region Movement Settings

    [Header("Movement")]
    [Range(0.0f, 20.0f), Tooltip("The players movement speed.")]
    public float m_movementSpeed = 10.0f;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("How much to reduce movement speed in the air by.")]
    float m_airMovementMultiplier = 0.2f;
    [Tooltip("How much to multiply movement speed by.")]
    float m_movementMultiplier = 10.0f;
    [Tooltip("Interacts with input to apply movement horizontally.")]
    float m_horizontalMovement;
    [Tooltip("Interacts with input to apply movement horizontally.")]
    float m_verticalMovement;
    [Tooltip("The direction to move in.")]
    public Vector3 m_moveDirection;
    bool m_mantleEnabled;
    #endregion

    #region Physics Settings
    [Header("Physics")]
    [SerializeField, Range(0.0f, 1000.0f), Tooltip("The maximum velocity the player can reach before being limited")]
    float m_maxVelocity = 27.0f;
    [Range(0.0f, 10.0f), Tooltip("How much drag to apply when the player is touching the ground.")]
    public float m_groundDrag = 8.0f;
    [Range(0.0f, 10.0f), Tooltip("How much drag to apply while the player is airborne.")]
    public float m_airDrag = 0.3f;
    [Tooltip("The rigidbody component to apply physics to.")]
    Rigidbody m_rigidBody;
    [SerializeField, Range(0.0f, 5.0f), Tooltip("The height of the player's model while standing.")]
    float m_heightStanding = 2.0f;
    [SerializeField, Range(0.0f, 5.0f), Tooltip("The height of the player's model while sliding")]
    float m_heightSliding = 1.0f;
    [HideInInspector, Tooltip("The current height of the players model.")]
    public float m_height;
    [SerializeField, Tooltip("The collision detection mode of the player.Continuous dynamic / speculative are good for fast moving projectiles.")] 
    CollisionDetectionMode m_collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    #endregion

    #region Jumping Settings
    [Header("Jumping")]
    [SerializeField, Range(0.0f, 50.0f), Tooltip("How high the player can jump.")]
    float m_jumpForce = 20.0f;
    [SerializeField, Tooltip("Which layer to be seen as the ground.")]
    LayerMask m_groundMask;
    [Tooltip("Whether or not the player is currently touching the ground.")] 
    public bool m_grounded;
    [Tooltip("If the player can currently jump.")]
    bool m_canJump = true;
    bool m_wallJump = false;
    bool m_canDoubleJump = true;
    float m_wallCount = -1;
    #endregion

    #region Sliding Settings
    [Header("Sliding")]
    [SerializeField, Range(0.0f, 10.0f), Tooltip("How much drag to apply to the player as they're sliding.")] 
    float m_slideDrag = 1.0f;
    [SerializeField, Range(0.0f, 10.0f), Tooltip("How much force to apply to player as they slide.")] 
    float m_slideForce = 8.0f;
    [HideInInspector, Tooltip("If the player is or isn't currently sliding.")]
    public bool m_sliding = false;
    [Tooltip("If the player should be sliding, regardless of if it actually is or isn't")]
    private bool m_slidingExpected = false;
    #endregion

    #region Health Settings
    [Header("Health")]
    [SerializeField, Tooltip("The tag to use for the enemy bullets.")] 
    string m_enemyBulletTag = "EnemyBullet";
    [SerializeField, Range(0.0f, 10.0f), Tooltip("The delay in seconds between being damaged and no longer being damaged.")]
    float m_recoveryDuration = 3.0f;
    [Tooltip("The players current damaged state. If damaged, the player will die when it takes damage")]
    bool m_damaged = false;
    [Tooltip("When the player will be recovered")]
    float m_recoveryTime;
    [Tooltip("The player's spawn point it will return to upon death")]
    public Transform m_spawnPoint;
    #endregion

    #region UI Settings
    [Header("UI")]
    [SerializeField, Tooltip("A reference to a s_weaponwheel class, which handles opening events etc")]
    public s_weaponWheel m_weaponWheel;
    [SerializeField, Tooltip("A reference to a s_pausemenu class, which handles opening events etc")]
    s_pauseMenu m_pauseMenu;
    [SerializeField, Tooltip("A reference to a s_hud class, to toggle it when the pause menu opens")]
    s_hud m_hud;
    [SerializeField, Tooltip("A reference to a s_victoryMenu class, to toggle it when the player wins")]
    s_victoryScreen m_victoryScreen;
    public float m_skipperCount;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField, Tooltip("The player's orientation component.")]
    Transform m_orientation;
    [SerializeField, Tooltip("The player's mantle checker, centered in the body")]
    Transform m_mantleCheck;
    [SerializeField, Tooltip("The player's body/capsule, needed to scale/animate while crouched.")]
    Transform m_body;
    [Tooltip("The camera associated with this game object, its tranform used to help calculate direction.")]
    Transform m_camera;
    [Tooltip("The camera's position in the previous call.")]
    Vector3 m_previousCameraPosition;
    [Tooltip("How much the player's position has changed since the previous call.")]
    Vector3 m_positionShift;
    [Tooltip("The player's left hand, that holds their left weapon.")]
    s_hand m_leftHand;
    [Tooltip("The player's right hand, that holds their right weapon.")]
    s_hand m_rightHand;

    //Audio Stuff
    [SerializeField] public float m_volume;
    public AudioSource m_audioSource;
    public AudioClip m_Clip;


    #endregion

    #region Camera Settings
    [SerializeField, Tooltip("The camera to transform on specific movement triggers")]
    Camera m_playerCamera;
    [SerializeField] float m_slideFOV;
    float m_baseFOV;
    #endregion

    #region Look Settings
    [Header("Looking")]
    Vector2 m_delta;
    float m_lookMultiplier = 0.01f;
    float m_xRotation;
    float m_yRotation;
    #endregion

    #region Input Settings
    [Header("Input")]
    public PlayerInput m_playerInput;
    [SerializeField, Tooltip("The mouse's sensitivity in the horizontal")] 
    public Vector2 m_sensitivity =  new Vector2(200.0f, 200.0f);
    #endregion 



    #region Initialization

    private void Start()
    {
        InitializeComponents();

        InitializeMovement();

        m_audioSource = GetComponent<AudioSource>();
        m_Clip = m_audioSource.clip;
    }

    private void OnEnable()
    {
        if (m_playerInput != null)
        {
            m_playerInput.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (m_playerInput != null)
        {
            m_playerInput.enabled = false;
        }
    }

    private void InitializeComponents()
    {
        //Get the player's camera and rigidbody components.
        m_camera = gameObject.GetComponentInChildren<Camera>().transform;
        m_playerInput = gameObject.GetComponent<PlayerInput>();

        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_rigidBody.freezeRotation = true;  //Prevent the rigidbody from rotating
        //Sets the rigidbody's collision detection mode, used to help in collision detection with fast moving object
        m_rigidBody.collisionDetectionMode = m_collisionDetectionMode;

        s_hand[] hands = gameObject.GetComponentsInChildren<s_hand>();  //Get all 2 of the players hands
        if (hands[0].name == "m_leftHand")                              //Figure out which is which.
        {
            m_leftHand = hands[0];
            m_rightHand = hands[1];
        }
        else
        {
            m_leftHand = hands[1];
            m_rightHand = hands[0];
        }

        m_baseFOV = m_playerCamera.fieldOfView;
    }

    private void InitializeMovement()
    {
        m_previousCameraPosition = m_camera.transform.position; //Set the camera's initial previous position to be the cameras position.

        //Set and adjust the initial position shift.
        m_positionShift = m_previousCameraPosition;
        m_positionShift.y -= 0.5f;

        SetHeight(m_heightStanding);
    }

    #endregion

    #region Updating

    private void Update()
    {
        Regenerate();
        CheckGrounded();
        ApplyDrag();
        MantleCheck();

        CalculateMovementDirection();
        CalculateCameraRotation();
        Look();

        LimitVelocity();
    }

    private void FixedUpdate()
    {
        TryMove();
        TrySlide();
    }

    #region Input

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 axis = context.ReadValue<Vector2>();

        CalculateMovementDirection(axis);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 axis = context.ReadValue<Vector2>();

        m_delta = axis;
    }

    public void OnLeftFire(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            m_leftHand.PullTrigger();
        }
        else if (context.canceled)         //The action has concluded i.e. the key has been released
        {
            m_leftHand.ReleaseTrigger();
        }
    }

    public void OnRightFire(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            m_rightHand.PullTrigger();
        }
        else if (context.canceled)         //The action has concluded i.e. the key has been released
        {
            m_rightHand.ReleaseTrigger();
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            StartSliding();
        }
        else if (context.canceled)         //The action has concluded i.e. the key has been released
        {
            StopSliding();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            Jump();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            TogglePause();
        }
    }

    public void OnWheel(InputAction.CallbackContext context)
    {
        if (context.started)                //The action has started i.e. the key has gone down
        {
            ToggleWeaponWheel();
        }
    }



    #endregion

    #endregion

    #region Movement

    /// <summary>Override of calculate movement direction that takes no new input. called in update</summary>
    public void CalculateMovementDirection()
    {
        m_moveDirection = m_orientation.forward * m_verticalMovement + m_orientation.right * m_horizontalMovement;
    }

    /// <summary>Applies horizontal and vertical input to movement components, and calculates a new direction of movement.</summary>
    /// <param name="input">Pass in dynamically from input move</param>
    public void CalculateMovementDirection(Vector2 axis)
    {
        m_horizontalMovement = axis.x;
        m_verticalMovement = axis.y;

        m_moveDirection = m_orientation.forward * m_verticalMovement + m_orientation.right * m_horizontalMovement;
    }

    /// <summary>Attempt to move the player in it's desired direction according to if its in the air, on the ground or sliding.</summary>
    private void TryMove()
    {
        if (m_grounded && !m_sliding)   //if we're on the ground and not sliding...
        {
            m_rigidBody.AddForce(m_moveDirection.normalized * m_movementSpeed * m_movementMultiplier, ForceMode.Acceleration);  //Apply the movement * m_movementMultiplier
        }
        else if (!m_grounded)           //If we're in the air...
        {
            m_rigidBody.AddForce(m_moveDirection * m_movementSpeed * m_movementMultiplier * m_airMovementMultiplier, ForceMode.Force);  //Apply the same * air multiplier
        }
        else                            //If we're sliding
        {
            m_rigidBody.AddForce(new Vector3(0, 0, 0)); //Apply no input
        }
    }

    /// <summary>Applies drag dependent on if the player is airborne.</summary>
    private void ApplyDrag()
    {
        if (m_sliding)                          //if we're sliding
        {
            m_rigidBody.drag = m_slideDrag;     //Apply slide drag.
        }
        else if (m_grounded)                    //if we're on the ground but not sliding:
        {
            m_rigidBody.drag = m_groundDrag;    //Apply the ground drag.
        }
        else                                   //If we're in the air:
        {
            m_rigidBody.drag = m_airDrag;       //Apply air drag.
        }
    }

    /// <summary>Prevents velocity from exceeding the specified maximum.</summary>
    private void LimitVelocity()
    {
        if (m_rigidBody.velocity.magnitude > m_maxVelocity) //If velocity has exceeded the maximum...
        {
            m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_maxVelocity; //...Normalize it and multiply it by the max, thereby limiting it.
        }
    }

    /// <summary>Checks if player needs to mantle a surface they missed</summary>
    private void MantleCheck()
    {
        if (m_grounded)
        {
            m_mantleEnabled = true;
        }

        if (!m_grounded && m_mantleEnabled)
        {
            if (Physics.Raycast(m_mantleCheck.position, m_mantleCheck.forward, 0.6f, LayerMask.GetMask("Ground")) &&
                !Physics.Raycast(m_camera.position, m_camera.forward, 0.6f, LayerMask.GetMask("Ground")))
            {
                m_mantleEnabled = false;
                m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
                m_rigidBody.AddForce(transform.up * 10, ForceMode.Impulse);
            }
        }
    }

    #region Jumping

    /// <summary>If we're on the ground, jump, otherwise double jump (if we can).</summary>
    public void Jump()
    {
        if (m_canJump)      //If we can jump...
        {
            if (m_grounded) //...and we're trying to jump from the ground:
            {
                m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);    //Apply an upwards force
            }
            else if(m_wallJump)
			{
                m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);                    //Apply an upwards force
                m_wallCount = -1;
                m_wallJump = false;
            }
            else if(m_canDoubleJump)            ///...and we're trying to perform a double jump:
            {
                m_canDoubleJump = false;          //Prevent us from double jumping a second time
                m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);  //Reset our vertical velocity
                m_rigidBody.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);                    //Apply an upwards force
            }
        }
    }

    /// <summary>Checks if the player is grounded by running a sphere collision with a tiny sphere beneath the player. Updates the player's ability jump if they have once again grounded.</summary>
    /// <param name="radius">The radius of the small sphere to spawn beneath the player.</param>
    /// <returns>Sets m_grounded accordingly and returns it.</returns>
    private bool CheckGrounded(float radius = 0.4f)
    {
        m_grounded = Physics.CheckSphere(transform.position - new Vector3(0, m_height / 2 - 1, 0), radius, m_groundMask); //Run a sphere collision below the player to see if we're touching the ground.
        if (m_wallCount >= 0)
        {
            m_wallCount += 1 * Time.deltaTime;
        }
        if(m_wallCount > 0.25)
		{
            m_wallJump = false;
            m_wallCount = -1;
        }

        if (!m_canDoubleJump && m_grounded)   //If we're grounded but can't jump...
        {
            //Rectify that.
            m_canDoubleJump = true;
        }

        

        return m_grounded;
    }

    #endregion

    #region Sliding

    public void StartSliding()
    {
        m_slidingExpected = true;   //Set that we should be sliding
    }

    public void StopSliding()
    {
        m_slidingExpected = false;      //Set that we shouldn't be sliding
        m_sliding = false;              //Make sure we aren't
        m_playerCamera.fieldOfView = m_baseFOV;
        SetHeight(m_heightStanding);    //Return us to our normal height

    }

    /// <summary>If we're meant to be sliding, checks to see if we're grounded. If we are, apply a slide force forward and stop checking if we're meant to be sliding.</summary>
    /// <returns>Whether or not the player slid.</returns>
    private bool TrySlide()
    {
        if (m_slidingExpected)  //If we should be sliding...
        {
            if (m_grounded)     //...and can...
            {
                SetHeight(m_heightSliding); //Set out height to that of a sliding person
                m_rigidBody.AddForce(m_camera.forward * m_slideForce, ForceMode.Impulse);   //Apply the slide force forwards

                m_sliding = true;           //Set that we are sliding (for drag reasons)
                m_slidingExpected = false;  //But reset that we should be sliding, so we can only apply the intial slide force once. We have slid.

                m_playerCamera.fieldOfView = m_slideFOV;
                return true;
            }
        }
        return false;
    }

    /// <summary>Set's m_height to the new height, scales down the capsule and moves the camera accordingly</summary>
    /// <param name="height">The new height.</param>
    private void SetHeight(float height)
    {
        m_height = height;                                                  //set m_height to the new height for use in grounded checks
        m_body.localScale = new Vector3(1, m_height / m_heightStanding, 1); //Scale down the capsule to the new height proportional to the old height
        m_camera.transform.localPosition = new Vector3(0, m_height, 0);     //Transform the camera up/down to the new height
    }

    #endregion

    #endregion

    #region Looking

    /// <summary>Calculates the camera's x and y rotation according to mouse input passed in.</summary>
    /// <param name="delta">Pass in dynamically from input look</param>
    public void CalculateCameraRotation()
    {
        m_yRotation += m_delta.x * m_sensitivity.x * m_lookMultiplier;
        m_xRotation -= m_delta.y * m_sensitivity.y * m_lookMultiplier;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);
    }

    private void Look()
    {
        m_rigidBody.transform.rotation = Quaternion.Euler(0, m_yRotation, 0);
        m_camera.transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);
    }

    #endregion

    #region Health

    #region Damaging

    public bool m_canBeHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject.CompareTag(m_enemyBulletTag))   //If we have collided with a bullet...
        {
            Debug.Log("OWW");
            Destroy(collision.gameObject); //Destroy that bullet
            if (!m_canBeHit)
            {
                Damage();   //Take damage.
            }
        }
        else if (collision.gameObject.layer==3)   //If we have collided with a Wall...
        {
            Debug.Log("Wally");
            m_wallJump = true;
            m_wallCount = -1;
        }
    }

	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.layer == 3)   //If we have collided with a bullet...
        {
            m_wallCount = 0;
        }
    }

IEnumerator DashDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

        public void Damage()
    {
        if (m_damaged)  //If we're already damaged, kill the playewr
        {
            m_audioSource.PlayOneShot(m_Clip, m_volume);
            Debug.Log("Dead LOL");
            Kill();
        }
        else    //Otherwise...
        {
            m_damaged = true;   //Set us to be damaged
            m_recoveryTime = Time.time + m_recoveryDuration;    //Set a time when we will be restored
        }
    }

    /// <summary>Called when the player takes damaged while damaged, or can be called publicly for instakills. Will call respawn.</summary>
    public void Kill()
    {
        Respawn();

    }

    /// <summary>Transform the player to the spawnpoints transform, and then reload the current level</summary>
    public void Respawn()
    {
        m_damaged = false;
        gameObject.transform.position = m_spawnPoint.position;   //Move the player to the position of the spawnpoint
        GetComponent<PlayerLook>().SetRotation(m_spawnPoint.rotation);  //Turn the player to face the same direction as the spawnpoint. Currently nonfunctional due to issues with turning code

        GetComponent<s_levelLoader>().ReloadLevel();    //Call the level loader to reload the current level. Will not reload the corridor though, luckily.
    }


	#endregion

	#region Recovering

	/// <summary>Checks if we've exceeded recovery time, if we have, recover.</summary>
	private void Regenerate()
    {
        if (m_damaged)   //If we're damaged,
        {
            if (Time.time >= m_recoveryTime)    //If we've reached the time we knew we'd've recovered by
            {
                Recover();  //Recover
            }
        }
    }

    /// <summary>Set m_damaged to false. Include any visual effects later if you like. IDC</summary>
    public void Recover()
    {
        m_damaged = false;
    }

    #endregion

    #endregion

    #region UI

    public void TogglePause()
    {
        if (m_pauseMenu.m_open)
        {
            ClosePause();
        }
        else
        {
            OpenPause();
        }
    }

    private void OpenPause()
    {
        m_playerInput.SwitchCurrentActionMap("UI");

        m_leftHand.Cancel();
        m_rightHand.Cancel();
        m_weaponWheel.Close();
        m_hud.Close();
        m_pauseMenu.Open();
    }

    private void ClosePause()
    {
        m_playerInput.SwitchCurrentActionMap("Player");

        m_hud.Open();
        m_pauseMenu.Close();
    }

    public void ToggleWeaponWheel()
    {
        if (m_weaponWheel.m_open)
        {
            CloseWeaponWheel();
        }
        else
        {
            OpenWeaponWheel();
        }
    }

    private void OpenWeaponWheel()
    {
        m_playerInput.SwitchCurrentActionMap("UI");

        m_weaponWheel.Open();
        m_leftHand.Cancel();
        m_rightHand.Cancel();
    }

    private void CloseWeaponWheel()
    {
        m_playerInput.SwitchCurrentActionMap("Player");

        m_weaponWheel.Close();
    }

    public void OpenVictoryScreen()
    {
        m_playerInput.SwitchCurrentActionMap("UI");

        m_leftHand.Cancel();
        m_rightHand.Cancel();
        m_weaponWheel.Close();
        m_hud.Close();
        m_victoryScreen.Open();
    }

    #endregion
}
