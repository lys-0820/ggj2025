using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ServingController;
using TMPro;
using System.Collections;

public class TicketsController : MonoBehaviour
{
    public static TicketsController Instance { get; private set; }
    // �̶�����������
    private List<HashSet<Ingredient>> orders = new List<HashSet<Ingredient>>();

    // ��ǰ����������̲�
    private HashSet<Ingredient> playerIngredients = new HashSet<Ingredient>();

    // ��ǰ�ԱȵĶ�������
    private int currentOrderIndex = 0;

    [SerializeField] private List<GameObject> ticketImgs = new List<GameObject>();
    [SerializeField] private TMP_Text resultText;

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
    // �����ɹ�ʱ�Ļص�
    public void OnOrderCompleted(bool isSuccessful)
    {
        if (isSuccessful)
        {
            Debug.Log("Order completed successfully!");
            // ִ����سɹ����������罱�����
        }
        else
        {
            Debug.Log("Order failed. Try again!");
            // ִ��ʧ�ܲ���
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateFixedOrders();
        resultText.text = "";
    }
    // ���ɹ̶�����������
    private void GenerateFixedOrders()
    {
        // ��һ����������
        orders.Add(new HashSet<Ingredient> { Ingredient.BlackTea });
        // ��һ���������� + ��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.GreenTea });

        // �ڶ����������� + С��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.BlackTea, Ingredient.Bubble });

        // �������������� + С��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.GreenTea, Ingredient.Yuyuan });
    }

    // �������������̲��Ƿ���ϵ�ǰ����
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
                currentOrderIndex++; // ��һ������
                
            }
            else
            {
                OnOrderCompleted(false); // ��������϶���������ʧ��
                StartCoroutine(showResult("Wrong!"));
                ticketImgs[currentOrderIndex].gameObject.SetActive(false);
                currentOrderIndex++; // ��һ������
            }
        }
        else
        {
            Debug.Log("All orders have been completed.");
        }
    }

    // �ж϶����Ƿ����
    private bool IsOrderMatch(HashSet<Ingredient> order)
    {
        // �����ҵ�ʳ�ĺͶ�����ʳ����ȫƥ�䣬�������
        return playerIngredients.SetEquals(order);
    }

    // ��ҵ���ϲ�ʱ����

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
