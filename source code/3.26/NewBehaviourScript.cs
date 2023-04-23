using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    public  float Speed = 10f;
    public float RotateSpeed = 1f;
    public float Gravity = -9.8f;
    private Vector3 Velocity = Vector3.zero;
    public Transform GroundCheck;
    public float CheckRadius = 0.2f;
    private bool IsGround;
    public LayerMask LayerMask;
    public float JumpHegiht = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller=transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLikeWow();
    }
    private void MoveLikeWow()
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, LayerMask);
        if (IsGround&&Velocity.y<0)
        {
            Velocity.y = 0;
        }

        if (IsGround && Input.GetButtonDown("Jump")) 
        {
            Velocity.y = Mathf.Sqrt(JumpHegiht * -2 * Gravity);
        }

        var Horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");

        var move = transform.forward * Speed * Vertical * Time.deltaTime;
        controller.Move(move);

        Velocity.y += Gravity * Time.deltaTime;
        controller.Move (Velocity * Time.deltaTime);

        transform.Rotate(Vector3.up,Horizontal * RotateSpeed);
    }

    private void MoveLikeTopDown()
    {
        var Horizontal = Input.GetAxis("Horizontal");
        var Vartical = Input.GetAxis("Vartical");

        var direction = new Vector3(Horizontal, 0, Vartical).normalized;
        var move =direction*Speed*Time.deltaTime;
        controller.Move(move);

        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var point = Input.mousePosition - playerScreenPoint;
        var angle = Mathf.Atan2(point.x ,point .y )*Mathf.Rad2Deg;

        transform .eulerAngles=new Vector3(transform.eulerAngles.x,angle,transform.eulerAngles.z );
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Finish"))
        {
            hit.rigidbody.AddForce(transform.forward * Speed);
        }
    }


}
