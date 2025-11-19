using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEditor;


//[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReceiver : MonoBehaviour
{
    #region const
        const string  ActionMapNamePlayer = "Player";
        const string  ActionMapNameUI = "UI";
    #endregion

    #region イベント定義

    #region PlayerCharacterモード イベント
    public static event Action<Vector2> OnPlayerMove;
        public static event Action<Vector2> OnPlayerLook;
        //public static event Action<Action> OnPlayerJump;
        public static event Action<bool> OnPlayerSprint;
    public static event Action OnPlayerJump;
    public static event Action<bool> OnPlayerCrouch;
    //public static event Action<Vector2> OnPlayerMousePosition;
    //public static event Action<bool> OnPlayerAim;
    //public static event Action<bool> OnPlayerTouch;



    public static event Action OnPlayerUIMode;
    #endregion

    #region UIモードイベント
    public static event Action OnUIPlayerMode;
    #endregion

    #region ActionMapイベント
    public static event Action<InputActionMap, InputActionMap> OnInputActionMapChanged;
    #endregion

    public static event Action<bool> OnPlayerCursorLock;

    #endregion

    #region Fields


    [Header("Components Referenced")]
    [SerializeField]
    private PlayerInput _playerInput;


    [Header("Character Input Values")]
    [SerializeField]
    private Vector2 move;
    //public  Vector2 MoveValue => move;
    [SerializeField]
    private Vector2 look;
    public Vector2 LookValue => look;
    //[SerializeField]
    //private bool flying;
    //public bool FlyValue => flying;
    [SerializeField]
    private bool sprinting;
    public bool SprintValue
    {
        get { return sprinting; }
        set {
            var previous = sprinting;
            sprinting = value;
            if(previous != value)
            {
                OnPlayerSprint?.Invoke(sprinting);
            }
        }
    }

    [SerializeField]
    private bool crouching;
    public bool CrouchValue
    {
        get { return crouching; }
        set
        {
            var previous = crouching;
            crouching = value;
            if(previous != value)
            {
                OnPlayerCrouch?.Invoke(crouching);
            }
        }
    }
    //[SerializeField]
    //private bool aiming;
    //public bool IsAiming => aiming;
    [SerializeField]
    private Vector2 mousePosition;

    [SerializeField]
    private bool cursorLock = false;

    [Header("ActionMap")]
    [SerializeField]
    private string ActionMapName;

    [Header("Mouse Cursor Settings")]
    [SerializeField, Range(1, 80)]
    private int mouseDeltaScale = 10;

    public bool cursorInputForLook = true;

    #endregion

    #region ActionMap Player

    #region PlayerMove

    private void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
        // var v = InputActionType.IsOpened;
    }

    private void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
        OnPlayerMove?.Invoke(move);
    }
    #endregion

    #region PlayerLook
   public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            //useDeltaScaleX 
            var vec2 = value.Get<Vector2>();
            LookInput(vec2 * mouseDeltaScale);
        }
    }
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
        OnPlayerLook?.Invoke(look);
    }
    #endregion

    #region PlayerSprint

    private void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    public void SprintInput(bool newSprintState)
    {
        sprinting = newSprintState;

    }

    #endregion

    #region PlayerJump
    private void OnJump(InputValue value)
    {
        Debug.Log("PlayerInputReceiver:OnJump");
          JumpInput();
    }
    public void JumpInput()
    {
        //if (flying == false)　
        //{
        //    flying = true;
            //OnPlayerJump?.Invoke(flying);
            OnPlayerJump?.Invoke();
        //}
    }
    #endregion

    #region PlayerCrouch
    private void OnCrouch(InputValue value)
    {
        CrouchInput();
    }
    public void CrouchInput()
    {
        CrouchValue = !CrouchValue;
    }
    #endregion

    #region UIMode
    private void OnUIMode(InputValue value)
    {
        this.SwitchCurrentActionMap(ActionMapNameUI);
        OnPlayerUIMode?.Invoke();


    }
    #endregion

    #region CursorLock

    private void OnCursorLock(InputValue value)
    {
        cursorLock = !cursorLock;
        OnPlayerCursorLock?.Invoke(cursorLock);
    }


    #endregion

    #endregion

    #region ActionMap UI
    private void OnPlayerMode(InputValue value)
    {
        this.SwitchCurrentActionMap(ActionMapNamePlayer);
        OnUIPlayerMode?.Invoke();
    }
    #endregion


    protected void SwitchCurrentActionMap(string actionMapName)
    {
        var previous = _playerInput.currentActionMap;
        _playerInput.SwitchCurrentActionMap( actionMapName);
        ActionMapName = _playerInput.currentActionMap.name;
        OnInputActionMapChanged?.Invoke(previous, _playerInput.currentActionMap);
    }


    // Start is called before the first frame update
    private void Start()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        //色々

        ActionMapName = _playerInput.currentActionMap.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


 
    
}
