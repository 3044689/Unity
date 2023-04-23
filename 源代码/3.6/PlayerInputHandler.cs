using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    
    private void Awake()
    {
        Instance = this;
    }
  
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private  Vector3 Getmoveinput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horozontal"), 0, Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        return move;
    }
}
