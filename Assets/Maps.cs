using UnityEngine;
using System.Collections;

public class Maps : MonoBehaviour {

    string url = "";
    float lat;
    float lon;
    LocationInfo li;

    // Use this for initialization
    void Start()
    {
        li = new LocationInfo();
        lat = li.latitude; lon = li.longitude;
        url = "http://maps.google.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=14&size=800x600↦type=hybrid&sensor=true";
        WWW www = new WWW(url);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
    }

    // Update is called once per frame
    void Update () {
    }

}
