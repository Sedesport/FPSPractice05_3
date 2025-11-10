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
    ① InputManager
    　●InputSystemを有効かする
    　　・PackageManagerでUnityRegistoryからInstallする
    　●Projectウインドウ内にAsssets\InputManagerフォルダを作成
    　●ヒエラルキーにInputManagerというEmptyObject作成
    　　・PlayerInputコンポーネントを追加
    　　 ・Actionsプロパティに.InputActionsファイルを設定しAsssets\InputManagerフォルダ内に保存
    　　・↑を受信する処理を実装(PlayerInputReceiver）

    ②Player
    　●Cinemachine
    　　・PackageManagerでUnityRegistoryのPackagesからInstallする
    　●Projectウインドウ内にAsssets\Playerフォルダを作成
    　●ヒエラルキーにPlayerというEmptyObject作成
    　　・子EmptyObjectを追加しCameraRootとする。Yは目線
    　　・子にVirtualCameraオブジェクトを追加
    　　　TransformをCameraRootと同じにする
    　　　・FollowプロパティにCameraRootを指定
        　・Bodyは3rdPersonFollow
        　  ・RigのSholderOffsetのXを0にする
    　　　　・RigのCameraDistanceを0にする
        　・AimはDoNothing
          ・NoiseはBasicMultiChannekPerlinのHandhold_tele_mild(なんでもいい）
      ●CharacterControllerコンポーネントを追加する。ここの設定は好みで
    　　　SkinWidth 0.02
          Center 0, 0.82, 0
          radius 0.3
          Height=1.56
    　　　　
    　●TCCインストール
        ・C:\Develop\開発用ツール・ライブラリ類\TCC\Project_TCC-main.zip
    　　　↑の中にReadme.mdファイル、インストール方法かいてある
       *********************************************************************************
        「
            2. **Zipフォルダを解凍**  
               ダウンロードしたZipフォルダを解凍します。

            3. **パッケージをプロジェクトに配置**  
               解凍したフォルダから以下のパッケージを、自身のプロジェクトの`Packages`フォルダ以下に配置します。（あるいは、パッケージを参照します。）
               - `com.utj.charactercontroller`（キャラクター制御）
               - `com.utj.gameobjectfolder`（フォルダとして扱えるGameObject）
               - `com.utj.savedata`（ゲームの進行データを保存する）
               - `com.utj.scenarioimporter`（テキストからゲームで扱いやすい台本を抽出する）
               - `com.utj.sceneloader`（シーンをGameObjectのように読込・解放する）

            4. **Unityエディターの再起動**  
               インポート完了後、Unityエディターを再起動します。  
               **注意:** インポート直後は`UIElements`などの項目でエラーが発生することがあります。例えば、`TypeLoadException: Could not load type 'Utj.SceneManagement.SceneLoadType' from assembly 'GOSubSceneEditor'.` などですが、このエラーは次回起動以降は発生しませんので、無視してかまいません。
    　　　
        ## SceneLoaderの使用

            SceneLoaderを使用する前に、Addressableが初期化されている必要があります。  
            **注意:** Addressableが初期化されていない場合、SceneLoaderにシーンを登録する事ができません。

            1. **Addressableの有効化**  
               - メニューから`Window > Asset Management > Addressable > Group`を開きます。
               - `Create Addressable Settings`ボタンを押してAddressableを有効にします。

               ![SysInst_Image04.png](./Images/SysInst_Image04.png "SysInst_Image04")
   
        」
        *********************************************************************************
       
    　●CharacterBrainコンポーネントを追加する。これが無いと動かない
    　●MoveControlコンポーネントを追加する。
            ・CharacterSettingコンポーネントももれなく追加される。
    　　　　　HeightとRadiusをCharacterControllerと合わせておくといいかも
    　●PlayerCharacterControllerスクリプトを新規作成する。
    　●OnMoveを処理するPlayerMove。OnEnabledとOnDisableにも処理を書く

                [SerializeField]
                private bool onGround = true;
    　　　　　　[SerializeField]
                private Vector2 currentMoveDirection; //最新の移動方向

                public void PlayerMove(Vector2 v)
                {
                    if (onGround == true)
                    {
                        //非ジャンプ状態の場合は普通に動く。
                        _moveControl.Move(v);
                    }
                    currentMoveDirection = v;　//移動方向を記憶しておく
                }
          　　//この時点で実行すると、Wで全身だがadsは回転

    　●TspCameraControlコンポーネントを追加する。cameraRootを指定。
    　●PlayerCharacterControllerスクリプトにOnLook処理。OnEnabledとOnDisableにも処理を書く
            [SerializeField]
            private Vector2 lookRotating = Vector2.zero;

            public void PlayerLook(Vector2 v)
            {
                _tpsCameraControl?.RotateCamera(v);
                lookRotating = v;
            }
          
    　　　　//この時点で実行すると、マウスの動きで
    　　　　上下(上下逆？DefaultTCCCameraSettingのInverseYをチェック) 左右に視線。adsは左右後に進行。

    　●PlayerSeeingViewControllerスクリプトを新規作成する。
    　　//実装内容は本文参照 RaycastHit処理で視認オブジェクト
    　　
    　●
    　●
    　●

    ③CursorManager　EmptyObjectをヒエラルキーに新規作成
    　●CursorControllerという名前でScriptを新規作成。デバッグ実行ではカーソルは消えてくれない
         *********************************************************************************
        「
            [SerializeField]
            private bool cursorLocked = true;

            #region 参照コンポーネント
            [Header("Components Referenced")]

            [SerializeField]
            private PlayerInputReceiver _playerInputReceiver;
            
    ///Start処理内でいろいろ行う
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
            」
        *********************************************************************************
     */
}
