using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
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

    [SerializeField]
    private bool _canSprint = true;
    public bool CanSprint
    {
        get { return _canSprint; }
        set
        {
            if (_canSprint != value)
            {
                _canSprint = value;
                SetMoveSpeed();
            }
        }
    }

    [SerializeField]
    private bool _canJump = true;
    public bool CanJump
    {
        get { return _canJump; }
        set {
            if (_canJump != value)
            {
                _canJump = value;
            }
        }
    }

    [SerializeField]
    private bool canCrunch = true;
    public bool CanCrunch
    {
        get { return canCrunch; }
        set
        {
            if (canCrunch != value)
            {
                canCrunch = value;
                SetMoveSpeed();
            }
        }
    }


    [SerializeField]
    public float CharacterRadius = 0.5f;

    [SerializeField]
    public float CharacterHeight = 1.6f;

    [SerializeField]
    public float CharacterHeightCrouching = 1f;

    [SerializeField]
    public float Speed_Walk =6;

    [SerializeField]
    public float Speed_AddOfSprint = 2;

    [SerializeField]
    public float Speed_Crouch = 0.5f;

    #endregion

    #region　プレイヤ―のリアルタイム値

    [Header("MyCharacter States")]

    [SerializeField]
    private Vector2 currentMoveDirection; //最新の移動方向

    //[SerializeField]
    //private bool isMoving = false;

    [SerializeField]
    private bool _isSprint = false;
    protected bool IsSprint
    {
        get { return _isSprint; }
        set
        {
            if (_isSprint != value)
            {
                _isSprint = value;
                SetMoveSpeed();
            }
        }
    }

    [SerializeField]
    private bool onGround = true;

    [SerializeField]
    private bool isCrunch = false; //かがみ中

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

        if(_tpsCameraControl == null)
        { _tpsCameraControl = GetComponent<TpsCameraControl>(); }




        SetMoveSpeed();

    }

    private void OnEnable()
    {
        PlayerInputReceiver.OnPlayerMove += PlayerMove;
        PlayerInputReceiver.OnPlayerLook += PlayerLook;

        PlayerInputReceiver.OnPlayerSprint += PlayerSprint;
    }
    private void OnDisable()
    {
        PlayerInputReceiver.OnPlayerMove -= PlayerMove;
        PlayerInputReceiver.OnPlayerLook -= PlayerLook;

        PlayerInputReceiver.OnPlayerSprint -= PlayerSprint;
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

    #region PlayerLook
    public void PlayerLook(Vector2 v)
    {
        _tpsCameraControl?.RotateCamera(v);
        lookRotating = v;
    }
    #endregion

    #region PlayerSprint
    public void PlayerSprint(bool pushed)
    {
        IsSprint = pushed;
    }
    #endregion


    protected void SetMoveSpeed()
    {
        float speed　=0;

        //空中時
        if(onGround == false)
        { 
            //空中時は移動スピードを変更できない
        }
        //かがみ中
        else if (CanCrunch && isCrunch)
        {
            speed = Speed_Crouch;
        }
        //走行中
        else if (CanSprint && IsSprint)
        {
            speed = Speed_Walk + Speed_AddOfSprint;
        }
        //徒歩
        else
        {
            speed = Speed_Walk;
        }

        _moveControl.MoveSpeed = speed;
    }
    
}
