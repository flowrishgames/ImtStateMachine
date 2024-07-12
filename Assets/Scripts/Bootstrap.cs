using IceMilkTea.StateMachine;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SmartBehaviourScript : MonoBehaviour
{
    // 状態型クラスの型定義を行うことで状態型の記述が楽になる
    private class MyState : ImtStateMachine<SmartBehaviourScript, StateEvent>.State { }

    // 状態イベントの型定義
    private enum StateEvent
    {
        Finish,
        Click,
    }

    private CancellationTokenSource cancellationTokenSource;

    // ステートマシンの宣言だけは普段通り
    private ImtStateMachine<SmartBehaviourScript, StateEvent> stateMachine;


    private void Awake()
    {
        cancellationTokenSource = new CancellationTokenSource();

        stateMachine = new ImtStateMachine<SmartBehaviourScript, StateEvent>(this);
        stateMachine.AddTransition<IdleState, MainLoopState>(StateEvent.Finish);
        stateMachine.AddTransition<MainLoopState, EndState>(StateEvent.Click);
        stateMachine.SetStartState<IdleState>();
    }

    private void Start()
    {
        LoopAsync();
    }
    [SerializeField]
    private bool isLoop = true;
    private async void LoopAsync()
    {
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            await stateMachine.Update(cancellationTokenSource);
        }
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }

    // このクラスの状態クラス型は MyState型 このため非常に状態クラスであることが見やすくなる
    private class IdleState : MyState
    {
        protected override async UniTask Enter(CancellationToken ct)
        {
            Debug.Log($"IdleState Enter:{Time.frameCount}");
            await UniTask.WaitForSeconds(1f);

            StateMachine.SendEvent(StateEvent.Finish);
        }
        protected override async UniTask Update(CancellationToken ct)
        {
            Debug.Log($"IdleState Update:{Time.frameCount}");
            await UniTask.Yield();
        }

        protected override async UniTask Exit(CancellationToken ct)
        {
            Debug.Log($"IdleState Exit:{Time.frameCount}");
            await UniTask.WaitForSeconds(3f);
            await base.Exit();
        }

    }


    private class MainLoopState : MyState
    {
        float waitTime;
        protected override async UniTask Enter(CancellationToken ct)
        {
            waitTime = 3f;
            Debug.Log($"MainLoopState Exit:{Time.frameCount}");
            await UniTask.WaitForSeconds(1f);
        }

        protected override async UniTask Update(CancellationToken ct)
        {
            Debug.Log($"MainLoop : waitTime={waitTime}");
            waitTime -= Time.deltaTime;

            if (waitTime < 0)
            {
                StateMachine.SendEvent(StateEvent.Click);
            }
            await UniTask.Yield();
        }

        protected override async UniTask Exit(CancellationToken ct)
        {
            Debug.Log($"MainLoopState Exit:{Time.frameCount}");
            await UniTask.WaitForSeconds(1f);
            await base.Exit();
        }
    }


    private class EndState : MyState
    {
        protected override async UniTask Enter(CancellationToken ct)
        {
            Debug.Log($"EndState:{Time.frameCount}");
            await base.Enter();
        }
    }
}
