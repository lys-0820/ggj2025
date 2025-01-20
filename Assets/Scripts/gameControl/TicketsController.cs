using System.Collections.Generic;
using UnityEngine;
using static ServingController;

public class TicketsController : MonoBehaviour
{
    public static TicketsController Instance { get; private set; }
    // �̶�����������
    private List<HashSet<Ingredient>> orders = new List<HashSet<Ingredient>>();

    // ��ǰ����������̲�
    private HashSet<Ingredient> playerIngredients = new HashSet<Ingredient>();

    // ��ǰ�ԱȵĶ�������
    private int currentOrderIndex = 0;

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
    }
    // ���ɹ̶�����������
    private void GenerateFixedOrders()
    {
        // ��һ���������� + ��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.BlackTea });

        // �ڶ����������� + С��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.GreenTea, Ingredient.Bubble });

        // �������������� + С��
        orders.Add(new HashSet<Ingredient> { Ingredient.Milk, Ingredient.BlackTea, Ingredient.Yuyuan });
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
                currentOrderIndex++; // ��һ������
            }
            else
            {
                OnOrderCompleted(false); // ��������϶���������ʧ��
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

}
