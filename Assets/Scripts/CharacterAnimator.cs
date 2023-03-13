using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject Cutter;
    [SerializeField] GameObject Slicer;

    Animator animator;
    bool isRun;

    public delegate void CutterIsEnable(bool value);
    public static CutterIsEnable EnableCutterEvent; // for stop moving player while play cut animation 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (!isRun)
            {
                animator.SetTrigger("Run");
            }
            isRun = true;
        }
        else
        {
            if (isRun)
            {
                animator.SetTrigger("Idle");
            }
            isRun = false;
        }

        animator.SetBool("IsRun", isRun);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Punch");
        }
    }

    public void CutButton()
    {
        animator.SetTrigger("Punch");
    }

    public void EnableCutter()
    {
        Cutter.SetActive(true);
        EnableCutterEvent.Invoke(true);
    }
    public void DisableCutter()
    {
        Cutter.SetActive(false);
        EnableCutterEvent.Invoke(false);
    }

    public void EnableSlicer()
    {
        Slicer.SetActive(true);
    }

    public void DisableSlicer()
    {
        Slicer.SetActive(false);
    }
}
