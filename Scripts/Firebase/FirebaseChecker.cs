using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using UnityEngine;

public class FirebaseChecker : MonoBehaviour
{
    private FirebaseApp app;
    
    private void Awake() {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(CalledAfterSomeTime);
    }
    private void CalledAfterSomeTime(Task<DependencyStatus> task) {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            //app = Firebase.FirebaseApp.DefaultInstance;
            // Set a flag here indiciating that Firebase is ready to use by your
            // application.
        }
        else
        {
            UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
    }
}
