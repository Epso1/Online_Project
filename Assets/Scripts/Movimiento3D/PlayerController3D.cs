using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : NetworkBehaviour
{
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float velMovimiento;
    [SerializeField] private float velMaxima;
    [SerializeField] private float velRot = 600f;
    [SerializeField] private Transform groundPos;
    [SerializeField] private LayerMask sueloLayer;
    [SerializeField] private Vector3 checkBoxTam;

    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private Vector2 _movimiento;

    private Transform _transform;

    private RaycastHit hit;
    public bool _isGrounded;
    private Vector3 _dirMov;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        _movimiento = _playerInput.actions["Mover"].ReadValue<Vector2>(); //Guardamos el valor del movimiento W A S D 

        //Debug.Log(_movimiento)
        _transform.position += new Vector3( _movimiento.x , 0, _movimiento.y ) * velMovimiento * Time.deltaTime;//Movemos por codigo

        //Comprobamos si estamos tocando el suelo 
        _isGrounded = Grounded(); //Comprobamos si toca el suelo

        _dirMov = new Vector3(_movimiento.x, 0, _movimiento.y).normalized;

        if (_dirMov != Vector3.zero)
        {
            // Giro suave
            Quaternion _dirRotacion = Quaternion.LookRotation(_dirMov);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _dirRotacion, velRot * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
       
        if (_rb.velocity.magnitude <= velMaxima) //controlamos la velocidad
        {
            //_rb.AddForce(new Vector3(_movimiento.x, 0, _movimiento.y) * velMovimiento); //Movimiento aplicando una fuerza (más natural)           
        }
        //_rb.velocity = new Vector3(_movimiento.x * velMovimiento, _rb.velocity.y, _movimiento.y * velMovimiento); //Movimiento modificando la velocidad

    }

    public void Saltar(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        Debug.Log(context.phase);

        if (context.started && _isGrounded)
        {
            _rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    public bool Grounded()
    {
        if (Physics.CheckBox(groundPos.position, checkBoxTam, groundPos.rotation, sueloLayer)) { return true; } else { return false; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundPos.position, checkBoxTam);
    }
}
