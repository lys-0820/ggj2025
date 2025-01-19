using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoilTea : MonoBehaviour
{
    public static BoilTea Instance { get; private set; }

    [SerializeField] private SpriteRenderer teaImage; // 茶汤的图片组件
    [SerializeField] private Sprite emptySprite; // 空杯子的图片
    [SerializeField] private Sprite teaLeafSprite; // 只有茶叶的图片
    [SerializeField] private Sprite hotWaterSprite; // 只有热水的图片
    [SerializeField] private Sprite finalTeaSprite; // 完成的茶的图片

    private bool hasTeaLeaf = false;
    private bool hasHotWater = false;

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
       StartCoroutine(UpdateTeaSprite());
    }

    public void AddTeaLeaf()
    {
        hasTeaLeaf = true;
       StartCoroutine(UpdateTeaSprite());
    }

    public void AddHotWater()
    {
        hasHotWater = true;
       StartCoroutine(UpdateTeaSprite());
    }

    public void Reset()
    {
        hasTeaLeaf = false;
        hasHotWater = false;
        StartCoroutine(UpdateTeaSprite());
    }
    private IEnumerator UpdateTeaSprite()
    {
        yield return new WaitForSeconds(2.0f);
        if (!hasTeaLeaf && !hasHotWater)
        {
            teaImage.sprite = emptySprite;
        }
        else if (hasTeaLeaf && !hasHotWater)
        {
            teaImage.sprite = teaLeafSprite;
        }
        else if (!hasTeaLeaf && hasHotWater)
        {
            teaImage.sprite = hotWaterSprite;
        }
        else if (hasTeaLeaf && hasHotWater)
        {
            teaImage.sprite = finalTeaSprite;
        }
    }

    // 获取当前状态的方法
    public bool HasTeaLeaf() => hasTeaLeaf;
    public bool HasHotWater() => hasHotWater;
}