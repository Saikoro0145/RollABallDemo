using UnityEngine;
using TMPro;
using System.Collections;

public class InGameManager : MonoBehaviour
{
    // ゲームの状態を表す列挙型（enum）。
    private enum GameState
    {
        GameStart,
        Playing,
        GameClear,
        GameOver
    }

    // 現在のゲーム状態。
    private GameState currentState;
    // タイマーを制御するコンポーネント（インスペクターで割り当てる）
    [SerializeField] private TimerController TimerController;
    // ゲームクリア時に表示するテキストUI（インスペクターで割り当てる）
    [SerializeField] private TMP_Text GameClearText;
    // プレイヤーの移動を制御するコンポーネント（インスペクターで割り当てる）
    [SerializeField] private PlayerController PlayerController;
    // ゲームオーバー時に表示するテキストUI（インスペクターで割り当てる）
    [SerializeField] private TMP_Text GameOverText;
    // カウントダウン表示を制御するコンポーネント（インスペクターで割り当てる）
    [SerializeField] private CountDownController CountDownController;

    void Start()
    {
        // 最初はゲームクリアテキストを非表示にする
        GameClearText.gameObject.SetActive(false);
        // 最初はゲームオーバーテキストを非表示にする
        GameOverText.gameObject.SetActive(false);
        // ゲーム開始処理を呼び出す
        ChangeState(GameState.GameStart);
    }

    void Update()
    {
        // プレイ中のときだけアイテム数を確認する
        if (currentState == GameState.Playing)
        {
            // シーン内で "Item" タグが付いたオブジェクトの数を調べる
            int itemCount = GameObject.FindGameObjectsWithTag("Item").Length;

            // アイテムが0個になったらゲームクリア状態に切り替える
            if (itemCount == 0)
            {
                ChangeState(GameState.GameClear);
            }

            // プレイヤーが特定の高さ以下に落ちたらゲームオーバー状態に切り替える
            if (PlayerController.transform.position.y < -10f)
            {
                ChangeState(GameState.GameOver);
            }
        }
    }

    /// <summary>
    /// ゲーム開始処理を行うコルーチン。
    /// カウントダウンが終わるまで待ってからプレイ状態に移行する。
    /// </summary>
    private IEnumerator GameStart()
    {
        // CountDownController のカウントダウンが完了するまでここで待機する
        // yield return StartCoroutine(...) で別のコルーチンの終了を待てる
        yield return StartCoroutine(CountDownController.StartCountDown());
        // カウントダウンが終わったらプレイ状態に切り替える
        ChangeState(GameState.Playing);
    }

    /// <summary>
    /// ゲームの状態を切り替えるメソッド。
    /// 新しい状態に応じた処理を実行する。
    /// </summary>
    /// <param name="newState">切り替え先の状態</param>
    private void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.GameStart:
                // カウントダウンコルーチンを開始する
                // StartCoroutine でコルーチンを実行すると、他の処理と並行して動かせる
                StartCoroutine(GameStart());
                break;

            case GameState.Playing:
                // プレイヤーが動けるようにする
                PlayerController.CanMove = true;
                // タイマーのカウントを開始する
                TimerController.StartTimer();
                break;

            case GameState.GameClear:
                // プレイヤーを完全に固定して動かなくする
                PlayerController.FreezePlayer();
                // タイマーを止める
                TimerController.StopTimer();
                // ゲームクリアテキストを表示する
                GameClearText.gameObject.SetActive(true);
                break;

            case GameState.GameOver:
                // プレイヤーを完全に固定して動かなくする
                PlayerController.FreezePlayer();
                // タイマーを止める
                TimerController.StopTimer();
                // ゲームオーバーテキストを表示する
                GameOverText.gameObject.SetActive(true);
                break;
        }
    }
}
