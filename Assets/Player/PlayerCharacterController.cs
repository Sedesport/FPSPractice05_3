using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEditor.Build.Pipeline.Interfaces;
using Unity.TinyCharacterController.Control;
using Unity.TinyCharacterController.Brain;
using Unity.TinyCharacterController.Effect;
using Unity.TinyCharacterController.Check;
using Unity.TinyCharacterController;

public class PlayerCharacterController : MonoBehaviour
{

    #region 参照コンポーネント
    [Header("Components Referenced")]

    [SerializeField]
    private Transform _cameraRootTransform;

    [SerializeField]
    private PlayerInputReceiver _playerInputReceiver;


    private TpsCameraControl _tpsCameraControl;
    private MoveControl _moveControl = null;
    private CursorPositionControl _cursorPositionControl;
    private CharacterSettings _characterSettings;
    private CharacterController _characterController;

    #endregion

    #region　プレイヤーの基本動作

    [Header("Movement settings")]

    //[SerializeField]
    //private bool canSprint = true;
    //public bool CanSprint
    //{
    //    get { return canSprint; }
    //    set
    //    {
    //        canSprint = value;
    //        if (canSprint == false)
    //        {
    //            PlayerSprint(false);
    //        }
    //    }
    //}

    //[SerializeField]
    //private bool canJump = true;
    //public bool CanJump
    //{
    //    get { return canJump; }
    //    set { canJump = value; }
    //}

    //[SerializeField]
    //private bool canCrounch = true;
    //public bool CanCrounch
    //{
    //    get { return canCrounch; }
    //    set
    //    {
    //        canCrounch = value;
    //    }
    //}



    [SerializeField]
    public float CharacterRadius = 0.5f;

    [SerializeField]
    public float CharacterHeight = 1.6f;

    [SerializeField]
    public float CharacterHeightCrouching = 1f;

    [SerializeField]
    public float Speed_Walk = 2;

    [SerializeField]
    public float Speed_Sprint = 8;

    [SerializeField]
    public float Speed_Crouch = 0.5f;

    #endregion

    #region　プレイヤ―のリアルタイム値

    [Header("MyCharacter States")]

    [SerializeField]
    private Vector2 currentMoveDirection; //最新の移動方向

    [SerializeField]
    private bool isMoving = false;

    [SerializeField]
    private bool isRunning = false;

    [SerializeField]
    private bool onGround = true;

    [SerializeField]
    private bool isCrunching = false; //かがみ中

    //2025
    [SerializeField]
    private Vector2 lookRotating = Vector2.zero;

    #endregion

    #region イベント定義

    public event Action<GameObject, GameObject> OnSeeingObjectChanged;
    public event Action<GameObject, GameObject> OnTouchableObjectChanged;
    public event Action<GameObject> OnPlayerTouched;
    public event Action<GameObject> OnPlayerDetectedTriggerEnter;
    public event Action<GameObject> OnPlayerDetectedTriggerExit;


    //public event Action OnEnableMyCharerControl;
    //public event Action OnDisableMyCharerControl;

    #endregion


    void Start()
    {
        if (_cameraRootTransform == null)
        { throw new NullReferenceException("_cameraRootTransform == null"); }

        if (_playerInputReceiver == null)
        { throw new NullReferenceException("_playerInputReceiver == null"); }
        
        if(_moveControl == null)
        { _moveControl = GetComponent<MoveControl>(); }

    }

    private void OnEnable()
    {
        PlayerInputReceiver.OnPlayerMove += PlayerMove;
    }
    private void OnDisable()
    {
        PlayerInputReceiver.OnPlayerMove -= PlayerMove;
    }


    #region PlayerMove
    public void PlayerMove(Vector2 v)
    {
        if (onGround == true)
        {
            //非ジャンプ状態の場合は普通に動く。
            _moveControl.Move(v);
        }
        currentMoveDirection = v;　//移動方向を記憶しておく
    }
    #endregion

}
