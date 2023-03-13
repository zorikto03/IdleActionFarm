using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float TurningSpeed;
    [SerializeField] float Gravity = -9.81f;
    [SerializeField] Transform groundCheckerPivot;
    [SerializeField] float checkRadius = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Joystick joystick;
    //Rigidbody rb;
    CharacterController controller;

    float horizontal = 0f;
    float vertical = 0f;
    float _velocityZ = 0f;

    bool _isMove = true;


    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CharacterAnimator.EnableCutterEvent += EnableCutterEventHandler;
    }

    private void OnDisable()
    {
        CharacterAnimator.EnableCutterEvent -= EnableCutterEventHandler;
    }

    private void EnableCutterEventHandler(bool value)
    {
        _isMove = !value;
    }

    private void Update()
    {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (IsOnTheGround())
        {
            _velocityZ = -2;
        }

        Movement();
        DoGravity();
    }

    void Movement()
    {
        //rb.AddTorque(transform.up * horizontal * TurningSpeed);
        var movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        if (_isMove)
        {
            controller.Move(movement * Speed);
        }
        
        if (Vector3.Angle(Vector3.forward, movement) > 1f || Vector3.Angle(Vector3.forward, movement) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, movement, TurningSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    void DoGravity()
    {
        _velocityZ += Gravity * Time.fixedDeltaTime;

        controller.Move(Vector3.up * _velocityZ * Time.fixedDeltaTime);
    }

    bool IsOnTheGround()
    {
        bool res = Physics.CheckSphere(groundCheckerPivot.position, checkRadius, groundMask);

        return res;
    }

}
