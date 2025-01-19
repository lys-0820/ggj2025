using UnityEngine;
using System.Collections;
public class HotWater : MonoBehaviour
{
    [SerializeField] private GameObject tap;
    [SerializeField] private GameObject water;
    private Animator waterAnimator;
    private float waterTimer = 0f;
    private bool isWaterFlowing = false;

    void Start()
    {
        //初始化时关闭水流
        water.SetActive(false);
        //获取水流的动画组件
        waterAnimator = water.GetComponent<Animator>();
    }

    void Update()
    {

        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            Debug.Log(hit.collider.name);
            if (hit.collider != null && hit.collider.gameObject == tap)
            {
                StartWaterFlow();
                BoilTea.Instance.AddHotWater();
            }

        }

        // 如果水在流动，计时
        if (isWaterFlowing)
        {
            waterTimer += Time.deltaTime;
            if (waterTimer >= 3f)
            {
                StopWaterFlow();
            }
        }
    }

    private void StartWaterFlow()
    {
        water.SetActive(true);
        isWaterFlowing = true;
        waterTimer = 0f;
        if (waterAnimator != null)
        {
            waterAnimator.SetTrigger("Play");
            
        }
    }

    private void StopWaterFlow()
    {
        water.SetActive(false);
        isWaterFlowing = false;
        waterTimer = 0f;
    }
}