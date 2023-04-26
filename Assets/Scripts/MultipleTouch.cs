using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    [SerializeField] private GameObject touchPoint;
    private List<TouchLocation> touches = new List<TouchLocation>();
    private bool pause = false;

    private void OnEnable()
    {
        Menu.onPause += Pause;
    }
    private void OnDisable()
    {
        Menu.onPause -= Pause;
    }

    private void Update()
    {
        if (!pause)
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    touches.Add(new TouchLocation(touch.fingerId, createCircle(touch)));
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == touch.fingerId);
                    Destroy(thisTouch.circle);
                    touches.RemoveAt(touches.IndexOf(thisTouch));
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == touch.fingerId);
                    thisTouch.circle.transform.position = getTouchPosition(touch.position);
                }
                ++i;
            }
        }
    }
    private Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
    private GameObject createCircle(Touch touch)
    {
        GameObject newTouchPoint = Instantiate(touchPoint);
        newTouchPoint.name = "Touch" + touch.fingerId;
        newTouchPoint.transform.position = getTouchPosition(touch.position);
        return newTouchPoint;
    }

    private void Pause(bool pause)
    {
        this.pause = pause;
    }
}
