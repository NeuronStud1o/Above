using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    private FirebaseUser user;
    public FirebaseUser User { get => user; private set => user = value; }

    private UserMetadata metadata;
    public UserMetadata Metadata { get => metadata; private set => metadata = value; }

    void Start()
    {
        instance = this;
    }

    public void SetUser(FirebaseUser newUser)
    {
        user = newUser;
        metadata = user.Metadata;
    }
}
