using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update

    string userID;
    DatabaseReference reference;
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //send from ui 
    public void CreateUser(string name, int score)
    {
        User newUser = new User(name, score);
        string json = JsonUtility.ToJson(newUser);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

}
