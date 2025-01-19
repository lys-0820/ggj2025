using UnityEngine;
using static ServingController;

public class AddIngredient : MonoBehaviour
{
    public Ingredient ingredientType; // ��ǰԪ�ص�sprite
    private void OnMouseDown()
    {
        // ȷ��ͨ����������ServingController
        if (ServingController.Instance != null)
        {
            if (ServingController.Instance.IsMakingStatus())
            {
                ServingController.Instance.UpdateCupSprite(ingredientType);
            }
            
        }
    }
}
