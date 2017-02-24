using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : EventTrigger {
    private Vector3 EndPosition;
    private float StartDistance;
    private float StartRotation;
    private RectTransform m_Transform;
    // Use this for initialization

    private void Awake()
    {
        m_Transform = GetComponent<RectTransform>();
        EndPosition = transform.position;
    }

    public void SetRandomPositionAndRotation(Vector2 InPos, float CircleRed)
    {
         m_Transform.position = InPos + Random.insideUnitCircle * CircleRed;
        StartDistance = Vector3.Distance(EndPosition, m_Transform.position);
        StartRotation = Random.value;
        Vector3 TempRotation = new Vector3(0, 0, StartRotation * 360);
        m_Transform.rotation = Quaternion.Euler(TempRotation);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        m_Transform.position = Input.mousePosition;
        float CurrentDistance = Vector3.Distance(EndPosition, m_Transform.position);
        float value  = Mathf.InverseLerp(StartDistance, 0, CurrentDistance);
        value = ;
        Vector3 TempRotation = new Vector3(0, 0, Mathf.Lerp(StartRotation, 0, value) * 360);
        m_Transform.rotation = Quaternion.Euler(TempRotation);
    }
}
