using System.Collections;
using UnityEngine;


public class Controller2D : RayCastController {

    private PlayerMovement player;
    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;
    float maxClimbAngle = 70f;
    float maxDescendAngle = 75f;

    //Vector2 playerInput;

	public override void Start () {
        base.Start();
        player = gameObject.GetComponent<PlayerMovement>();
    }

    void VerticalCollisions(ref Vector3 velocity)    //Not simply a variable copy
    {
        //direction of the vertical ray
        float directionY = Mathf.Sign(velocity.y);
        //size of the ray determined by the y velocity
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            //See which direction we are moving ie: move down bottom left corner
            Vector2 rayOrigin = (directionY == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.topLeft;
            //Add to ray origin
            //Add velocity.x velo on x axis to cast ray on the point we will be once we move
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            //perform from ray origin go in direction y (multiply by y)
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                //Move through platfroms up
                if (hit.collider.tag == "Through")
                {
                    //We have a velocity of skin width in the rays so it slides up
                    if(directionY == 1 || hit.distance == 0)
                    {
                        //Skip everything below and move to next ray in loop
                        continue;
                    }
                    if (collisions.fallingThroughPlatform)
                    {
                        continue;
                    }
                    if(playerInput.y == -1 && Input.GetKeyDown(KeyCode.Space))
                    {
                        collisions.fallingThroughPlatform = true;
                        //caled after half a second
                        Invoke("ResetFallingThroughPlatform", 0.1f);
                        continue;
                    }
                }

                
                // set y velo equal to the amount we have to move. move ray distance
                velocity.y = (hit.distance - skinWidth) * directionY;
                //Change or update ray length once we hit something
                rayLength = hit.distance;

                //climbing up slope against velocity y collision
                //update x axis while climbing so it does not bounce as y is updaing without x
                //Velocity y is updated to a stop but x is still not reduced
                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }


                //set it depending on the direction
                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
            }
        }
        //Out of Loop for rays final checks
        if (collisions.climbingSlope)
        {
            //fire a horizontal ray from the point on the y axis which we will be to check for a new slope at that height
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            // if we are moving left = left otherwaise moving right = right
            // cast from new height
            Vector2 rayOrigin = ((directionX == 0) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight) * Mathf.Sign(velocity.x) + Vector2.up * velocity.y;
            
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if (hit)
            {
                //Get the angle of what we hit
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                //if new slope angle is not equal then we have collided with  new slope
                if(slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }


    void HorizontalCollisions(ref Vector3 velocity)    //Not simply a variable copy
    {
        //direction of the vertical ray
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            //See which direction we are moving ie: move down bottom left corner
            Vector2 rayOrigin = (directionX == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;
            //Add to ray origin
            //Add velocity.x velo on x axis to cast ray on the point we will be once we move
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            //perform from ray origin go in direction y (multiply by y)
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Through")
                {
                    continue;
                }
                //Finding the angle of hit collision
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                //done only if bottom most ray
                if(i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }
                    //Make sure that the ray is passed to the edge for fine climbing
                    float distanceToSlopeStart = 0;
                    //StartClimbing a new slope
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        //subtracting so when climbslope it only uses velocity x
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }


                //check rest of the rays for collisions
                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    //Change or update ray length once we hit something
                    rayLength = hit.distance;

                    //climbing up slope against velocity x collision
                    //update Y axis while climbing so it does not bounce as x is updaing without y
                    //Velocity x is updated but y is still not reduced
                    if (collisions.climbingSlope)
                    {
                        //collisions.slopeAngle specifies which angle we are climbing
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x); 
                    }

                    //set it depending on the direction
                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        //up the slope distance
        float moveDistance = Mathf.Abs(velocity.x);

        //Velocity y is reset everytime because horizontal is being intialized first
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            //Move towards the same direction as initial velocity.x 
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            //When we are climbing we change the collision to show that we are standing on the ground
            //Fix jump slope
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }

    }

    void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        //cast a ray downwards
        //if moving left ray start bottom right vise versa
        Vector2 rayOrigin = (directionX == -1) ? rayCastOrigins.bottomRight : rayCastOrigins.bottomLeft;
        //infinity because we dont know when it will hit
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            //flat surface
            if(slopeAngle != 0 && slopeAngle < maxDescendAngle)
            {
                //check if we are moving down the slope
                //hit.normal is direction perpendicular to slope so we can see from its x which way the slope is facing
                //we compare hitNormal's velocity x to the directionX to see if they are moving in the same direction se we move down the slope
                if(Mathf.Sign(hit.normal.x) == directionX)
                {
                    //since ray is infinity down
                    //check to see if we are close enough to the slope for to take effect
                    //how far away from the slope      how far down we have to move down now that we know the slope's angle and velocity on the x axis
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        //Velocity y is reset everytime because horizontal is being intialized first
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        //Move towards the same direction as initial velocity.x 
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;

                    }
                }
            }
        }
    }

    public void Move(Vector3 velocity)
    {
        Move(velocity, Vector2.zero);
    }

    public void Move(Vector3 velocity, Vector2 input)
    {
        UpdateRayCastOrigins();
        collisions.Reset(); //blank slate each time

        //solve V problem save the descending slope
        collisions.velocityOld = velocity;
        playerInput = input;

        if(velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        //Handle Collisions and modify velocity
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        //Move the player
        transform.Translate(velocity);
    }

    void ResetFallingThroughPlatform()
    {
        collisions.fallingThroughPlatform = false;

    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld;
        //V slope slow down problem
        //reduces velocity on x axis so movedistance is reduced
        public Vector3 velocityOld;

        public bool fallingThroughPlatform;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
            descendingSlope = false;
        }
    }
}
