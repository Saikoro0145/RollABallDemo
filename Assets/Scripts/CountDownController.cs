using TMPro;
using System.Collections;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
    // インスペクターで設定できるカウントダウンの開始秒数
    [SerializeField] private int CountDownTime = 3;
    // カウントダウンの数字を表示するテキストUI
    [SerializeField] private TMP_Text CountDownText;
    // 現在のカウントダウンの残り秒数
    private int currentCountDownTime = 0;

    /// <summary>
    /// カウントダウンを開始するコルーチン。
    /// コルーチンとは，処理の途中で一時停止し，後のフレームで再開できる処理のこと。
    /// yield return を使うことで，他の処理を止めずに，指定した条件や時間まで待機できる。
    /// </summary>
    public IEnumerator StartCountDown()
    {
        // カウントダウンをインスペクターで設定した値にリセットする
        currentCountDownTime = CountDownTime;

        // カウントダウンが 0 より大きい間、繰り返す
        while (currentCountDownTime > 0)
        {
            // テキストUIに現在の残り秒数を表示する（int → string に変換）
            CountDownText.text = currentCountDownTime.ToString();
            // 1秒間待機する（コルーチンなので他の処理はブロックしない）
            yield return new WaitForSeconds(1f);
            // 1秒経ったので残り時間を1減らす
            currentCountDownTime--;
        }

        // カウントが0になったら「Go!」と表示
        CountDownText.text = "Go";

        // 1秒待ってからカウントダウンテキストを非表示にする
        yield return new WaitForSeconds(1f);
        CountDownText.gameObject.SetActive(false);
    }
}
