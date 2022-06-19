using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensivity;

    [SerializeField] private Transform _player;

    private float _mouseX;
    private float _mouseY;

    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensivity * Time.deltaTime;

        _player.Rotate(Vector3.up * _mouseX);

        _xRotation -= _mouseY;

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90);
    }
}
