using UnityEngine;
using System.Collections;
using TMPro;
public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    [SerializeField] private TMP_Text timerText;          // UI�еļ�ʱ���ı�
    private void Awake()
    {
        // ʵ�ֵ���ģʽ
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
        float totalBoilTime = 9f;  // �����ʱ��Ϊ10��
        float remainingTime = totalBoilTime;


        // ��ʼ��ʱ��������UI
        while (remainingTime > 0.01f)
        {
            timerText.text = "00:0" + Mathf.Ceil(remainingTime).ToString();  // ��ʾ����ʱ
            remainingTime -= Time.deltaTime;  // ÿ֡����ʱ��
            yield return null;  // �ȴ���һ֡
        }
        resetTimer();

    }
}
