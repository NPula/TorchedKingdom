using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController m_instance;
    public static PlayerController Instance { get { return m_instance; } }

    [SerializeField] private float playerMoveSpeed = 1f;
    
    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    private Vector2 playerMoveInput;
    private Vector2 playerVelocity;

    public string TransitionName { get; set; }

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get player input and set the players velocity.
        playerMoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        playerVelocity = playerMoveInput.normalized * playerMoveSpeed * Time.deltaTime;
        
        // doesnt work well with collisions but it stops movement jitteryness with the camera.
        // TODO - create a different movement/collision system using raycasts instead later. 
        transform.Translate(playerVelocity);

        // Change player animations
        SetPlayerAnimatorState();
    }

    void FixedUpdate()
    {
        // Get the normalized player velocity
        //playerVelocity = playerMoveInput.normalized * playerMoveSpeed * Time.deltaTime;

        // Move the player.
        //playerRb.MovePosition(playerRb.position + playerVelocity);
    }

    void SetPlayerAnimatorState()
    {
        // Change player movement animations if move input is pressed.
        playerAnimator.SetFloat("movementX", playerMoveInput.x);
        playerAnimator.SetFloat("movementY", playerMoveInput.y);

        // If playerMoveInput is not zero then store the last x and y coordinate to set the idle direction
        if (playerMoveInput != Vector2.zero)
        {
            playerAnimator.SetFloat("lastX", playerMoveInput.x);
            playerAnimator.SetFloat("lastY", playerMoveInput.y);
        }
    }
}
