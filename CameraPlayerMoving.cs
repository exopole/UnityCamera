using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPlayerMoving : MonoBehaviour {

    #region editor variables
    public float rotationAmountX = 1.0f;
    public float rotationAmountY = 1.0f;
    public CameraController cameraController;

    Vector3 mousePositionRef;
    [SerializeField]
    private bool inverseRotate = false;
    private int inverseValue = 1;
    public Toggle toggleInverse;

    private void Awake()
    {
        inverseValue = (inverseRotate) ? -1 : 1;
        toggleInverse.isOn = inverseRotate;
    }

    #endregion
    #region unity methods
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            mousePositionRef = Input.mousePosition;
            Cursor.visible = false;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition - mousePositionRef;
            cameraController.H += rotationAmountX * mousePosition.x * inverseValue;
            cameraController.V += rotationAmountY * mousePosition.y * inverseValue;

            mousePositionRef = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
        }

    }
    #endregion

    #region getter/setter
    public bool InverseRotate
    {
        get
        {
            return inverseRotate;
        }

        set
        {
            inverseRotate = value;
            inverseValue = (value) ? -1 : 1;
            toggleInverse.isOn = value;
        }
    }
    #endregion

    public void InverseRotation(Toggle toggle)
    {
        InverseRotate = toggle.isOn;
    }

}
