using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public enum teaType
{
    black,
    green,
    none
}
public class BoilTeaController : MonoBehaviour
{

    public static BoilTeaController Instance { get; private set; }

    [SerializeField] private SpriteRenderer teaImage; // 茶汤的图片组件
    [SerializeField] private Sprite emptySprite; // 空杯子的图片
    [SerializeField] private Sprite blackTeaLeafSprite; // 只有茶叶的图片
    [SerializeField] private Sprite greenTeaLeafSprite; // 只有茶叶的图片
    [SerializeField] private Sprite hotWaterSprite; // 只有热水的图片
    [SerializeField] private Sprite blackWaterSprite; // 茶叶+水的图片
    [SerializeField] private Sprite greenWaterSprite; // 茶叶+水的图片
    [SerializeField] private Sprite finalBlackTeaSprite; // 完成的茶的图片
    [SerializeField] private Sprite finalGreenTeaSprite; // 完成的茶的图片

    [SerializeField] private SpriteRenderer blackTeaPot;
    [SerializeField] private SpriteRenderer greenTeaPot;
    [SerializeField] private Sprite emptyBlackPot;
    [SerializeField] private Sprite filledBlackPot;
    [SerializeField] private Sprite emptyGreenPot;
    [SerializeField] private Sprite filledGreenPot;

    private bool hasTeaLeaf = false;
    private bool hasHotWater = false;
    private teaType type = teaType.none;

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

    private void Start()
    {
        // 确保有图片组件
        if (teaImage == null)
        {
            Debug.LogError("Tea Image component is not assigned!");
        }
       StartCoroutine(UpdateTeaSprite(type));
    }

    public void AddTeaLeaf(teaType teaType)
    {
        hasTeaLeaf = true;
        type = teaType;
        Debug.Log(type);
        StartCoroutine(UpdateTeaSprite(type));
        PreparationController.Instance.StartTeaPreparation();
    }

    public void AddHotWater()
    {
        
        StartCoroutine(UpdateTeaSprite(type));
    }

    public void Reset()
    {
        hasTeaLeaf = false;
        hasHotWater = false;
        StartCoroutine(UpdateTeaSprite(type));
        PreparationController.Instance.CompleteTeaPreparation();
        PreparationController.Instance.ResetHotWater();

        // 清空计时器文本
        timerText.text = "0s";
    }
    private IEnumerator UpdateTeaSprite(teaType type)
    {
        hasHotWater = PreparationController.Instance.HasHotWater();
        Debug.Log("hot water:" + hasHotWater);
        yield return new WaitForSeconds(2.0f);
        if (!hasTeaLeaf && !hasHotWater)
        {
            teaImage.sprite = emptySprite;
        }
        else if (hasTeaLeaf && !hasHotWater)
        {
            if(type == teaType.black)
            {
                teaImage.sprite = blackTeaLeafSprite;
            }
            else
            {
                teaImage.sprite = greenTeaLeafSprite;
            }
            
        }
        else if (!hasTeaLeaf && hasHotWater)
        {
            teaImage.sprite = hotWaterSprite;
        }
        else if (hasTeaLeaf && hasHotWater)
        {

            StartCoroutine(startBoiling(type));
        }
    }

    // 获取当前状态的方法
    public bool HasTeaLeaf() => hasTeaLeaf;
    public bool HasHotWater() => hasHotWater;

    private IEnumerator startBoiling(teaType type)
    {
        float totalBoilTime = 10f;  // 总煮茶时间为10秒
        float remainingTime = totalBoilTime;


        // 开始计时，并更新UI
        while (remainingTime > 0)
        {
            timerText.text = Mathf.Ceil(remainingTime).ToString() + "s";  // 显示倒计时
            remainingTime -= Time.deltaTime;  // 每帧减少时间
            yield return null;  // 等待下一帧
        }
        if (type == teaType.black)
        {
            teaImage.sprite = blackWaterSprite;
        }
        else
        {
            teaImage.sprite = greenWaterSprite;
        }
        yield return new WaitForSeconds(8f);
        if (type == teaType.black)
        {
            teaImage.sprite = finalBlackTeaSprite;
            blackTeaPot.sprite = filledBlackPot;
        }
        else
        {
            teaImage.sprite = finalGreenTeaSprite;
            greenTeaPot.sprite = filledGreenPot;
        }
        yield return new WaitForSeconds(2f);
        Reset();
    }
}