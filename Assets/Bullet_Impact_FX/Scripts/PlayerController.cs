using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private CharacterController m_CharacterController;
    private bool m_Jump = false;
    private Vector2 m_Input;
    private Vector3 m_MoveDirection = Vector3.zero;
    private float m_Gravity = 50.0f;
    private float m_WalkSpeed = 5f;

    // Use this for initialization
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float speed;

        GetInput(out speed);

        if (m_CharacterController.isGrounded)
        {
            m_MoveDirection = new Vector3(m_Input.x, 0, m_Input.y);
            m_MoveDirection = transform.TransformDirection(m_MoveDirection);
            m_MoveDirection *= speed;
        }
        m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        m_CharacterController.Move(m_MoveDirection * Time.deltaTime);
    }

    
    private void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        speed = m_WalkSpeed;

        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }
}