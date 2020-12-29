using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryDragHandler : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	private Vector3 startPosition;
	private Transform startParent;

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
	}

}
