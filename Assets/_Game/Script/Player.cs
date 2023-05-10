using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rb;

    public bool isCanMove;
    void Start()
    {
        isCanMove = true;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (isCanMove)
        {
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
             //   _isMove = true;
                rb.MovePosition(rb.position + JoystickControl.direct * moveSpeed * Time.fixedDeltaTime);
              //  ChangeAnim(Constant.ANIM_RUN);
                Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

}
