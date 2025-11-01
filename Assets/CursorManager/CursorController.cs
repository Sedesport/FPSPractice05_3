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

    #region �Q�ƃR���|�[�l���g
    [Header("Components Referenced")]

    [SerializeField]
    private PlayerInputReceiver _playerInputReceiver;

    
    ////�J�[�\���\�����̃J�[�\���`��
    //[SerializeField]
    //private Texture2D CursorArrowTexture;

    ////�J�[�\���Œ��\�����̃J�[�\���`��
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
            // �J�[�\���𒆉��Ƀ��b�N
            Cursor.lockState = CursorLockMode.Locked;

            // �J�[�\�����\���ɂ���i�K�v�ɉ����āj
            Cursor.visible = false;
        }
        else
        {
            cursorLocked = false;
            // �J�[�\�����b�N�����
            Cursor.lockState = CursorLockMode.None ;

            // �J�[�\�����\���ɂ���i�K�v�ɉ����āj
            Cursor.visible = true;
        }

    }
}
