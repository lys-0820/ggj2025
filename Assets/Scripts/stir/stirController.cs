using UnityEngine;
using System.Collections;
public class stirController : MonoBehaviour
{
    public static stirController Instance { get; private set; }
    [SerializeField] private SpriteRenderer bubbleImage; // ������ͼƬ���
    [SerializeField] private Sprite emptySprite; // �ձ��ӵ�ͼƬ
    [SerializeField] private Sprite bubbleSprite; // ֻ�������ͼƬ
    [SerializeField] private Sprite hotWaterSprite; // ֻ����ˮ��ͼƬ
    [SerializeField] private Sprite hotWaterBubbleSprite; // ��ˮ������δ�����ͼƬ
    [SerializeField] private Sprite finalBubbleSprite; // ��ɵ������ͼƬ
    [SerializeField] private GameObject stirStick;

    private bool hasBubble = false;
    private bool hasHotWater = false;
    private bool hasStir = false;
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
        // ȷ����ͼƬ���
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
        PreparationController.Instance.StartBubblePreparation();
    }

    public void AddHotWater()
    {
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
        PreparationController.Instance.CompleteBubblePreparation();
        PreparationController.Instance.ResetHotWater();
    }
    private IEnumerator UpdateBubbleSprite()
    {
        hasHotWater = PreparationController.Instance.HasHotWater();
        Debug.Log("hot water:" + hasHotWater);
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
            bubbleImage.sprite = hotWaterBubbleSprite;
            stirStick.SetActive(true);
        }

        if (hasStir)
        {
            
            bubbleImage.sprite = finalBubbleSprite;
            Reset();
            
        }
        
    }

    // ��ȡ��ǰ״̬�ķ���
    public bool HasBubble() => hasBubble;
    public bool HasHotWater() => hasHotWater;


}
