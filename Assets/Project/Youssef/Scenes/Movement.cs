using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f; // سرعة الحركة
    public float distance = 10f; // المسافة التي تريد تحريك العربية إليها
    public float turnDuration = 1f; // مدة التدوير
    public float delayBetweenLoops = 2f; // تأخير بين الحركات المتكررة

    private Vector3 startPosition;
    private bool movingForward = true; // لتحديد اتجاه الحركة
    private bool isTurning = false; // لتحديد إذا كانت العربية تدور

    void Start()
    {
        startPosition = transform.position;
        StartMovement();
    }

    void Update()
    {
        if (movingForward)
        {
            MoveForward();
        }
        else if (isTurning)
        {
            TurnAround();
        }
        else
        {
            MoveBackward();
        }
    }

    void MoveForward()
    {
        float distanceMoved = Vector3.Distance(startPosition, transform.position);

        if (distanceMoved < distance)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            movingForward = false;
            isTurning = true;
        }
    }

    void TurnAround()
    {
        float turnSpeed = 180f / turnDuration; // سرعة التدوير
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        if (transform.rotation.eulerAngles.y >= 180f) // التدوير الكامل
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // تأكيد التدوير عند 180 درجة
            isTurning = false;
            Invoke("StartMovement", delayBetweenLoops); // تأخير قبل بدء الحركة للخلف
        }
    }

    void MoveBackward()
    {
        float distanceMoved = Vector3.Distance(startPosition, transform.position);

        if (distanceMoved > 0)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else
        {
            movingForward = true;
            startPosition = transform.position; // تحديث الموضع الحالي كموضع البداية الجديد
            Invoke("StartMovement", delayBetweenLoops); // تأخير قبل بدء الحركة للأمام مرة أخرى
        }
    }

    void StartMovement()
    {
        if (movingForward)
        {
            startPosition = transform.position; // تحديث الموضع الحالي كموضع البداية الجديد
        }
        else
        {
            movingForward = true; // استئناف الحركة للأمام
        }
    }
}
