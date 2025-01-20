using UnityEngine;
using System.Collections;
using TMPro;
public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    [SerializeField] private TMP_Text timerText;          // UI中的计时器文本
    private void Awake()
    {
        // 实现单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startBoiling()
    {
        StartCoroutine(startTimer());
    }
    private void resetTimer()
    {
        timerText.text = "00:10";
    }
    private IEnumerator startTimer()
    {
        float totalBoilTime = 9f;  // 总煮茶时间为10秒
        float remainingTime = totalBoilTime;


        // 开始计时，并更新UI
        while (remainingTime > 0.01f)
        {
            timerText.text = "00:0" + Mathf.Ceil(remainingTime).ToString();  // 显示倒计时
            remainingTime -= Time.deltaTime;  // 每帧减少时间
            yield return null;  // 等待下一帧
        }
        resetTimer();

    }
}
