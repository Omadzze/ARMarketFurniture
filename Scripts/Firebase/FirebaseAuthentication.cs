using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class FirebaseAuthentication : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;

    private Firebase.Database.FirebaseDatabase mDatabaseRef;

    private Firebase.Auth.FirebaseUser user;
    //private Firebase.Auth.FirebaseAuth auth;
    private Uri imageUrl;
    public GameObject signUpCamvas; 
    public GameObject signInCanvas;
    public GameObject userAccountCavnas;
    private string idToken;

    #region CreateAccountUI

    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField phoneNumberInputField;
    

    #endregion

    #region LoginAccountUI

    public TMP_InputField emailInputFieldLogin;
    public TMP_InputField passwordInputFieldLogin;

    #endregion
    
    #region UI
    public TextMeshProUGUI email;
    public TextMeshProUGUI name;
    #endregion

    #region DatabaseList

    public List<string> emailList;
    public string userNameDb;
    public string phoneNumber;
    #endregion

    #region DeliveryAddress

    public GameObject deliveryAddress;
    
    public TMP_InputField addressInputField;
    public TMP_InputField postcodeInputField;
    public TMP_InputField cityInputField;
    
    #endregion

    public List<CloudData> userList;
    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        mDatabaseRef = Firebase.Database.FirebaseDatabase.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        userList = new List<CloudData>();
        
        // use to relogin again
        //auth.StateChanged -= AuthStateChanged;
        //auth = null;
    }

    public void CreateAccount()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailInputField.text, passwordInputField.text).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            //PlayerPrefs.DeleteAll();
            // Firebase user has been created.
            user = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            userNameDb = nameInputField.text;
            name.text = userNameDb;
            
            addNewUserDatabase(user.UserId, user.Email, nameInputField.text, phoneNumberInputField.text, "no", "no", "no");
        });
    }

    private void addNewUserDatabase(string userId, string email, string username, string phoneNumber,
        string postcode, string address, string city)
    {
        CloudData cloudData = new CloudData(username, email, phoneNumber, postcode, address, city);
        string json = JsonUtility.ToJson(cloudData);

        mDatabaseRef.RootReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
        Debug.Log("Saved to the Firebase");
        //userList.Add(cloudData);
        UserAccountCanvas();
    }
    

    public void SignInAccount()
    {
        auth.SignInWithEmailAndPasswordAsync(emailInputFieldLogin.text, passwordInputFieldLogin.text).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            GetDataAccount(newUser.Email);
        });
    }

    public void GetDataAccount(string email)
    {
        mDatabaseRef
            .GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                    Debug.LogError("Error occured with reading database");
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot users in snapshot.Children)
                    {
                        if (email == users.Child("email").GetRawJsonValue().Replace("\"", ""))
                        {
                            userNameDb = users.Child("username").GetRawJsonValue().Replace("\"", "");
                            phoneNumber = users.Child("phoneNumber").GetRawJsonValue().Replace("\"", "");
                            name.text = userNameDb;
                            Debug.Log("Name: " + userNameDb);
                            UserAccountCanvas();
                        }
                    }
                }
            });
        
        UserAccountCanvas();
    }
    
    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.UserId);
                Debug.Log("User Email: " + user.Email);
                GetDataAccount(user.Email);
            }
        }
    }

    public void UserAccountCanvas()
    {
        userAccountCavnas.SetActive(true);
        signInCanvas.SetActive(false);
        signUpCamvas.SetActive(false);
        deliveryAddress.SetActive(false);
        
        name.text = userNameDb;
        email.text = user.Email;
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    public void OpenDeliveryAdress()
    {
        deliveryAddress.SetActive(true);
    }

    public void SentAddress()
    {
        addNewUserDatabase(user.UserId, user.Email, userNameDb, phoneNumber, postcodeInputField.text,
            addressInputField.text, cityInputField.text);
        
        deliveryAddress.SetActive(false);
    }

    public void LogOutButton()
    {
        auth.SignOut();
        signUpCamvas.SetActive(true);
    }

    public void SignInButton()
    {
        signUpCamvas.SetActive(false);
        signInCanvas.SetActive(true);
    }
    
    public void SignUpButton()
    {
        signInCanvas.SetActive(false);
        signUpCamvas.SetActive(true);
    }
}
