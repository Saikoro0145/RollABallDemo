using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class InGameManager : MonoBehaviour
{
    // ゲームクリア時に表示するテキストUI（インスペクターで割り当てる）
    [SerializeField] private TMP_Text GameClearText;
    // プレイヤーの移動を制御するコンポーネント（インスペクターで割り当てる）
    [SerializeField] private PlayerController PlayerController;

    void Start()
    {
        // 最初はゲームクリアテキストを非表示にする
        GameClearText.gameObject.SetActive(false);
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
    }
}
