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

    // 用于管理cup的sprite
    [SerializeField] private SpriteRenderer cupSpriteRenderer;

    // 不同单独食材的sprite
    public Sprite milkSprite;
    public Sprite greenTeaSprite;
    public Sprite blackTeaSprite;
    //public Sprite toppingSprite;

    // 组合食材的sprite
    public Sprite blackMilkTeaSprite;
    public Sprite greenMilkTeaSprite;
    //public Sprite milkAndToppingSprite;
    //public Sprite allIngredientsSprite;

    // 当前已添加的食材
    private HashSet<Ingredient> currentIngredients = new HashSet<Ingredient>();

    private bool makingStatus = false;
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
    // 更新cup的sprite
    public void UpdateCupSprite(Ingredient ingredient)
    {
        AddIngredient(ingredient);

        // 根据当前添加的食材组合选择sprite
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
            // 如果只有一个或没有食材，显示单独食材的sprite
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
        // 添加当前食材到已添加集合
        currentIngredients.Add(ingredient);
    }
    // Coroutine 方法，计时 30 秒后调用另一个方法
    private IEnumerator StartTimer()
    {
        // 等待30秒
        yield return new WaitForSeconds(5f);

        // 30秒后调用另外的方法
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
