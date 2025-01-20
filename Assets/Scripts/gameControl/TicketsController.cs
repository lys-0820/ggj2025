using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ServingController;
using TMPro;
using System.Collections;

public class TicketsController : MonoBehaviour
{
    public static TicketsController Instance { get; private set; }
    // 固定的三个订单
    private List<HashSet<Ingredient>> orders = new List<HashSet<Ingredient>>();

    // 当前玩家制作的奶茶
    private HashSet<Ingredient> playerIngredients = new HashSet<Ingredient>();

    // 当前对比的订单索引
    private int currentOrderIndex = 0;

    [SerializeField] private List<GameObject> ticketImgs = new List<GameObject>();
    [SerializeField] private TMP_Text resultText;

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
    // 订单成功时的回调
    public void OnOrderCompleted(bool isSuccessful)
    {
        if (isSuccessful)
        {
            Debug.Log("Order completed successfully!");
            // 执行相关成功操作，比如奖励玩家
        }
        else
        {
            Debug.Log("Order failed. Try again!");
            // 执行失败操作
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateFixedOrders();
        resultText.text = "";
    }
    // 生成固定的三个订单
    private void GenerateFixedOrders()
    {
        // 第一个订单：茶
        orders.Add(new HashSet<Ingredient> { Ingredient.BlackTea });
        // 第一个订单：奶 + 茶
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.GreenTea });

        // 第二个订单：奶 + 小料
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.BlackTea, Ingredient.Bubble });

        // 第三个订单：茶 + 小料
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.GreenTea, Ingredient.Yuyuan });
    }

    // 检查玩家制作的奶茶是否符合当前订单
    public void CheckOrder()
    {
        if (orders.Count == 0)
        {
            Debug.Log("No orders to check.");
            return;
        }

        if (currentOrderIndex < orders.Count)
        {
            if (IsOrderMatch(orders[currentOrderIndex]))
            {
                OnOrderCompleted(true);
                StartCoroutine(showResult("Excellent!"));
                ticketImgs[currentOrderIndex].gameObject.SetActive(false);
                currentOrderIndex++; // 下一个订单
                
            }
            else
            {
                OnOrderCompleted(false); // 如果不符合订单，反馈失败
                StartCoroutine(showResult("Wrong!"));
                ticketImgs[currentOrderIndex].gameObject.SetActive(false);
                currentOrderIndex++; // 下一个订单
            }
        }
        else
        {
            Debug.Log("All orders have been completed.");
        }
    }

    // 判断订单是否符合
    private bool IsOrderMatch(HashSet<Ingredient> order)
    {
        // 如果玩家的食材和订单的食材完全匹配，订单完成
        return playerIngredients.SetEquals(order);
    }

    // 玩家点击上菜时调用

    public void SetPlayerRecipe(HashSet<Ingredient> playerSet)
    {
        playerIngredients = playerSet;
        CheckOrder();
    }
    private IEnumerator showResult(string result)
    {
        resultText.text = result;
        yield return new WaitForSeconds(2f);
        resultText.text = "";
    }

}
