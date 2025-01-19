using UnityEngine;
using static ServingController;

public class AddIngredient : MonoBehaviour
{
    public Ingredient ingredientType; // 当前元素的sprite
    private void OnMouseDown()
    {
        // 确保通过单例访问ServingController
        if (ServingController.Instance != null)
        {
            if (ServingController.Instance.IsMakingStatus())
            {
                ServingController.Instance.UpdateCupSprite(ingredientType);
            }
            
        }
    }
}
