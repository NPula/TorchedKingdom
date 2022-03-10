using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController m_instance;
    public static PlayerController Instance { get { return m_instance; } }

    [SerializeField] private float m_playerMoveSpeed = 1f;
    private bool deactivateMovement = false;

    private Rigidbody2D m_playerRb;
    private Animator m_playerAnimator;

    private Vector2 m_playerMoveInput;
    private Vector2 m_playerVelocity;

    public string TransitionName { get; set; }

    private Vector3 m_mapBottomLeftEdge;
    private Vector3 m_mapUpperRightEdge;

    public bool DeactivateMovement { get { return deactivateMovement; } set { deactivateMovement = value; } }

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
        // Subscribe area change event
        EventManager.StartListening("OnMapExit", OnChangeArea);

        m_playerRb = GetComponent<Rigidbody2D>();
        m_playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!deactivateMovement)
        {
            // Get player input and set the players velocity.
            m_playerMoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
            m_playerVelocity = m_playerMoveInput.normalized * m_playerMoveSpeed * Time.deltaTime;
        
            // doesnt work well with collisions but it stops movement jitteryness with the camera.
            // TODO - create a different movement/collision system using raycasts instead later. 
            transform.Translate(m_playerVelocity);

            // Clamp the player position to the tilemap size
            //transform.position = new Vector3(
            //    Mathf.Clamp(transform.position.x, m_mapBottomLeftEdge.x, m_mapUpperRightEdge.x), 
            //    Mathf.Clamp(transform.position.y, m_mapBottomLeftEdge.y, m_mapUpperRightEdge.y));
        }

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
        m_playerAnimator.SetFloat("movementX", m_playerMoveInput.x);
        m_playerAnimator.SetFloat("movementY", m_playerMoveInput.y);

        // If playerMoveInput is not zero then store the last x and y coordinate to set the idle direction
        if (m_playerMoveInput != Vector2.zero)
        {
            m_playerAnimator.SetFloat("lastX", m_playerMoveInput.x);
            m_playerAnimator.SetFloat("lastY", m_playerMoveInput.y);
        }
    }


    // Set the area the player can move in or is confined to.
    public void SetMoveLimit(Vector3 bottomLeftEdgeToSet, Vector3 topRightEdgeToSet)
    {
        m_mapBottomLeftEdge = bottomLeftEdgeToSet;
        m_mapUpperRightEdge = topRightEdgeToSet;
    }

    public void OnChangeArea(Dictionary<string, object> message)
    {
        float newPosX = 0f;
        float newPosY = 0f;

        // TODO - change the hardcoded values later.
        switch (message["direction"])
        {
            case "Up":
                newPosY = 3f;
                break;
            case "Down":
                newPosY = -3f;
                break;
            case "Left":
                newPosX = -4f;
                break;
            case "Right":
                newPosX = 4f;
                break;
        }

        transform.position += new Vector3(newPosX, newPosY);
    }
}
