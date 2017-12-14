using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to scale the current GameObject
	public class LeanCustomScale : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers;

		[Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("Does scaling require an object to be selected?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("If you want the mouse wheel to simulate pinching then set the strength of it here")]
		[Range(-1.0f, 1.0f)]
		public float WheelSensitivity;

		[Tooltip("The camera that will be used to calculate the zoom")]
		public Camera Camera;

		[Tooltip("Should the scaling be performanced relative to the finger center?")]
		public bool Relative;

        public float minScaleSize = .1f;
        public float maxScaleSize = 5f;
        public bool useRangeForScaling = false;
		
#if UNITY_EDITOR
		protected virtual void Reset()
		{
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}
#endif

		protected virtual void Update()
		{
			// If we require a selectable and it isn't selected, cancel scaling
			if (RequiredSelectable != null && RequiredSelectable.IsSelected == false)
			{
				return;
			}

			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

			// Calculate the scaling values based on these fingers
			var scale        = LeanGesture.GetPinchScale(fingers, WheelSensitivity);
			var screenCenter = LeanGesture.GetScreenCenter(fingers);

			// Perform the scaling
			Scale(scale, screenCenter);
		}

		private void Scale(float scale, Vector2 screenCenter)
		{
			// Make sure the scale is valid
			if (scale > 0.0f)
			{
				if (Relative == true)
				{
					// If camera is null, try and get the main camera, return true if a camera was found
					if (LeanTouch.GetCamera(ref Camera) == true)
					{
						// Screen position of the transform
						var screenPosition = Camera.WorldToScreenPoint(transform.position);
						
						// Push the screen position away from the reference point based on the scale
						screenPosition.x = screenCenter.x + (screenPosition.x - screenCenter.x) * scale;
						screenPosition.y = screenCenter.y + (screenPosition.y - screenCenter.y) * scale;
						
						// Convert back to world space
						transform.position = Camera.ScreenToWorldPoint(screenPosition);
						
						// Grow the local scale by scale
						if(useRangeForScaling)
                        {
                            
                            Vector3 newScale= transform.localScale * scale;
                            float newLocalX = newScale.x;
                            float newLocalY = newScale.y;
                            float newLocalZ = newScale.z;
                            //print(string.Format("before changing x:{0} y:{0} z:{0}", newLocalX, newLocalY, newLocalZ));
                            if (newLocalX < minScaleSize)
                            {
                                newLocalX = minScaleSize;
                            }
                            if (newLocalY < minScaleSize)
                            {
                                newLocalY = minScaleSize;
                            }
                            if (newLocalZ < minScaleSize)
                            {
                                newLocalZ = minScaleSize;
                            }

                            //print(string.Format("after changing x:{0} y:{0} z:{0}", newLocalX, newLocalY, newLocalZ));
                            newScale = new Vector3(newLocalX, newLocalY, newLocalZ);

                            transform.localScale = newScale;
                        }
                        else
                        {
                            transform.localScale *= scale;
                        }

                    }
				}
				else
				{
                    // Grow the local scale by scale
                    if (useRangeForScaling)
                    {

                        Vector3 newScale = transform.localScale * scale;
                        float newLocalX = newScale.x;
                        float newLocalY = newScale.y;
                        float newLocalZ = newScale.z;
                        //print(string.Format("before changing x:{0} y:{0} z:{0}", newLocalX, newLocalY, newLocalZ));
                        if (newLocalX < minScaleSize)
                        {
                            newLocalX = minScaleSize;
                        }
                        if (newLocalY < minScaleSize)
                        {
                            newLocalY = minScaleSize;
                        }
                        if (newLocalZ < minScaleSize)
                        {
                            newLocalZ = minScaleSize;
                        }

                        //print(string.Format("after changing x:{0} y:{0} z:{0}", newLocalX, newLocalY, newLocalZ));
                        newScale = new Vector3(newLocalX, newLocalY, newLocalZ);

                        transform.localScale = newScale;
                    }
                    else
                    {
                        transform.localScale *= scale;
                    }
                }
			}
		}
	}
}