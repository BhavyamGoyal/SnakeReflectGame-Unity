using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour, IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public float axis;

    private RectTransform wheelRT;
    private Vector2 centerPoint;

    public float maximumSteeringAngle = 120f;
    public float wheelReleasedSpeed = 350f;

    private float wheelAngle = 0f;
    private float wheelPrevAngle = 0f;

    private bool wheelBeingHeld = false;

    private float m_value;
    public float Value { get { return m_value; } }

    public float Angle { get { return wheelAngle; } }

    private void Awake()
    {
        wheelRT = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!wheelBeingHeld && wheelAngle != 0f)
        {
            float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= deltaAngle;
            else
                wheelAngle += deltaAngle;
        }
        wheelRT.localEulerAngles = new Vector3(0f, 0f, -wheelAngle);
    }

    public float GetWheelValue()
    {
        return wheelAngle / maximumSteeringAngle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        wheelBeingHeld = true;
        centerPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, wheelRT.position);
        wheelPrevAngle = Vector2.Angle(Vector2.up, eventData.position - centerPoint);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPos = eventData.position;
        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        if ((pointerPos - centerPoint).sqrMagnitude >= 400f)
        {
            if (pointerPos.x > centerPoint.x)
                wheelAngle += wheelNewAngle - wheelPrevAngle;
            else
                wheelAngle -= wheelNewAngle - wheelPrevAngle;
        }
        wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
        wheelPrevAngle = wheelNewAngle;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDrag(eventData);
        wheelBeingHeld = false;
    }
}