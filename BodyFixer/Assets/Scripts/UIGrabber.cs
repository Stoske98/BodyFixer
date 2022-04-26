using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Animations.Rigging;

public class UIGrabber : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform joint;
    MultiPositionConstraint targetPosition;
    [SerializeField]
    private Vector3 desiredJointPosition;
    [SerializeField]
    private float bestPerformance;
    [SerializeField]
    private float worstPerformance;
    public float maxRange = 1.75f;
    public float minRange = 0.35f;

    public bool fixY;
    public bool fixX;
    public bool fixZ;

    public void Start()
    {
        targetPosition = joint.transform.parent.GetComponent<MultiPositionConstraint>();
        Invoke("ResetPositionOnJoint", 0.1f);
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

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, Camera.main.WorldToScreenPoint(joint.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
       // if(worldPosition.y > minY && worldPosition.y < maxY)
       if(fixY)
        {
            if (worldPosition.y > minRange && worldPosition.y < maxRange)
            {
                joint.position = new Vector3(joint.position.x, worldPosition.y, joint.position.z);
            }
        }

       if(fixX)
        {
            if (worldPosition.x > minRange && worldPosition.x < maxRange)
            {
                Debug.Log(worldPosition.x);
                joint.position = new Vector3(worldPosition.x, joint.position.y, joint.position.z);
            }
        }

       if(fixZ)
        {
            if (worldPosition.z > minRange && worldPosition.z < maxRange)
            {
                joint.position = new Vector3(joint.position.x, joint.position.y, worldPosition.z);
            }
        }

        Manager.GameManager.Instance.ResetJoints();
        //Debug.Log(targetPosition.data.constrainedObject.position);
        //Debug.Log((desiredJointPosition - targetPosition.data.constrainedObject.position).magnitude);
        Manager.GameManager.Instance.CheckPerformance();
    }

    public float getPerformance()
    {
        if ((desiredJointPosition - targetPosition.data.constrainedObject.position).magnitude < bestPerformance)
            return 1f;
        else if ((desiredJointPosition - targetPosition.data.constrainedObject.position).magnitude < worstPerformance)
            return 0.5f;
        else return 0f;
    }

    public void DropHandler(BaseEventData data)
    {
        ResetPositionOnJoint();
    }

    public void ResetPositionOnJoint()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPosition.data.constrainedObject.position);
        GetComponent<RectTransform>().position = screenPos;
    }
}
