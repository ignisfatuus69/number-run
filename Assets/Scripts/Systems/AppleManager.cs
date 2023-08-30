using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    private IAppleAuthManager appleAuthManager;

    public string AppleUserIdKey { get; private set; }

    void Start()
    {
   // If the current platform is supported
   if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this.appleAuthManager = new AppleAuthManager(deserializer);
        }
    }

    void Update()
    {
    // Updates the AppleAuthManager instance to execute
    // pending callbacks inside Unity's execution loop
    if (this.appleAuthManager != null)
        {
            this.appleAuthManager.Update();
        }
    }

    public void SignIn()
    {
        // we are getting userid, email and full name and we need to save them somewhere
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this.appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
        // Obtained credential, cast it to IAppleIDCredential
        var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
            // Apple User ID
            // You should save the user ID somewhere in the device
            var userId = appleIdCredential.User;
                    PlayerPrefs.SetString(AppleUserIdKey, userId);

            // Email (Received ONLY in the first login)
            var email = appleIdCredential.Email;

            // Full name (Received ONLY in the first login)
            var fullName = appleIdCredential.FullName;

            // Identity token
            var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);

            // Authorization code
            var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);

            // And now you have all the information to create/login a user in your system
        }
            },
            error =>
            {
        // Something went wrong
        var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }
}
