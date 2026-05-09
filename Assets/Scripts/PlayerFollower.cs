using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    // 追従するプレイヤーの Transform（インスペクターでプレイヤーを割り当てる）
    // Transform とは、ゲームオブジェクトの位置・回転・拡縮を管理するコンポーネント
    [SerializeField] private Transform playerTransform;

    // カメラとプレイヤーの初期位置の差（オフセット）を保存する変数
    private Vector3 offset;

    /// <summary>
    /// 最初に一度だけ呼ばれるメソッド。
    /// カメラとプレイヤーの初期位置の差を記録しておく。
    /// </summary>
    void Start()
    {
        // カメラの位置 - プレイヤーの位置 = 差分（オフセット）を計算して保存する
        offset = transform.position - playerTransform.position;
    }

    /// <summary>
    /// 毎フレーム、他のすべての Update が終わった後に呼ばれるメソッド。
    /// LateUpdate を使うことで、プレイヤーが移動し終えてからカメラを追従できる。
    /// </summary>
    void LateUpdate()
    {
        // プレイヤーの現在位置 + 初期オフセット = カメラが移動すべき位置
        transform.position = playerTransform.position + offset;
    }
}
