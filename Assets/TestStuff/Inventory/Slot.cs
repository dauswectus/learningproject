using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler {

	private int i;

	IEnumerator Timer(int sec)
	{
		yield return new WaitForSeconds(2);
		i = 0;
	}

	public GameObject item 
	{
		get
		{ 
			if (transform.childCount > 0)
			{
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine (Timer (2));
		i++;
		if(i == 2)
		{
			Debug.Log (gameObject.transform.GetChild(0).GetChild(0));
			Transform inventoryItem = gameObject.transform.GetChild (0).GetChild (0);
			if (inventoryItem.GetComponent<InteractableItemScript> ().InventoryToHand ()) 
			{
				Destroy (gameObject.transform.GetChild (0).gameObject);
			}
			i = 0;
		}

	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if (!item)
		{
			DragHandeler.itemBeingDragged.transform.SetParent (transform);
			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		} 
	}

	#endregion
}
