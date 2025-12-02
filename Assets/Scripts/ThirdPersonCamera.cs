using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerOBJ;
    public Rigidbody rb;
    public InputActionReference lookAction;
    public Transform combatLookAt;
    public GameObject thirdPersonCamera;
    public GameObject combatCamera;


    [Header("Camera Settings")]
    public float rotationSpeed;
    public CameraStyle currentStyle;
    
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
        SwitchCameraStyle(currentStyle);
    }

    void Update()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        float mouseX = lookInput.x;
        float mouseY = lookInput.y;

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        if (currentStyle == CameraStyle.Basic)
        {
         
            Vector3 inputDir = orientation.forward * mouseY + orientation.right * mouseX;

            if (inputDir != Vector3.zero)
            {
                playerOBJ.forward = Vector3.Slerp(playerOBJ.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }   
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 combatLookAtDir = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = combatLookAtDir.normalized;

            playerOBJ.forward = combatLookAtDir.normalized;
        }
    }

    void SwitchCameraStyle(CameraStyle newStyle)
    {
        thirdPersonCamera.SetActive(false);
        combatCamera.SetActive(false);

        if (newStyle == CameraStyle.Basic) {thirdPersonCamera.SetActive(true);}
        if (newStyle == CameraStyle.Combat) {combatCamera.SetActive(true);}

        currentStyle = newStyle;
    }

    public void OnThirdPersonSwitch()
    {
        SwitchCameraStyle(CameraStyle.Basic);
    }

    public void OnCombatSwitch()
    {
        SwitchCameraStyle(CameraStyle.Combat);
    }
}
