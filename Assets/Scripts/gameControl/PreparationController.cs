using UnityEngine;

public class PreparationController : MonoBehaviour
{
    // 单例实例
    public static PreparationController Instance { get; private set; }

    private PreparationStep currentStep;
    private bool isPreparationInProgress = false;
    public bool hasHotWater = false;

    // Awake 方法，确保单例实例
    private void Awake()
    {
        // 如果实例已经存在，则销毁当前对象
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 可选：防止销毁
        }
    }

    // 抽象类：定义共同的操作步骤
    public abstract class PreparationStep : MonoBehaviour
    {
        // 虚方法，允许子类重写
        public virtual void StartPreparation()
        {
            Debug.Log("开始准备");
        }

        public virtual void UpdatePreparation()
        {
            Debug.Log("准备进行中...");
        }

        public virtual bool IsPreparationComplete()
        {
            // 默认返回false，可以被重写
            return false;
        }
        public virtual void CompletePreparation()
        {

            Debug.Log("准备完成");
        }
    }
    // 启动茶叶备菜
    public void StartTeaPreparation()
    {
        if (isPreparationInProgress) return;

        currentStep = new TeaPreparation(); // 创建茶叶备菜实例
        currentStep.StartPreparation();
        isPreparationInProgress = true;
    }
    public void CompleteTeaPreparation()
    {

        currentStep.CompletePreparation();

        UpdateStatus();
        isPreparationInProgress = false;
        Debug.Log("茶叶准备完成");
    }

    // 启动珍珠备菜
    public void StartBubblePreparation()
    {
        if (isPreparationInProgress) return;

        currentStep = new BubblePreparation(); // 创建珍珠备菜实例
        currentStep.StartPreparation();
        isPreparationInProgress = true;
    }
    public void CompleteBubblePreparation()
    {

        currentStep.CompletePreparation();
        
        UpdateStatus();
        isPreparationInProgress = false;
        Debug.Log("珍珠准备完成");
    }

    // 具体步骤：煮茶叶
    private class TeaPreparation : PreparationStep
    {
        public override void StartPreparation()
        {
            Debug.Log("开始煮茶叶");
            // 启动煮茶叶的操作
        }

        public override void UpdatePreparation()
        {
            // 更新煮茶叶的过程
            Debug.Log("煮茶叶进行中...");
        }

        public override bool IsPreparationComplete()
        {
            // 判断煮茶叶是否完成
            return true;
        }
    }

    // 具体步骤：煮珍珠
    private class BubblePreparation : PreparationStep
    {
        public override void StartPreparation()
        {
            Debug.Log("开始煮珍珠");
            // 启动煮珍珠的操作
        }

        public override void UpdatePreparation()
        {
            // 更新煮珍珠的过程
            Debug.Log("煮珍珠进行中...");
        }

        public override bool IsPreparationComplete()
        {
            // 判断煮珍珠是否完成
            return true;
        }
    }
    // 更新备菜进度
    public void UpdateStatus()
    {
        if (isPreparationInProgress && currentStep != null)
        {
            currentStep.UpdatePreparation();

            if (currentStep.IsPreparationComplete())
            {
                Debug.Log("备菜完成");
                isPreparationInProgress = false;
                // 你可以在此处开始下一个阶段的操作，例如上菜
            }
        }
    }
    public void AddHotWater()
    {
        hasHotWater = true;
    }
    public bool HasHotWater()
    {
        return hasHotWater;
    }
    
    public void ResetHotWater()
    {
        hasHotWater = false; 
    }
}
