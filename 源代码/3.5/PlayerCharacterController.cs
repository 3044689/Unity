using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;
    public float gravityDownForce = 20f;
    public float maxSpeedOnGround = 8f;
    public float moveSharpnessOnGround = 15f;

    public float cameraHeightRatio = 0.9f;

    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private float  _targetCharacterHeight = 1.8f;

    public Vector3 CharacterVelocity { get; set; }

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();

        _characterController.enableOverlapRecovery=true;

        UpdateCharacterHeight();
    }

    private void Update()
    {
        HandleCharacterMovement();
    }


    private void UpdateCharacterHeight()
    {
        _characterController.height = _targetCharacterHeight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;

        playerCamera.transform.localposition = Vector3.up * _characterController.height * 0.9;
    }


    private void HandleCharacterMovement()
    {
        //move
        Vector3 worldSpaceMoveInput = trensform.TransformVector(_inputHandler.GetmoveInput());

        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldSpaceMoveInput * maxSpeedOnGround;

            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity, moveSharpnessOnGround * Time.deltaTime);

        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }

        _characterController.Move(CharacterVelocity * Time.deltaTime);
    }
}



