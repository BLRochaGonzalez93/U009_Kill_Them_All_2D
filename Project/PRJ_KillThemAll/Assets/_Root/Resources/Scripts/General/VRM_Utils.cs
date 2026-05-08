using UnityEngine;

namespace VRMGames
{
    public static class VRM_Utils
    {
        #region Mouse World Position
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        #endregion

        #region Float Time ==> String Text
        public static string FloatTimeToStringText(float totalTime)
        {
            string timeText;
            int minutesTime, secondsTime;
            minutesTime = Mathf.FloorToInt(totalTime / 60);
            secondsTime = Mathf.FloorToInt(totalTime % 60);
            timeText = string.Format("{0:00}:{1:00}", minutesTime, secondsTime);
            return timeText;
        }
        #endregion

        #region Rotate Around Pivot
        public static void RotateAroundPivot(GameObject rotatingObject, GameObject pivot, float rotationSpeed)
        {
            rotatingObject.transform.RotateAround(pivot.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
        #endregion
    }
}