using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region editor variables

    public GameObject focus;
    //public GameObject test;

    public Vector3 offset;


    public float minDistance;
    public float maxDistance;
    public float heigthDetect;
    public float minHeight;
    public float smooth;
    public float focusYAdd;

    public Text verticalText;
    public Text horizontalText;
    public Text DistanceText;

    public bool blocByEnv = true;

    public LayerMask mask;

    public float maxY = 0;
    public float minY = 90;

    public float maxX = 0;
    public float minX = 360;


    #endregion editor variables

    #region other variables

    [SerializeField]
    private float distance = 20;
    
    private float targetAngle = 0;
    private const float rotationAmount = 1.0f;

    [SerializeField]
    private float v = 45;

    [SerializeField]
    private float h = 45;

    private float z;



    #endregion other variables

    #region unity methods

    private void Awake()
    {
        setVerticalUI(v);
        setHorizontalUI(h);
        SetUIText(DistanceText,Distance);
        //test = GameObject.CreatePrimitive(PrimitiveType.Sphere); 

    }

    private void Start()
    {
        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        transform.position = focus.transform.position - offset * Distance;
        transform.LookAt(focus.transform);
    }

    private void LateUpdate()
    {
        MoveSmoothlyCam();
        RotateSmoothlyCam();
    }

    #endregion unity methods

    #region move Camera
    public void MoveSmoothlyCam()
    {
        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        Vector3 newPosition;
        if (blocByEnv)
        {
            newPosition = DetectPosition(FocusPosition(), offset);
        }
        else
        {
            newPosition = FocusPosition() - offset * Distance;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, smooth * Time.deltaTime);
    }

    public void RotateSmoothlyCam()
    {
        Vector3 lTargetDir = focus.transform.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * smooth);
    }

    public void MoveCam()
    {
        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        transform.position = DetectEnvironnemnt(focus.transform.position - offset * Distance);
        transform.LookAt(focus.transform);
    }

    public Vector3 DetectEnvironnemnt(Vector3 p1)
    {
        RaycastHit hit;


        if (Physics.Raycast(p1, -Vector3.up, out hit, heigthDetect, mask) && hit.distance < minHeight)
        {
            p1.y += minHeight;
            V += hit.distance * minHeight;
        }
        return p1;
    }

    public Vector3 DetectPosition(Vector3 p1, Vector3 offset)
    {
        RaycastHit hit;

        Debug.DrawRay(p1, -offset * distance, Color.green);

        if (Physics.Raycast(p1, -offset, out hit, Distance, mask))
        {
            //Debug.Log(hit.point);
            //test.transform.position = hit.point + offset;
            p1 = hit.point;
            p1.y += minHeight;
            return p1 + offset;
        }
        return p1 - offset * Distance;
    }

    public Vector3 FocusPosition()
    {
        return focus.transform.position + new Vector3(0,focusYAdd,0);
    }

    #endregion

    #region getter/setter

    public float V
    {
        get
        {
            return v;
        }

        set
        {
            v = Mathf.Clamp(value, 0, 90);
            setVerticalUI(v);
        }
    }

    public float H
    {
        get
        {
            return h;
        }

        set
        {
            h = (value < 0) ? value + 360 : (value > 360) ? value - 360 : value;
            setHorizontalUI(h);
        }
    }

    public float Z
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
        }
    }

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
            SetUIText(DistanceText,value);
        }
    }

    #endregion getter/setter

    #region UI

    public void setHorizontalUI(float value)
    {
        if (horizontalText != null)
        {
            horizontalText.text = value.ToString();
        }
    }

    public void setVerticalUI(float value)
    {
        if (verticalText != null)
        {
            verticalText.text = value.ToString();
        }
    }

    public void SetUIText(Text uiText, float value)
    {
        if (verticalText != null)
        {
            uiText.text = value.ToString();
        }
        else
        {
            Debug.Log("CameraController ==> uiText manquant");
        }
    }
    #endregion UI
}