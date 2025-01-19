using TMPro;
using UnityEngine;
using static ServingController;

public class AddToppings : MonoBehaviour
{
    public Ingredient ingredientType; // 当前元素的sprite
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject nowToppingPrefab;
    private GameObject nowTopping;
    private float moveSpeed = 2f;
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //nowToppings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == transform.gameObject)
            {
                // 确保通过单例访问ServingController
                if (ServingController.Instance != null)
                {
                    if (ServingController.Instance.IsMakingStatus())
                    {
                        ServingController.Instance.AddIngredient(ingredientType);
                        StartMoving();

                    }

                }

            }

        }
        if (isMoving)
        {
            MoveToppings();
        }

    }
    private void OnMouseDown()
    {

    }
    private void StartMoving()
    {
        isMoving = true;
        nowTopping = Instantiate(nowToppingPrefab);
        nowTopping.SetActive(true);
        Vector3 cupPosition = cup.transform.position;
        nowTopping.transform.position = new Vector3(cupPosition.x, cupPosition.y + 2f, cupPosition.z);
    }
    private void MoveToppings()
    {
        if (cup.transform.position.y - nowTopping.transform.position.y < 0.5f)
        {
            nowTopping.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }
}
