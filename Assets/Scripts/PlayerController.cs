using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // SerializeField属性を使用して、Unityエディタで移動速度を調整できるようにする。
    // プレイヤーの移動速度。大きいほど速く動く。
    [SerializeField] private float speed = 5f;

    // Rigidbody は物理演算を担当するコンポーネント（重力・衝突など）
    private Rigidbody rb;

    // 「Move」アクションを参照する変数（Input System で定義したアクション）
    private InputAction moveAction;

    /// <summary>
    /// 最初に一度だけ呼ばれるメソッド。
    /// 必要なコンポーネントの取得と入力アクションの検索を行う。
    /// </summary>
    void Start()
    {
        // このゲームオブジェクトにアタッチされている Rigidbody を取得する。
        rb = GetComponent<Rigidbody>();

        // Input System のアクションマップから "Move" アクションを探して取得する。
        moveAction = InputSystem.actions.FindAction("Move");
    }

    /// <summary>
    /// ハードウェアの性能によらず一定間隔で呼ばれるメソッド。
    /// Upate() よりも物理演算に適している。
    /// プレイヤーへの入力を読み取り、Rigidbody に力を加えて移動させる。
    /// </summary>
    void FixedUpdate()
    {
        // 入力の値を Vector2（X軸・Y軸）で読み取る
        // キーボードなら WASD や矢印キー、コントローラーなら左スティックに対応する
        Vector2 input = moveAction.ReadValue<Vector2>();

        // 2D入力（画面の横・縦）を3D空間の移動ベクトルに変換する
        // Y軸（上下）は動かさないので 0 にする
        Vector3 movement = new Vector3(input.x, 0, input.y) * speed;

        // Rigidbody に力（Force）を加えてボールを動かす
        // AddForce はリアルな物理的な動きになる（直接位置を変えるより自然）
        rb.AddForce(movement);
    }

    /// <summary>
    /// 他のコライダー（衝突判定）に触れたとき呼ばれるメソッド。
    /// ただし「トリガー」に設定されたコライダーに触れたときのみ発火する。
    /// </summary>
    /// <param name="other">触れた相手のコライダー</param>
    private void OnTriggerEnter(Collider other)
    {
        // 触れた相手のタグが "Item" かどうかを確認する
        if (other.CompareTag("Item"))
        {
            // アイテムのゲームオブジェクトをシーンから削除する（収集）
            Destroy(other.gameObject);
        }
    }
}
