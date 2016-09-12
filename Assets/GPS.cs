using UnityEngine;
using System.Collections;

public class GPS : MonoBehaviour {

    string message = "nao iniciado";


    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            message = "Timed out";
            print(message);
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            message = "Unable to determine device location";
            print(message);
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            message = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            print(message);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        GUI.TextField(new Rect(10, 10, 300, 60), message, 25, style);
    }
}
