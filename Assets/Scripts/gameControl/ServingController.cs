using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static ServingController;
public class ServingController : MonoBehaviour
{
    public enum Ingredient
    {
        Milk,
        BlackTea,
        GreenTea,
        Bubble,
        Yuyuan,
        None
    }
    public static ServingController Instance { get; private set; }

    [SerializeField] private GameObject blackTeaPot;
    [SerializeField] private GameObject greenTeaPot;
    [SerializeField] private GameObject milk;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject yuyuan;

    // ���ڹ���cup��sprite
    [SerializeField] private SpriteRenderer cupSpriteRenderer;

    // ��ͬ����ʳ�ĵ�sprite
    public Sprite milkSprite;
    public Sprite greenTeaSprite;
    public Sprite blackTeaSprite;
    //public Sprite toppingSprite;

    // ���ʳ�ĵ�sprite
    public Sprite blackMilkTeaSprite;
    public Sprite greenMilkTeaSprite;
    //public Sprite milkAndToppingSprite;
    //public Sprite allIngredientsSprite;

    // ��ǰ����ӵ�ʳ��
    private HashSet<Ingredient> currentIngredients = new HashSet<Ingredient>();

    private bool makingStatus = false;
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
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMakingStatus(bool status)
    {
        makingStatus = status;
    }
    public bool IsMakingStatus()
    {
        return makingStatus;
    }
    // ����cup��sprite
    public void UpdateCupSprite(Ingredient ingredient)
    {
        AddIngredient(ingredient);

        // ���ݵ�ǰ��ӵ�ʳ�����ѡ��sprite
        if (currentIngredients.Contains(Ingredient.Milk) && currentIngredients.Contains(Ingredient.BlackTea))
        {
            cupSpriteRenderer.sprite = blackMilkTeaSprite;
        }
        else if (currentIngredients.Contains(Ingredient.Milk) && currentIngredients.Contains(Ingredient.GreenTea))
        {
            cupSpriteRenderer.sprite = greenMilkTeaSprite;
        }
        else
        {
            // ���ֻ��һ����û��ʳ�ģ���ʾ����ʳ�ĵ�sprite
            if (currentIngredients.Contains(Ingredient.Milk))
                cupSpriteRenderer.sprite = milkSprite;
            else if (currentIngredients.Contains(Ingredient.BlackTea))
                cupSpriteRenderer.sprite = blackTeaSprite;
            else if (currentIngredients.Contains(Ingredient.GreenTea))
                cupSpriteRenderer.sprite = greenTeaSprite;
        }
    }
    public void AddIngredient(Ingredient ingredient)
    {
        // ��ӵ�ǰʳ�ĵ�����Ӽ���
        currentIngredients.Add(ingredient);
    }
    // Coroutine ��������ʱ 30 ��������һ������
    private IEnumerator StartTimer()
    {
        // �ȴ�30��
        yield return new WaitForSeconds(5f);

        // 30����������ķ���
        changeMakingStatus(true);
    }

    public void checkIngredientCorrect()
    {
        foreach(var ingredient in currentIngredients)
        {
            Debug.Log(ingredient);
        }
    }
}
