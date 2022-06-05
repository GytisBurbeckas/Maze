using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference moveInputActionReference;

    [Min(0f)]
    [SerializeField]
    private float maxMoveAngle = 10f;

    [Min(0f)]
    [SerializeField]
    private float moveSpeed = 10f;

    private new Rigidbody rigidbody;

    private Vector3 targetAngles;
    private Vector3 currentAngles;
    private Vector3 inputAxis;

    private InputAction MoveInputAction
    {
        get
        {
            var action = moveInputActionReference.action;
            if(!action.enabled)
            {
                action.Enable();
            }
            return action;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //pastoviai updatina ir jei paspausta, tai buna value, jei ne tai ne
        UpdateTargetAngles();
        ClampTargetAngles();
        UpdateCurrentAngles();
        ApplyCurrentAngles();
    }

    private void OnEnable()
    {
        //kai paspaudzia
        MoveInputAction.performed += OnMovePerformed;
        MoveInputAction.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        //kai atleidzia mygtuka
        MoveInputAction.performed -= OnMovePerformed;
        MoveInputAction.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        //nuskaito pojudejimus jei juda tai buna atitinkamos value
        inputAxis = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        //jei nieko nespaudzia, aciton tipo, tai buna 0
        targetAngles = Vector3.zero;
        inputAxis = Vector2.zero;
    }

    private void UpdateTargetAngles()
    {
        targetAngles.x += inputAxis.y;
        targetAngles.z -= inputAxis.x;
    }

    private void ClampTargetAngles()
    {
        targetAngles.x = Mathf.Clamp(targetAngles.x, -maxMoveAngle, maxMoveAngle);
        targetAngles.z = Mathf.Clamp(targetAngles.z, -maxMoveAngle, maxMoveAngle); ;
    }

    private void UpdateCurrentAngles()
    {
        var time = Time.deltaTime * moveSpeed;
        currentAngles.x = Mathf.LerpAngle(currentAngles.x, targetAngles.x, time);
        currentAngles.z = Mathf.LerpAngle(currentAngles.z, targetAngles.z, time);
    }

    private void ApplyCurrentAngles()
    {
        var rotation = Quaternion.Euler(currentAngles);
        rigidbody.MoveRotation(rotation);
    }
}
