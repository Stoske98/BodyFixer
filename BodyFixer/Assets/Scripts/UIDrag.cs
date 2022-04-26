using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Animations.Rigging;
public class UIDrag : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform joint;
    TwoBoneIKConstraint IKContrain;
    [SerializeField]
    private Vector3 desiredJointPosition;
    [SerializeField]
    private float bestPerformance;
    [SerializeField]
    private float worstPerformance;

    public void Start()
    {
        IKContrain = joint.transform.parent.GetComponent<TwoBoneIKConstraint>();
        Invoke("ResetPositionOnJoint", 0.1f);
    }
    public void DragHandler(BaseEventData data)
    {
        Debug.Log("AE u kurac");
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
        joint.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

         Debug.Log((desiredJointPosition - IKContrain.data.tip.position).magnitude);
        //Debug.Log(IKContrain.data.tip.position);
        Manager.GameManager.Instance.CheckPerformance();
    }

    public float getPerformance()
    {
        if ((desiredJointPosition - IKContrain.data.tip.position).magnitude < bestPerformance)
            return 1f;
        else if ((desiredJointPosition - IKContrain.data.tip.position).magnitude < worstPerformance)
            return 0.5f;
        else return 0f;
    }

    public void DropHandler(BaseEventData data)
    {
        ResetPositionOnJoint();
    }

    public void ResetPositionOnJoint()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(IKContrain.data.tip.position);
        GetComponent<RectTransform>().position = screenPos;
    }
}
