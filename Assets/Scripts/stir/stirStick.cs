using TMPro;
using UnityEngine;

public class stirStick : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    private bool isMoving = false;

    private float rotationRadius = 0.5f;  // 旋转半径
    private float rotationSpeed = 180f;   // 旋转速度（度/秒）
    private Vector3 centerPosition;        // 旋转中心点
    private float currentAngle = 0f;      // 当前角度
    private float stirDuration = 2f;      // 搅拌持续时间
    private float stirTimer = 0f;         // 搅拌计时器
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == stick)
            {
                StartStickMovement();
                stirController.Instance.StirBubble();

            }
        }
        if (isMoving)
        {
            MoveStick();
        }
    }
    private void StartStickMovement()
    {
        isMoving = true;
        stirTimer = 0f;
        currentAngle = 0f;
        // 记录开始旋转时的位置作为中心点
        centerPosition = stick.transform.position;
    }

    private void MoveStick()
    {
        stirTimer += Time.deltaTime;
        if (stirTimer >= stirDuration)
        {
            isMoving = false;
            return;
        }

        // 计算新的角度
        currentAngle += rotationSpeed * Time.deltaTime;
        
        // 计算新的位置
        float x = centerPosition.x + rotationRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = centerPosition.y + rotationRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        
        // 只更新搅拌棒位置，不改变旋转
        stick.transform.position = new Vector3(x, y, centerPosition.z);
    }
}

