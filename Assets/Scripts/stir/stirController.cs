using UnityEngine;
using System.Collections;
public class stirController : MonoBehaviour
{
    public static stirController Instance { get; private set; }
    [SerializeField] private SpriteRenderer bubbleImage; // 茶汤的图片组件
    [SerializeField] private Sprite emptySprite; // 空杯子的图片
    [SerializeField] private Sprite bubbleSprite; // 只有珍珠的图片
    [SerializeField] private Sprite hotWaterSprite; // 只有热水的图片
    [SerializeField] private Sprite finalBubbleSprite; // 完成的珍珠的图片
    [SerializeField] private GameObject stirStick;

    private bool hasBubble = false;
    private bool hasHotWater = false;
    private bool hasStir = false;
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
        // 确保有图片组件
        if (bubbleImage == null)
        {
            Debug.LogError("Tea Image component is not assigned!");
        }
        stirStick.SetActive(false);
        StartCoroutine(UpdateBubbleSprite());
    }

    public void AddBubble()
    {
        hasBubble = true;
        StartCoroutine(UpdateBubbleSprite());
    }

    public void AddHotWater()
    {
        hasHotWater = true;
        StartCoroutine(UpdateBubbleSprite());
    }
    public void StirBubble()
    {
        hasStir = true;
        StartCoroutine(UpdateBubbleSprite());
    }
    public void Reset()
    {
        hasBubble = false;
        hasHotWater = false;
        hasStir = false;
        stirStick.SetActive(false);
        StartCoroutine(UpdateBubbleSprite());
    }
    private IEnumerator UpdateBubbleSprite()
    {
        yield return new WaitForSeconds(3.0f);
        if (!hasBubble && !hasHotWater)
        {
            bubbleImage.sprite = emptySprite;
        }
        else if (hasBubble && !hasHotWater)
        {
            bubbleImage.sprite = bubbleSprite;
        }
        else if (!hasBubble && hasHotWater)
        {
            bubbleImage.sprite = hotWaterSprite;
        }
        else if (hasBubble && hasHotWater)
        {
            stirStick.SetActive(true);
        }

        if (hasStir)
        {
            
            bubbleImage.sprite = finalBubbleSprite;
            Reset();
        }
        
    }

    // 获取当前状态的方法
    public bool HasBubble() => hasBubble;
    public bool HasHotWater() => hasHotWater;
}
