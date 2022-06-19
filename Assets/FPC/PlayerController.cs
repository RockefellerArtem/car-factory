using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public enum TypeController
    {
        Mobile,
        PC
    }

    [SerializeField] private TypeController _typeController;
    private Vector3 _moveDirection;
    private float _xMove;
    private float _zMove;

    public bool isSmooth;


    [SerializeField] private float _smoothDump;

    [SerializeField] private float _speed;
    [SerializeField] private float _sensetive;
    [SerializeField] private float _rayDistance;

    [SerializeField] private GameObject _cameraFPC;
    [SerializeField] private Camera _camera;
    private CharacterController _characterController;

    private float _xRot;
    private float _yRot;

    private float _xRotCurrent, _yRotCurrent;
    private float _currentVelocityY, _currentVelocityX;


    [SerializeField] private Joystick _joystick;
    [SerializeField] private TouchField _touchField;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
        Interactions();
    }
    private void Movement()
    {
        if(_typeController == TypeController.PC)
        {
            _xMove = Input.GetAxis("Horizontal");
            _zMove = Input.GetAxis("Vertical");
        }
        else
        {
            _xMove = _joystick.Horizontal();
            _zMove = _joystick.Vertical();
        }
       

        _moveDirection = new Vector3(_xMove, 0f, _zMove);
        _moveDirection = transform.TransformDirection(_moveDirection);

        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Rotation()
    {
        if (_typeController == TypeController.Mobile)
        {
            _xRot += _touchField.TouchDist.x * _sensetive * Time.fixedDeltaTime;
            _yRot += _touchField.TouchDist.y * _sensetive * Time.fixedDeltaTime;
        }
        else
        {
            _xRot += Input.GetAxis("Mouse X") * _sensetive * Time.fixedDeltaTime;
            _yRot += Input.GetAxis("Mouse Y") * _sensetive * Time.fixedDeltaTime;
        }
       

        _yRot = Mathf.Clamp(_yRot, -90f, 90f);

        if (isSmooth)
        {
            _yRotCurrent = Mathf.SmoothDamp(_yRotCurrent, _yRot, ref _currentVelocityY, _smoothDump);
            _xRotCurrent = Mathf.SmoothDamp(_xRotCurrent, _xRot, ref _currentVelocityX, _smoothDump);

            _cameraFPC.transform.rotation = Quaternion.Euler(-_yRotCurrent, _xRotCurrent, 0f);
            _characterController.transform.rotation = Quaternion.Euler(0f, _xRotCurrent, 0f);
        }
        else
        {
            _cameraFPC.transform.localRotation = Quaternion.Euler(-_yRot, _xRot, 0f);
            _characterController.transform.localRotation = Quaternion.Euler(0f, _xRot, 0f);
        }

    }

    private void Interactions()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(_cameraFPC.transform.position, _cameraFPC.transform.forward, out raycastHit, _rayDistance))
        {
            Debug.DrawLine(_cameraFPC.transform.position, raycastHit.point, Color.red);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.DrawLine(_cameraFPC.transform.position, raycastHit.point, Color.green);
                Destroy(raycastHit.collider.gameObject);
            }
        }
    }


}
