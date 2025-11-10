using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScreenState.ScreenState;

namespace ScreenState
{
 
    /// <summary>
    /// スクリーンステートの状態管理
    /// 
    /// void Translate(newState)  新しい状態にTranslateする。
    /// </summary>
    public  class ScreenState 
    {
        public enum ScreenStateType
        {
            none = 0,
            Playing = 0x50,
            Pausing = 0x51,
        }

        protected Stack<ScreenStateBase> screenStateStack = new();

        public ScreenStateBase currentState
        {
            get
            {
                if (screenStateStack == null || screenStateStack.Count == 0)
                {
                    return null;
                }
                else
                { return screenStateStack.Peek(); }
            }
        }


        public void InitializeState(ScreenStateType defaultState)
        {
            InitializeState();

            //ScreenStateBase state = ScreenStateFactory.GetInstance(defaultState);
            //screenStateStack.Push(state);
                    }

        protected void InitializeState()
        {
            screenStateStack.Clear();
        }

        /// <summary>
        /// 状態を遷移(移動）する
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        public void Translate(ScreenStateBase newState)
        {
            ScreenStateBase previous = screenStateStack.Pop();
            screenStateStack.Push(newState);

            previous?.OnExit();
            newState.OnEnter();
            
        }


    }


    public abstract class ScreenStateBase
    {
        public abstract ScreenStateType StateType();


        public static event Action<ScreenStateBase> ScreenStateEnter;
        public static event Action<ScreenStateBase> ScreenStateExit;

        public virtual void OnEnter() 
        { 
        
        }
        public virtual void OnExit()
        {
        
        }
    }

    public abstract class ScreenStateSingleton<T> : ScreenStateBase where T : ScreenStateSingleton<T>
    {
        public static T instance;

        protected abstract void Instantiate();
        protected abstract void Terminate();

        public event Action<T> ScreenStateEnter;
        public event Action<T> ScreenStateExit;

        public override void OnEnter() { }
        public override void OnExit() { }
    }


    /// <summary>
    /// ScreenStatePlaying「ゲーム中」状態
    /// </summary>
    public class ScreenStatePlaying : ScreenStateSingleton<ScreenStatePlaying>
    {
        public static ScreenStateType screenStateType = ScreenStateType.Playing;
        public override ScreenStateType StateType() => screenStateType;

        static ScreenStatePlaying()
        {
            ScreenStateFactory.RegistInstance(screenStateType, GetInstance);
        }

        static ScreenStatePlaying GetInstance()
        {
            if (instance == null)
            { instance = new ScreenStatePlaying(); }
            return instance;
        }
        static bool DestroyInstance()
        {
            if (instance == null)
            { return false; }

            instance.Terminate();

            instance = null;
            return true;
        }

        protected ScreenStatePlaying() : base()
        {
            Instantiate();
        }

        protected override void Instantiate()
        { }
        protected override void Terminate()
        { }


    }

    /// <summary>
    /// ScreenStatePausing「ゲーム一時停止中」状態
    /// </summary>
    public class ScreenStatePausing : ScreenStateSingleton<ScreenStatePausing>
    {
        protected ScreenStatePausing() : base() { }
        public override ScreenStateType StateType() => ScreenStateType.Pausing;

        protected override void Instantiate()
        { }
        protected override void Terminate()
        { }
    }

    /// <summary>
    /// ScreenStateファクトリクラス
    /// 各ScreenStateクラスのStaticコンストラクタから
    /// instancesDictionaryにScreenStateTypeをキーにGetInstance()を登録しておく
    /// </summary>
    public static class ScreenStateFactory
    {
        public static Dictionary<ScreenStateType, Func<ScreenStateBase>> instances = new();
        public static ScreenStateBase GetInstance(ScreenStateType type)
        {
            if (instances.ContainsKey(type))
            {
                return instances[type].Invoke();
            }
            else
            {
                throw new ArgumentException($"ScreenState クラス未登録 {type}");
            }
        }
        public static void RegistInstance(ScreenStateType screenStateType, Func<ScreenStateBase> getInstance)
        {
            //BaseのStatic保管庫に登録
            if (instances.ContainsKey(screenStateType))
            {
                instances.Remove(screenStateType);
            }
            instances.Add(screenStateType, getInstance);
        }
    }


}
