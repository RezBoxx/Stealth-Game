using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldPlayerController : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 6.0f;
    public float m_Gravity = 20.0f;
    public float m_LookSens = 1.0f;

    CharacterController characterController;
    private string m_VerticalAxisName;
    private string m_HorizontalAxisName;
    private float m_VerticalAxisInput;
    private float m_HorizontalAxisInput;

    private string m_MouseVerticalAxisName;
    private string m_MouseHorizontalAxisName;
    private float m_MouseVerticalAxisInput;
    private float m_MouseHorizontalAxisInput;

    Camera kamera;
    public GameObject flashlight;
    public bool hasFlashlight;

    public GameObject NightvisionFX;
    public bool hasNightvision;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        kamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        m_MouseVerticalAxisName = "Mouse Y" + m_PlayerNumber;
        m_MouseHorizontalAxisName = "Mouse X" + m_PlayerNumber;
        m_VerticalAxisName = "Vertical" + m_PlayerNumber;
        m_HorizontalAxisName = "Horizontal" + m_PlayerNumber;
    }

    public bool crouching;

    // Update is called once per frame
    void Update()
    {
        m_HorizontalAxisInput = Input.GetAxis(m_HorizontalAxisName);
        m_VerticalAxisInput = Input.GetAxis(m_VerticalAxisName);
        m_MouseHorizontalAxisInput = Input.GetAxis(m_MouseHorizontalAxisName) * m_LookSens;
        m_MouseVerticalAxisInput = Input.GetAxis(m_MouseVerticalAxisName) * m_LookSens;


        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            SceneManager.LoadScene(1);
        }

        //Simple Crouch
        if (crouching)
        {
            characterController.height = 1;
        }
        else
        {
            characterController.height = 1.85f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;
        }

        //Taschenlampe
        if (hasFlashlight)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }

        //Nachtsicht
        if (hasNightvision)
        {
            NightvisionFX.SetActive(true);
        }
        else
        {
            NightvisionFX.SetActive(false);
        }




        //super toggle 
        if (Input.GetKeyDown(KeyCode.U))
        {
            hasNightvision = !hasNightvision;
            
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            
            hasFlashlight = !hasFlashlight;
        }
    }
    private void FixedUpdate()
    {
        Moving();
        CameraMove();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Moving()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float forSpeed = m_VerticalAxisInput * m_Speed;
        characterController.SimpleMove(forward * forSpeed);
        Vector3 side = transform.TransformDirection(Vector3.right);
        float sidSpeed = m_HorizontalAxisInput * m_Speed;
        characterController.SimpleMove(side * sidSpeed);
        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }
    void CameraMove()
    {
        //transform.rotation = Quaternion.Euler(0f,m_MouseHorizontalAxisInput * 3.0f,0f);
        transform.Rotate(Vector3.up, m_MouseHorizontalAxisInput, Space.World);
        kamera.transform.Rotate(kamera.transform.right, -m_MouseVerticalAxisInput, Space.World);
    }
}
