using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeaLeaf : MonoBehaviour
{
    [SerializeField] private GameObject teaLeaf;
    [SerializeField] private GameObject teaPot;
    [SerializeField] private Sprite boiledTeaPotSprite;
    
    private bool isMoving = false;
    private float moveSpeed = 2f;
    private Vector3 targetPosition;

    void Start()
    {
        teaLeaf.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == teaLeaf)
            {
                StartTeaLeafMovement();
                BoilTea.Instance.AddTeaLeaf();
            }
        }

        if (isMoving)
        {
            MoveTeaLeaf();
        }
    }

    private void StartTeaLeafMovement()
    {
        isMoving = true;
        Vector3 teaPotPosition = teaPot.transform.position;
        targetPosition = new Vector3(teaPotPosition.x, teaPotPosition.y + 2f, teaPotPosition.z);
        teaLeaf.transform.position = targetPosition;
    }

    private void MoveTeaLeaf()
    {
        teaLeaf.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(teaLeaf.transform.position, teaPot.transform.position) < 0.5f)
        {
            isMoving = false;
            teaLeaf.SetActive(false);
        }
    }


}
