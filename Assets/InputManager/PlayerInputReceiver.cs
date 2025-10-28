using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


//[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReceiver : MonoBehaviour
{

    #region const
    string ActionMapNamePlayer = "Player";
    string ActionMapNameUI = "UI";
    #endregion

    #region イベント定義
    public static event Action<Vector2> OnPlayerMove;
    public static event Action<Vector2> OnPlayerLook;
    //public static event Action<Action> OnPlayerJump;
    public static event Action<bool> OnPlayerSprint;
    //public static event Action<bool> OnPlayerCrouch;
    //public static event Action<Vector2> OnPlayerMousePosition;
    //public static event Action<bool> OnPlayerAim;
    //public static event Action<bool> OnPlayerTouch;
    public static event Action<bool> OnPlayerUIMode;

    public static event Action<bool> OnUIPlayrMode;




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
    //public bool JumpValue => flying;
    [SerializeField]
    private bool sprinting;
    public bool SprintValue => sprinting;
    //[SerializeField]
    //private bool crouching;
    //public bool CrouchValue => crouching;
    //[SerializeField]
    //private bool aiming;
    //public bool IsAiming => aiming;
    [SerializeField]
    private Vector2 mousePosition;

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
        OnPlayerSprint?.Invoke(sprinting);
    }

    #endregion

    private void OnUIMode(InputValue value)
    {
        this.SwitchCurrentActionMap(ActionMapNameUI);
        
    }

    #endregion

    #region ActionMap UI
    private void OnPlayerMode(InputValue value)
    {
        this.SwitchCurrentActionMap(ActionMapNamePlayer);

    }
    #endregion


    protected void SwitchCurrentActionMap(string actionMapName)
    {
        _playerInput.SwitchCurrentActionMap( actionMapName);
        ActionMapName = _playerInput.currentActionMap.name;
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
