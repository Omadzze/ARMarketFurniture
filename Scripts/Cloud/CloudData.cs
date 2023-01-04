using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData : MonoBehaviour
{
    public string email;
    public string username;
    public string phoneNumber;
    public string postcode;
    public string address;
    public string city;
    public CloudData()
    {
    }

    public CloudData(string username, string email, string phoneNumber, string postcode, string address, string city)
    {
        this.username = username;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.postcode = postcode;
        this.address = address;
        this.city = city;
    }
}
