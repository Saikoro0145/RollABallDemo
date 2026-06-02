using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面を管理するクラス。
/// スタートボタンが押されたらゲームシーン（InGame）に切り替える。
/// </summary>
public class TitleManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // SceneManager.LoadScene でシーン名を指定してシーンを切り替える
        SceneManager.LoadScene("InGame");
    }
}
