using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class UserInputHandler : MonoBehaviour
    {
        #region EVENTS
        public delegate void TapAction(Touch touch);
        public static event TapAction OnTouchAction;
        //TO check accelerometer action
        public delegate void AccelerometerChangedAction(Vector3 acceleration); 
        public static event AccelerometerChangedAction OnAccelerometerChanged;
        #endregion



        #region PUBLIC VARIABLES
        public float tapMaxMovement = 50f;      // Maximum pixel,a tap can move   
        #endregion

        
        #region PRIVATE VARIABLES
        private Vector2 movement;      // Movement vector will track how far we move
        private bool tapGestureFailed = false; // tap gesture will become, if tap moves too far
        private Vector3 defaultAcceleration;
        #endregion



        #region MONOBEHAVIOUR METHODS

        // Update is called once per frame
        void Update()
        {

            if (Input.touchCount > 0)     // To finding out, no. of touches greater than 0 or not. If no touches, then no movement
            {
                Touch touch = Input.touches[0];  // Need to find out no. of touches on the screen. If there are more no. of touches, need to call array
                if (touch.phase == TouchPhase.Began) // We have several touch phases, began enters the first frame of the touch
                {
                    movement = Vector2.zero; //We made our movement to zero
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    movement += touch.deltaPosition; // The position delta since last change in pixel coordinates.

                    if (movement.magnitude > tapMaxMovement)
                    {
                        tapGestureFailed = true;
                        //Debug.Log("tap failed");
                    }
                }
                else
                {
                    if (!tapGestureFailed) // If finger is removed, then we are calling tap 
                    {
                        //Debug.Log("tap enabled");
                        if (OnTouchAction != null)
                        {
                            OnTouchAction(touch);
                        }
                    }
                    tapGestureFailed = false; // ready for the next tap
                }
            }
        }
 


        void OnEnable()
        {
            defaultAcceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
        }
    }
    #endregion

}
