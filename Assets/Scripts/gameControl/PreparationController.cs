using UnityEngine;

public class PreparationController : MonoBehaviour
{
    // ����ʵ��
    public static PreparationController Instance { get; private set; }

    private PreparationStep currentStep;
    private bool isPreparationInProgress = false;
    public bool hasHotWater = false;

    // Awake ������ȷ������ʵ��
    private void Awake()
    {
        // ���ʵ���Ѿ����ڣ������ٵ�ǰ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��ѡ����ֹ����
        }
    }

    // �����ࣺ���干ͬ�Ĳ�������
    public abstract class PreparationStep : MonoBehaviour
    {
        // �鷽��������������д
        public virtual void StartPreparation()
        {
            Debug.Log("��ʼ׼��");
        }

        public virtual void UpdatePreparation()
        {
            Debug.Log("׼��������...");
        }

        public virtual bool IsPreparationComplete()
        {
            // Ĭ�Ϸ���false�����Ա���д
            return false;
        }
        public virtual void CompletePreparation()
        {

            Debug.Log("׼�����");
        }
    }
    // ������Ҷ����
    public void StartTeaPreparation()
    {
        if (isPreparationInProgress) return;

        currentStep = new TeaPreparation(); // ������Ҷ����ʵ��
        currentStep.StartPreparation();
        isPreparationInProgress = true;
    }
    public void CompleteTeaPreparation()
    {

        currentStep.CompletePreparation();

        UpdateStatus();
        isPreparationInProgress = false;
        Debug.Log("��Ҷ׼�����");
    }

    // �������鱸��
    public void StartBubblePreparation()
    {
        if (isPreparationInProgress) return;

        currentStep = new BubblePreparation(); // �������鱸��ʵ��
        currentStep.StartPreparation();
        isPreparationInProgress = true;
    }
    public void CompleteBubblePreparation()
    {

        currentStep.CompletePreparation();
        
        UpdateStatus();
        isPreparationInProgress = false;
        Debug.Log("����׼�����");
    }

    // ���岽�裺���Ҷ
    private class TeaPreparation : PreparationStep
    {
        public override void StartPreparation()
        {
            Debug.Log("��ʼ���Ҷ");
            // �������Ҷ�Ĳ���
        }

        public override void UpdatePreparation()
        {
            // �������Ҷ�Ĺ���
            Debug.Log("���Ҷ������...");
        }

        public override bool IsPreparationComplete()
        {
            // �ж����Ҷ�Ƿ����
            return true;
        }
    }

    // ���岽�裺������
    private class BubblePreparation : PreparationStep
    {
        public override void StartPreparation()
        {
            Debug.Log("��ʼ������");
            // ����������Ĳ���
        }

        public override void UpdatePreparation()
        {
            // ����������Ĺ���
            Debug.Log("�����������...");
        }

        public override bool IsPreparationComplete()
        {
            // �ж��������Ƿ����
            return true;
        }
    }
    // ���±��˽���
    public void UpdateStatus()
    {
        if (isPreparationInProgress && currentStep != null)
        {
            currentStep.UpdatePreparation();

            if (currentStep.IsPreparationComplete())
            {
                Debug.Log("�������");
                isPreparationInProgress = false;
                // ������ڴ˴���ʼ��һ���׶εĲ����������ϲ�
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
