using UnityEngine;

public class ServingController : MonoBehaviour
{

    public static ServingController Instance { get; private set; }

    [SerializeField] private GameObject blackTeaPot;
    [SerializeField] private GameObject greenTeaPot;
    [SerializeField] private GameObject milk;
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject yuyuan;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
