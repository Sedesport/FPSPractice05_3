using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    �@ InputManager
    �@��InputSystem��L��������
    �@�@�EPackageManager��UnityRegistory����Install����
    �@��Project�E�C���h�E����Asssets\InputManager�t�H���_���쐬
    �@���q�G�����L�[��InputManager�Ƃ���EmptyObject�쐬
    �@�@�EPlayerInput�R���|�[�l���g��ǉ�
    �@�@ �EActions�v���p�e�B��.InputActions�t�@�C����ݒ肵Asssets\InputManager�t�H���_���ɕۑ�
    �@�@�E������M���鏈��������(PlayerInputReceiver�j

    �APlayer
    �@��Cinemachine
    �@�@�EPackageManager��UnityRegistory��Packages����Install����
    �@��Project�E�C���h�E����Asssets\Player�t�H���_���쐬
    �@���q�G�����L�[��Player�Ƃ���EmptyObject�쐬
    �@�@�E�qEmptyObject��ǉ���CameraRoot�Ƃ���BY�͖ڐ�
    �@�@�E�q��VirtualCamera�I�u�W�F�N�g��ǉ�
    �@�@�@Transform��CameraRoot�Ɠ����ɂ���
    �@�@�@�EFollow�v���p�e�B��CameraRoot���w��
        �@�EBody��3rdPersonFollow
        �@  �ERig��SholderOffset��X��0�ɂ���
    �@�@�@�@�ERig��CameraDistance��0�ɂ���
        �@�EAim��DoNothing
          �ENoise��BasicMultiChannekPerlin��Handhold_tele_mild(�Ȃ�ł������j
      ��CharacterController�R���|�[�l���g��ǉ�����B�����̐ݒ�͍D�݂�
    �@�@�@SkinWidth 0.02
          Center 0, 0.82, 0
          radius 0.3
          Height=1.56
    �@�@�@�@
    �@��TCC�C���X�g�[��
        �EC:\Develop\�J���p�c�[���E���C�u������\TCC\Project_TCC-main.zip
    �@�@�@���̒���Readme.md�t�@�C���A�C���X�g�[�����@�����Ă���
       *********************************************************************************
        �u
            2. **Zip�t�H���_����**  
               �_�E�����[�h����Zip�t�H���_���𓀂��܂��B

            3. **�p�b�P�[�W���v���W�F�N�g�ɔz�u**  
               �𓀂����t�H���_����ȉ��̃p�b�P�[�W���A���g�̃v���W�F�N�g��`Packages`�t�H���_�ȉ��ɔz�u���܂��B�i���邢�́A�p�b�P�[�W���Q�Ƃ��܂��B�j
               - `com.utj.charactercontroller`�i�L�����N�^�[����j
               - `com.utj.gameobjectfolder`�i�t�H���_�Ƃ��Ĉ�����GameObject�j
               - `com.utj.savedata`�i�Q�[���̐i�s�f�[�^��ۑ�����j
               - `com.utj.scenarioimporter`�i�e�L�X�g����Q�[���ň����₷����{�𒊏o����j
               - `com.utj.sceneloader`�i�V�[����GameObject�̂悤�ɓǍ��E�������j

            4. **Unity�G�f�B�^�[�̍ċN��**  
               �C���|�[�g������AUnity�G�f�B�^�[���ċN�����܂��B  
               **����:** �C���|�[�g�����`UIElements`�Ȃǂ̍��ڂŃG���[���������邱�Ƃ�����܂��B�Ⴆ�΁A`TypeLoadException: Could not load type 'Utj.SceneManagement.SceneLoadType' from assembly 'GOSubSceneEditor'.` �Ȃǂł����A���̃G���[�͎���N���ȍ~�͔������܂���̂ŁA�������Ă��܂��܂���B
    �@�@�@
        ## SceneLoader�̎g�p

            SceneLoader���g�p����O�ɁAAddressable������������Ă���K�v������܂��B  
            **����:** Addressable������������Ă��Ȃ��ꍇ�ASceneLoader�ɃV�[����o�^���鎖���ł��܂���B

            1. **Addressable�̗L����**  
               - ���j���[����`Window > Asset Management > Addressable > Group`���J���܂��B
               - `Create Addressable Settings`�{�^����������Addressable��L���ɂ��܂��B

               ![SysInst_Image04.png](./Images/SysInst_Image04.png "SysInst_Image04")
   
        �v
        *********************************************************************************
       
    �@��CharacterBrain�R���|�[�l���g��ǉ�����B���ꂪ�����Ɠ����Ȃ�
    �@��MoveControl�R���|�[�l���g��ǉ�����B
            �ECharacterSetting�R���|�[�l���g������Ȃ��ǉ������B
    �@�@�@�@�@Height��Radius��CharacterController�ƍ��킹�Ă����Ƃ�������
    �@��PlayerCharacterController�X�N���v�g��V�K�쐬����B
    �@��OnMove����������PlayerMove�BOnEnabled��OnDisable�ɂ�����������

                [SerializeField]
                private bool onGround = true;
    �@�@�@�@�@�@[SerializeField]
                private Vector2 currentMoveDirection; //�ŐV�̈ړ�����

                public void PlayerMove(Vector2 v)
                {
                    if (onGround == true)
                    {
                        //��W�����v��Ԃ̏ꍇ�͕��ʂɓ����B
                        _moveControl.Move(v);
                    }
                    currentMoveDirection = v;�@//�ړ��������L�����Ă���
                }
          �@�@//���̎��_�Ŏ��s����ƁAW�őS�g����ads�͉�]

    �@��TspCameraControl�R���|�[�l���g��ǉ�����BcameraRoot���w��B
    �@��PlayerCharacterController�X�N���v�g��OnLook�����BOnEnabled��OnDisable�ɂ�����������
            [SerializeField]
            private Vector2 lookRotating = Vector2.zero;

            public void PlayerLook(Vector2 v)
            {
                _tpsCameraControl?.RotateCamera(v);
                lookRotating = v;
            }
          
    �@�@�@�@//���̎��_�Ŏ��s����ƁA�}�E�X�̓�����
    �@�@�@�@�㉺(�㉺�t�HDefaultTCCCameraSetting��InverseY���`�F�b�N) ���E�Ɏ����Bads�͍��E��ɐi�s�B

    �@��PlayerSeeingViewController�X�N���v�g��V�K�쐬����B
    �@�@//�������e�͖{���Q�� RaycastHit����
    �@�@
    �@��
    �@��
    �@��

    �BCursorManager�@EmptyObject���q�G�����L�[�ɐV�K�쐬
    �@��CursorController�Ƃ������O��Script��V�K�쐬�B�f�o�b�O���s�ł̓J�[�\���͏����Ă���Ȃ�
         *********************************************************************************
        �u
            [SerializeField]
            private bool cursorLocked = true;

            #region �Q�ƃR���|�[�l���g
            [Header("Components Referenced")]

            [SerializeField]
            private PlayerInputReceiver _playerInputReceiver;
            
    ///Start�������ł��낢��s��
    ///
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
            �v
        *********************************************************************************
     */
}
