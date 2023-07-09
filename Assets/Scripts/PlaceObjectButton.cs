using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceObjectButton : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    
    [SerializeField] GameObject _objectPrefab;
    [SerializeField] GameObject _previewPrefab;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] Button button;
    public itemID _item;
    RaycastHit raycastHit;
    bool _validateObject;
    //public int _count;
    private void Start()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _previewPrefab.SetActive(true);
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out raycastHit, Mathf.Infinity, hitLayers) /*&& storeManager.instance.GetValue(_item) > 0*/)
        {
            _previewPrefab.transform.position = raycastHit.point;
            if (!_validateObject)
                _validateObject = true;
            else
            {
                _validateObject = false;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _previewPrefab.SetActive(false);
        if(_validateObject && storeManager.instance.GetValue(_item) > 0)
        {
            _validateObject = false;
            Instantiate(_objectPrefab, _previewPrefab.transform.position, _previewPrefab.transform.rotation);
            storeManager.instance.restQuantity(_item);
        }
    }

    private void Update()
    {
        if(storeManager.instance.GetValue(_item) <= 0)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }



}
