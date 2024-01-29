using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHelper : MonoBehaviour
{
    private InputAction press, screenPosition, touch;
    private PlayerInput playerInput;
    public LineDrawer lineDrawer;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        press = playerInput.actions["Press"];
        screenPosition = playerInput.actions["ScreenPosition"];
        touch = playerInput.actions["Tap"];
    }
    private void OnEnable()
    {
        press.performed += lineDrawer.BeginDraw;
        screenPosition.performed += lineDrawer.setCurrentScreenPosition;
        touch.performed += lineDrawer.test;
    }
    private void OnDisable()
    {
        press.performed -= lineDrawer.BeginDraw;
        screenPosition.performed -= lineDrawer.setCurrentScreenPosition;
        touch.performed -= lineDrawer.test;
    }
}
