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


    #region イベント定義

    /// <summary>
    /// プレイヤーが視認しているPropが変更された場合に発生
    /// </summary>
    public event Action<GameObject, GameObject> RayCastHitObjectChanged;

    /// <summary>
    /// プレイヤー視認Prop　Ennter
    /// </summary>
    public event Action<GameObject, float> RayCastHitObjectTriggerEnter;
    /// <summary>
    /// プレイヤー視認Prop　Stay
    /// </summary>
    public event Action<GameObject, float> RayCastHitObjectTriggerStayDistanceChange;
    /// <summary>
    /// プレイヤー視認Prop　Exit
    /// </summary>
    public event Action<GameObject> RayCastHItObjectTriggerExit;


    #endregion


 

    //視認中オブジェクト
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
                    //null →　オブジェクト
                    rayCastHitObject = value;

                    RayCastHitObjectTriggerEnter?.Invoke(value, DistanceToObject);
                    RayCastHitObjectChanged?.Invoke(previous, value);
                }
            }
            else
            {
                if (value == null)
                {
                    //オブジェクト →　null
                    rayCastHitObject = value;
                    RayCastHItObjectTriggerExit?.Invoke(previous);
                    RayCastHitObjectChanged?.Invoke(previous, value);
                }
                else if (GameObject.ReferenceEquals(value, rayCastHitObject))
                {
                    //同じオブジェクトの場合は距離が変化した場合にイベント発生
                    if (!Mathf.Approximately(DistanceToObjectPrevious, DistanceToObject))
                    {
                        RayCastHitObjectTriggerStayDistanceChange?.Invoke(value, DistanceToObject);
                    }
                }
                else
                {
                    //違うオブジェクトへ変化
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
        ////Raycastの方向
        //Ray rayDrawing;

        //カメラの正面
        var rayDirection = CameraRoot.forward;
        var startPoint = CameraRoot.position;
        rayDrawingforward = new Ray(startPoint, rayDirection);

        RaycastHit hit;

        //視認判定

        //var bHit = Physics.Raycast(startPoint, rayDirection, out hit, ViewLineDistance);
        var bHit = Physics.Raycast(startPoint, rayDirection, out hit, ViewLineDistance);

        isRayCastHit = bHit;

        if (bHit)
        //if (Physics.Raycast(rayDrawing, out hit, ViewLineDistance))
        {
            //レイキャスト
            GameObject obj = hit.collider.gameObject;

            if (!GameObject.ReferenceEquals(obj, null))
            {

                if (IsPropGameObject(obj))
                {
                    RayCastHitObject = obj;
                    RayCastHitObjectName = obj.name;

        //接触可能判定
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
        #region　デバッグ用機能
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

    //オブジェクトのレイヤーが視認対象レイヤーかどうか
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
