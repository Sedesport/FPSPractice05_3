using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSeeingViewController : MonoBehaviour
{

    [SerializeField]
    public Transform CameraRoot;

    [SerializeField]
    public Image CenterPoint;

    [Header("SeeingView")]
    [Tooltip("SeeingObjectName")]
    [SerializeField]
    public float ViewLineDistance = 3.0f;

    [Tooltip("TouchableObjectName")]
    [SerializeField]
    public float ViewLineTouchableDistance = 0.6f;

    [Header("SeeingObject")]
    private GameObject rayCastHitObject = null;
    [SerializeField]
    protected string RayCastHitObjectName;

    [Tooltip("LayerNameOfProps")]
    [SerializeField]
    public List<string> layerNamePropGameObjects = new List<string>();
    
    //[SerializeField]
    //private string SeeingObjectName;

    //[SerializeField]
    //private string SeeingObjectNamePrevious;

    [SerializeField]
    private float DistanceToObject = float.PositiveInfinity;
    private float DistanceToObjectPrevious;

    [Header("Debugging")]
    [SerializeField]
    private bool VisibleRayLine = true;

    [SerializeField]
    private bool isRayCastHit;


    #region �C�x���g��`

    /// <summary>
    /// �v���C���[�����F���Ă���Prop���ύX���ꂽ�ꍇ�ɔ���
    /// </summary>
    public event Action<GameObject, GameObject> RayCastHitObjectChanged;

    /// <summary>
    /// �v���C���[���FProp�@Ennter
    /// </summary>
    public event Action<GameObject, float> RayCastHitObjectTriggerEnter;
    /// <summary>
    /// �v���C���[���FProp�@Stay
    /// </summary>
    public event Action<GameObject, float> RayCastHitObjectTriggerStayDistanceChange;
    /// <summary>
    /// �v���C���[���FProp�@Exit
    /// </summary>
    public event Action<GameObject> RayCastHItObjectTriggerExit;


    #endregion


 

    //���F���I�u�W�F�N�g
    protected GameObject RayCastHitObject
    {
        get { return rayCastHitObject; }
        set
        {
            GameObject previous = rayCastHitObject;
            if (rayCastHitObject == null)
            {
                if (value == null) { }
                else
                {
                    //null ���@�I�u�W�F�N�g
                    rayCastHitObject = value;

                    RayCastHitObjectTriggerEnter?.Invoke(value, DistanceToObject);
                    RayCastHitObjectChanged?.Invoke(previous, value);
                }
            }
            else
            {
                if (value == null)
                {
                    //�I�u�W�F�N�g ���@null
                    rayCastHitObject = value;
                    RayCastHItObjectTriggerExit?.Invoke(previous);
                    RayCastHitObjectChanged?.Invoke(previous, value);
                }
                else if (GameObject.ReferenceEquals(value, rayCastHitObject))
                {
                    //�����I�u�W�F�N�g�̏ꍇ�͋������ω������ꍇ�ɃC�x���g����
                    if (!Mathf.Approximately(DistanceToObjectPrevious, DistanceToObject))
                    {
                        RayCastHitObjectTriggerStayDistanceChange?.Invoke(value, DistanceToObject);
                    }
                }
                else
                {
                    //�Ⴄ�I�u�W�F�N�g�֕ω�
                    var obj = rayCastHitObject;
                    rayCastHitObject = value;
                    RayCastHItObjectTriggerExit?.Invoke(obj);
                    RayCastHitObjectTriggerEnter?.Invoke(value, DistanceToObject);
                    RayCastHitObjectChanged?.Invoke(previous, value);
                }
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        if (CameraRoot == null)
        {
            throw new System.NullReferenceException(GetType().Name + " CameraRoot");
        }
    }
    private void OnEnable()
    {
        RayCastHitObjectChanged += _ChangedSeeingObject;
    }

    private void OnDisable()
    {
        RayCastHitObjectChanged -= _ChangedSeeingObject;

    }

    void Update()
    {
        ////Raycast�̕���
        //Ray rayDrawing;

        //�J�����̐���
        var rayDirection = CameraRoot.forward;
        var startPoint = CameraRoot.position;
        rayDrawingforward = new Ray(startPoint, rayDirection);

        RaycastHit hit;

        //���F����

        //var bHit = Physics.Raycast(startPoint, rayDirection, out hit, ViewLineDistance);
        var bHit = Physics.Raycast(startPoint, rayDirection, out hit, ViewLineDistance);

        isRayCastHit = bHit;

        if (bHit)
        //if (Physics.Raycast(rayDrawing, out hit, ViewLineDistance))
        {
            //���C�L���X�g
            GameObject obj = hit.collider.gameObject;

            if (!GameObject.ReferenceEquals(obj, null))
            {

                if (IsPropGameObject(obj))
                {
                    RayCastHitObject = obj;
                    RayCastHitObjectName = obj.name;

        //�ڐG�\����
        DistanceToObjectPrevious = DistanceToObject;
                    DistanceToObject = hit.distance;
                    return;
                }
            }

        }
        RayCastHitObject = null;

        DistanceToObject = float.PositiveInfinity;
        RayCastHitObjectName = "";
    }


    Ray rayDrawingforward;

    //[SerializeField]
    //Vector3 origin;

    //[SerializeField]
    //Vector3 length;

    //[SerializeField]
    //Vector3 root;

    private void FixedUpdate()
    {
        #region�@�f�o�b�O�p�@�\
        if (VisibleRayLine)
        {
            Color colF;
            Color colC;
            if (RayCastHitObject == null)
            {
                colF = Color.yellow;
                colC = Color.green;
            }
            else
            {
                colF = Color.red;
                colC = Color.blue;
            }
            //origin = rayDrawingforward.origin;
            //length = rayDrawingforward.direction * 10;
            //root = CameraRoot.position;

            Debug.DrawRay(rayDrawingforward.origin, rayDrawingforward.direction * 10, colF);
            //Debug.DrawRay(rayDrawingCenterPoint.origin, rayDrawingCenterPoint.direction * 10, colC);

            #endregion
        }
    }

        private void _ChangedSeeingObject(GameObject previous, GameObject now)
    {
        //SeeingObjectName = now ? now.name : "null";
        //SeeingObjectNamePrevious = previous ? previous.name : "null";
    }

    //�I�u�W�F�N�g�̃��C���[�����F�Ώۃ��C���[���ǂ���
    protected bool IsPropGameObject(GameObject obj)
    {
        foreach (var s in layerNamePropGameObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(s))
            {
                return true;
            }
        }
        return false;
    }
}
