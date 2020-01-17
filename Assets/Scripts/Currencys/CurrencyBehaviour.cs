using UnityEngine;

public class CurrencyBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public float rotateSpeed;
    public float delayTime;

    [Header("Behaviour")]
    float magneticSpeed;
    float currentTime;
    [HideInInspector] public int value;
    [HideInInspector] public bool magnetic;
    [HideInInspector] public GameObject targetToMove;


    void Start()
    {
        Destroy(this.gameObject, 10f);
    }


    void Update()
    {
        Behaviour();
    }


    public void Behaviour()
    {
        Movement();

        if (currentTime > delayTime)
        {
            if (magnetic && targetToMove != null)
                Attraction();
        }
        else
            currentTime += Time.deltaTime * GameManager.Instance.gameTime; ;
    }


    public void SetValue(int newValue)
    {
        value = newValue;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ship"))
        {
            CurrencyManager.Instance.AddCurrency(this.value);
            Destroy(this.gameObject);
        }
    }


    public void Movement()
    {
        this.transform.Rotate(0, rotateSpeed * Time.deltaTime * GameManager.Instance.gameTime, 0);
    }


    void Attraction()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetToMove.transform.position,
            magneticSpeed * Time.deltaTime * GameManager.Instance.gameTime
        );
    }


    public void SetAttractionComponent(GameObject newTarget, float newMagneticSpeed)
    {
        magnetic = true;
        targetToMove = newTarget;
        magneticSpeed = newMagneticSpeed;
    }
}
