using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Controller2D target;
    public Vector2 focusAreaSize;
    FocusArea focusArea;

    public float verticalOffSet;

    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothLookVelocityY;

    public float yPosRestriction;

    //key switch correction
    bool lookAheadStopped;

    float nextTimeToSearch = 0;
    private void Start()
    {
        focusArea = new FocusArea(target.collide.bounds, focusAreaSize);
    }

    //All player movement has been finished in the frame
    private void LateUpdate()
    {
        if(target == null)
        {
            FindPlayer();
            return;
        }
        
        focusArea.Update(target.collide.bounds);

        //Camera
        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffSet;

        //Camera clamp
        focusPosition = new Vector2(focusPosition.x, Mathf.Clamp(focusPosition.y, yPosRestriction, Mathf.Infinity));

        //Look ahead direction
        if(focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            //if we are holding down the left key and suddenly turn the right key player will continue to move to the left for a few frames
            //set targetlookahead X if focus area is moving but b/c of the smoothing the focus area could be moving left while the player is moving right
            //only set lookaheadx if the inpupt is in the same direction as the focus area is MOVING
            if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
            {
                //set to the full amount
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStopped)
                {                 //stop targetLookahead x from moving too far
                                  ///then add a fraction of the remaining
                    //addition is how far we need to complete the look ahead
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4;
                }
            }
        }
        //Target 
        targetLookAheadX = lookAheadDirX * lookAheadDstX;
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        //vertical smoothing
        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothLookVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;

        //Camera Clamp
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, yPosRestriction, Mathf.Infinity), transform.position.z);

        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x/2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right)/2, (top + bottom)/2);
        }

        public void Update(Bounds targetBounds)
        {
            //Moving against either the left or right bound
            float shiftX = 0;
            //Moving more than the left bound, moving against
            if(targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }else if(targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            //Updating the shift movement
            left += shiftX;
            right += shiftX;

            //Vertical Shift
            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            //update center focus area has moved
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);

            //if we want to know how far the focus area has moved
            velocity = new Vector2(shiftX, shiftY);
        }

    }

    void FindPlayer()
    {
        if(nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if(searchResult != null)
            {
                target = searchResult.transform.GetComponent<Controller2D>();
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
