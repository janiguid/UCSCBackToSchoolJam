using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    //Should we hardcode these?
    [SerializeField]
    private float mapHeight;
    [SerializeField]
    private float mapWidth;

    [SerializeField]
    private SpriteRenderer mapSprite;

    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private float maxYBound;
    [SerializeField]
    private float minYBound;
    [SerializeField]
    private float maxXBound;
    [SerializeField]
    private float minXBound;

    public float horizontalLength;
    public float verticalLength;

    // Start is called before the first frame update
    void Start()
    {
        //CalculateBoundaries();

    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    private void LateUpdate()
    {
        Vector3 finalTarget = new Vector3(targetPosition.position.x, targetPosition.position.y, -10);

        finalTarget.x = Mathf.Clamp(finalTarget.x, minXBound, maxXBound);
        finalTarget.y = Mathf.Clamp(finalTarget.y, minYBound, maxYBound);

        transform.position = finalTarget;
    }

    public void SetTarget(Transform newTarget)
    {
        targetPosition = newTarget;
    }

    //void CalculateBoundaries()
    //{
    //    //gets half of the vertical orhtographic screen size
    //    verticalLength = Camera.main.orthographicSize;
    //    //gets half of the horizontal orthographic screen size
    //    horizontalLength = verticalLength * Screen.width / Screen.height;

    //    //use sprite's bounds to get map height and width
    //    mapHeight = mapSprite.sprite.rect.height;
    //    mapWidth = mapSprite.sprite.rect.width;

    //    //get Y bounds
    //    minYBound = -mapHeight / 2;
    //    maxYBound = mapHeight / 2;

    //    //get X bounds
    //    minXBound = -mapWidth / 2;
    //    maxXBound = mapWidth / 2;

    //}

}
