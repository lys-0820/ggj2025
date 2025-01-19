using UnityEngine;

public class bubble : MonoBehaviour
{
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject bubblePot;

    private bool isMoving = false;
    private float moveSpeed = 2f;
    private Vector3 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbles.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == bubbles)
            {
                StartBubbleMovement();
                stirController.Instance.AddBubble();

            }
        }

        if (isMoving)
        {
            MoveBubbles();
        }
    }
    private void StartBubbleMovement()
    {
        isMoving = true;
        Vector3 bubblePotPosition = bubblePot.transform.position;
        targetPosition = new Vector3(bubblePotPosition.x, bubblePotPosition.y +2.0f, bubblePotPosition.z);
        bubbles.transform.position = targetPosition;
    }

    private void MoveBubbles()
    {
        bubbles.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(bubbles.transform.position, bubblePot.transform.position) < 0.5f)
        {
            isMoving = false;
            bubbles.SetActive(false);
        }
    }
}
