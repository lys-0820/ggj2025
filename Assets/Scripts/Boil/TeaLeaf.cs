using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeaLeaf : MonoBehaviour
{
    [SerializeField] private GameObject blackTeaLeaf;
    [SerializeField] private GameObject greenTeaLeaf;
    [SerializeField] private GameObject teaPot;
    
    private bool isMoving = false;
    private float moveSpeed = 2f;
    private Vector3 targetPosition;

    [SerializeField] private GameObject greenTeaPrefab;
    [SerializeField] private GameObject blackTeaPrefab;
    private GameObject nowTeaLeaf;


    void Start()
    {
        //blackTeaLeaf.AddComponent<BoxCollider2D>();
        //greenTeaLeaf.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && (hit.collider.gameObject == blackTeaLeaf||hit.collider.gameObject == greenTeaLeaf))
            {
                if(hit.collider.gameObject == blackTeaLeaf)
                {
                    nowTeaLeaf = Instantiate(blackTeaPrefab);
                    BoilTeaController.Instance.AddTeaLeaf(teaType.black);
                }
                else
                {
                    nowTeaLeaf = Instantiate(greenTeaPrefab);
                    BoilTeaController.Instance.AddTeaLeaf(teaType.green);
                }
                
                StartTeaLeafMovement(nowTeaLeaf);
                
            }
        }

        if (isMoving&& nowTeaLeaf)
        {
            MoveTeaLeaf(nowTeaLeaf);
        }
    }

    private void StartTeaLeafMovement(GameObject teaLeaf)
    {
        isMoving = true;
        Vector3 teaPotPosition = teaPot.transform.position;
        targetPosition = new Vector3(teaPotPosition.x, teaPotPosition.y + 2f, teaPotPosition.z);
        teaLeaf.transform.position = targetPosition;
    }

    private void MoveTeaLeaf(GameObject teaLeaf)
    {
        teaLeaf.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(teaLeaf.transform.position, teaPot.transform.position) < 0.5f)
        {
            isMoving = false;
            Destroy(teaLeaf);
        }
    }


}
