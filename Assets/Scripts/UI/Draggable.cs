using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("이동할 UI 요소")]
    [SerializeField] private Transform moveUiTarget;
    private Vector2 originPos;
    private Vector2 originMousePos;
    private Vector2 movedPos;

    private CursorLockMode prevCursorLockMode;
    private void StopDrag()
    {
        Cursor.lockState = prevCursorLockMode;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        prevCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;

        originPos = moveUiTarget.position;
        originMousePos = eventData.position;

        moveUiTarget.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        movedPos = eventData.position - originMousePos;

        moveUiTarget.position = originPos + movedPos;
        if(Input.GetMouseButtonUp(0) || !gameObject.activeInHierarchy)
        {
            StopDrag();
        }
    }

}
