using UnityEngine;
using TMPro;
using System.Collections;

public class InGameManager : MonoBehaviour
{
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
        Debug.Log("InGameManager Start!");
        // 最初はゲームクリアテキストを非表示にする
        GameClearText.gameObject.SetActive(false);
        // 最初はゲームオーバーテキストを非表示にする
        GameOverText.gameObject.SetActive(false);
        // ゲーム開始処理を呼び出す
        StartCoroutine(GameStart());
    }

    void Update()
    {
        // シーン内で "Item" タグが付いたオブジェクトの数を調べる
        int itemCount = GameObject.FindGameObjectsWithTag("Item").Length;

        // アイテムが0個になったら
        if (itemCount == 0)
        {
            // ゲームクリアテキストを表示する
            GameClearText.gameObject.SetActive(true);
            // プレイヤーの移動を止める
            PlayerController.FreezePlayer();
        }

        // プレイヤーが特定の高さ以下に落ちたら
        if (PlayerController.transform.position.y < -10f)
        {
            // ゲームオーバーテキストを表示する
            GameOverText.gameObject.SetActive(true);
            // プレイヤーの移動を止める
            PlayerController.FreezePlayer();
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
    }
}
