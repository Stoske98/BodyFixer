using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class UIRotate : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform joint;
    [SerializeField]
    private Transform target;
    Vector3 startPosition;

    [SerializeField]
    private Vector3 desiredJointRotation;
    [SerializeField]
    private float bestPerformance;
    [SerializeField]
    private float worstPerformance;
    public void Start()
    {
        Invoke("SetupPosition", 0.1f);
    }
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position
            );

        transform.position = canvas.transform.TransformPoint(position);

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, Camera.main.WorldToScreenPoint(target.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
        target.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

        //Debug.Log((desiredJointRotation - target.position).magnitude);
        Manager.GameManager.Instance.CheckPerformance();

    }
    public float getPerformance()
    {
        if ((desiredJointRotation - target.position).magnitude < bestPerformance)
            return 1f;
        else if ((desiredJointRotation - target.position).magnitude < worstPerformance)
            return 0.5f;
        else return 0f;
    }

    public void DropHandler(BaseEventData data)
    {
        ResetPositionOnJoint();
    }
    public void SetupPosition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(joint.position);
        GetComponent<RectTransform>().position = screenPos;
        startPosition = screenPos;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, Camera.main.WorldToScreenPoint(target.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
        target.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
    }
    public void ResetPositionOnJoint()
     {
        GetComponent<RectTransform>().position = startPosition;
     }
}
