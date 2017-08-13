using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RayCastController
{

    LayerMask passengerMask;
    //Move platform
    public Vector3 move;

    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateRayCastOrigins();
        Vector3 velocity = move * Time.deltaTime;
        //Before translate the platform
        MovePassenger(velocity);
        transform.Translate(velocity);
    }

    void MovePassenger(Vector3 velocity)
    {
        //b/c casting mutiple rays the player can be moved multiple times per frame
        HashSet<Transform> movePassengers = new HashSet<Transform>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //Vertically Moving Platform
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                //See which direction we are moving ie: move down bottom left corner
                Vector2 rayOrigin = (directionY == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.topLeft;
                //Add to ray origin
                //Add velocity.x velo on x axis to cast ray on the point we will be once we move
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                //perform from ray origin go in direction y (multiply by y)
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit)
                {
                    if (!movePassengers.Contains(hit.transform))
                    {
                        //once we move it we will add it 
                        movePassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }

        //Horizontal Moving
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                //See which direction we are moving ie: move down bottom left corner
                Vector2 rayOrigin = (directionX == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;
                //Add to ray origin
                //Add velocity.x velo on x axis to cast ray on the point we will be once we move
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                //perform from ray origin go in direction y (multiply by y)
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit)
                {
                    if (!movePassengers.Contains(hit.transform))
                    {
                        //once we move it we will add it 
                        movePassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = 0;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }

        //Passenger on top of a horizontally or downward platform
        //cast small ray up to see if there is something lfush
        if(directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            if (velocity.y != 0)
            {
                //one for small ray detection on for skin even
                float rayLength = skinWidth * 2;

                for (int i = 0; i < verticalRayCount; i++)
                {
                    //Ray always casted top left
                    //Add to ray origin
                    //Add velocity.x velo on x axis to cast ray on the point we will be once we move
                    Vector2 rayOrigin = rayCastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                    //always want the rau to be cast up so we dont multiply by directionY
                    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                    if (hit)
                    {
                        if (!movePassengers.Contains(hit.transform))
                        {
                            //once we move it we will add it 
                            movePassengers.Add(hit.transform);
                            float pushX = velocity.x;
                            float pushY = velocity.y;

                            hit.transform.Translate(new Vector3(pushX, pushY));
                        }
                    }
                }
            }
        }
    }
}
