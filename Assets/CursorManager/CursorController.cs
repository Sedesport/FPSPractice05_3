using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.TinyCharacterController.Control;
using UnityEngine;
using UnityEngine.LowLevel;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;

public class CursorController : MonoBehaviour
{

    [SerializeField]
    private bool cursorLocked = true;

    #region 参照コンポーネント
    [Header("Components Referenced")]

    [SerializeField]
    private PlayerInputReceiver _playerInputReceiver;

    
    ////カーソル表示時のカーソル形状
    //[SerializeField]
    //private Texture2D CursorArrowTexture;

    ////カーソル固定非表示時のカーソル形状
    //[SerializeField]
    //private Texture2D CursorCenterTexture;


    #endregion



    void Start()
    {
        if (_playerInputReceiver == null)
        { throw new NullReferenceException("_playerInputReceiver == null"); }

        //if (CursorCenterTexture == null)
        //{
        //    throw new NullReferenceException("CursorCenterTexture == null");
        //}
    }

    private void OnEnable()
    {
        PlayerInputReceiver.OnPlayerCursorLock += ChangeCursorLock;
        ChangeCursorLock(cursorLocked);
    }
    private void OnDisable()
    {
        PlayerInputReceiver.OnPlayerCursorLock -= ChangeCursorLock;
        ChangeCursorLock(false);
    }

    protected virtual void ChangeCursorLock(bool locked)
    {
        if (locked) 
        {
            cursorLocked = true;
            // カーソルを中央にロック
            Cursor.lockState = CursorLockMode.Locked;

            // カーソルを非表示にする（必要に応じて）
            Cursor.visible = false;
        }
        else
        {
            cursorLocked = false;
            // カーソルロックを解放
            Cursor.lockState = CursorLockMode.None ;

            // カーソルを非表示にする（必要に応じて）
            Cursor.visible = true;
        }

    }
}
