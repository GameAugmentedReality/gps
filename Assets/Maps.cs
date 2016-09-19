using UnityEngine;
using System.Collections;

public class Maps : MonoBehaviour {


    private string message = "nao iniciado";

    public static LocationInfo locationInfo;


    // Use this for initialization
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
            locationInfo = Input.location.lastData;
            print(message);

            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            string url = "http://maps.google.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=14&size=800x600&type=hybrid&sensor=true";
            //url = "http://maps.google.com/maps/api/staticmap?center=-16.6746576,-49.2385342&zoom=14&size=800x600&type=hybrid&sensor=true";
            WWW www = new WWW(url);
            yield return www;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;

        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();


    }

    // Update is called once per frame
    void Update () {
    }

}
