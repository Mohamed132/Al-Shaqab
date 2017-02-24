using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PuzzlePiece : EventTrigger
{
    private Vector3 EndPosition;
    private float StartDistance;
    private float StartRotation;
    private RectTransform m_Transform;
    private Vector2 MinDragingLimits;
    private Vector2 MaxDragingLimits;
    private RectTransform SelectedPieceParent;
    private Transform NormalParent;
    private bool bSolved;

    public void RandomizeThePiece(Vector2 InPos, float CircleRed, Vector2 _MinDragingLimits, Vector2 _MaxDraginLimits, RectTransform _SelectedPieceParent)
    {
        NormalParent = transform.parent;
        SelectedPieceParent = _SelectedPieceParent;
        MinDragingLimits = _MinDragingLimits;
        MaxDragingLimits = _MaxDraginLimits;
        m_Transform = GetComponent<RectTransform>();
        EndPosition = m_Transform.position;
        m_Transform.position = InPos + Random.insideUnitCircle * CircleRed;
        StartDistance = Vector3.Distance(EndPosition, m_Transform.position);
        StartRotation = Random.value;
        Vector3 TempRotation = new Vector3(0, 0, StartRotation * 360);
        m_Transform.rotation = Quaternion.Euler(TempRotation);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (bSolved)
            return;
        base.OnDrag(eventData);
        Vector3 MousePos = Input.mousePosition;
        MousePos.x = Mathf.Clamp(MousePos.x, MinDragingLimits.x , MaxDragingLimits.x);
        MousePos.y = Mathf.Clamp(MousePos.y, MinDragingLimits.y, MaxDragingLimits.y);
        m_Transform.position = MousePos;
        float CurrentDistance = Vector3.Distance(EndPosition, m_Transform.position);
        if(CurrentDistance < 30)
        {
            m_Transform.position = EndPosition;
            m_Transform.rotation = Quaternion.identity;
            bSolved = true;
            PuzzleGenerator.singleton.OnPieceSolved();
            return;
        }
        float value = Mathf.InverseLerp(StartDistance, 0, CurrentDistance);
        Vector3 TempRotation = new Vector3(0, 0, Mathf.Lerp(StartRotation, 0, value) * 360);
        m_Transform.rotation = Quaternion.Euler(TempRotation);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        transform.parent = SelectedPieceParent;

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        transform.parent = NormalParent;
    }
}
