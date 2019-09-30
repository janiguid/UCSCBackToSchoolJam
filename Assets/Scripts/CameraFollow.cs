using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Controller2D target;
    public Vector2 focusAreaSize;

    public float verticalOffset;

    FocusArea focusArea;

    private void Start()
    {
        focusArea = new FocusArea(target.collider.bounds, focusAreaSize);
    }

    private void LateUpdate()
    {
        focusArea.Update(target.collider.bounds);

        //Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        //transform.position = new Vector3(focusPosition.x, focusPosition.y, -10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }
    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds playerBounds, Vector2 focusAreaSize)
        {
            left = playerBounds.center.x - focusAreaSize.x/2;
            right = playerBounds.center.x + focusAreaSize.x / 2;
            top = playerBounds.min.y + focusAreaSize.y;
            bottom = playerBounds.min.y;
            center = new Vector2((left+right)/2, (top + bottom) / 2);

            velocity = Vector2.zero;
        }

        public void Update(Bounds playerBounds)
        {
            float deltaX = 0;
            if(playerBounds.max.x > right)
            {
                deltaX = playerBounds.max.x - right;
            }else if(playerBounds.min.x < left)
            {
                deltaX = playerBounds.min.x - left;
            }

            left += deltaX;
            right += deltaX;

            float deltaY = 0;
            if (playerBounds.max.y > top)
            {
                deltaY = playerBounds.max.y - top;
            }
            else if (playerBounds.min.y < bottom)
            {
                deltaY = playerBounds.min.y - bottom;
            }

            top += deltaY;
            bottom += deltaY;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(deltaX, deltaY);
        }
    }
}
