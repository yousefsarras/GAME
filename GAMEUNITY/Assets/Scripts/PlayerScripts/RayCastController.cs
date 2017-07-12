using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RayCastController : MonoBehaviour {

    [HideInInspector]
    public BoxCollider2D collide;
    public RayCastOrigins rayCastOrigins;
    public const float skinWidth = .015f;
    //How many rays are fired vertically and horizontally
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    public LayerMask collisionMask;

    //allows it to not be overriden if extended
    //Awake because this step should be first
    public virtual void Awake()
    {
        collide = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    public void UpdateRayCastOrigins()
    {
        Bounds bounds = collide.bounds; //bounds of our collider
        //shrink those bounds from the outer edge to a skin
        bounds.Expand(skinWidth * -2);  //skin will inverted at all points

        //Assign the vector 2
        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = collide.bounds; //bounds of our collider
        //shrink those bounds from the outer edge to a skin
        bounds.Expand(skinWidth * -2);  //skin will inverted at all points

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        //Calculate Spacing
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RayCastOrigins   //store a buch of vector 2
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
