using System.Collections.Generic;
using UnityEngine;

public class WallsCreationController : MonoBehaviour
{
    public GameObject baseWallPrefab;
    private GameObject currentWall;
    private Vector2 startPos;
    private bool isCreating = false;
    public bool available;

    void Update()
    {
        if (!available) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touchPos;
                    currentWall = Instantiate(baseWallPrefab, startPos, Quaternion.identity);
                    isCreating = true;
                    break;

                case TouchPhase.Moved:
                    if (isCreating && currentWall != null)
                    {
                        Vector2 direction = touchPos - startPos;
                        float distance = direction.magnitude;
                        Vector2 midPoint = (startPos + touchPos) / 2f;

                        currentWall.transform.position = midPoint;
                        currentWall.transform.right = direction.normalized;
                        currentWall.transform.localScale = new Vector3(distance, currentWall.transform.localScale.y, 1);
                    }
                    break;

                case TouchPhase.Ended:
                    isCreating = false;
                    break;
            }
        }
    }
}
