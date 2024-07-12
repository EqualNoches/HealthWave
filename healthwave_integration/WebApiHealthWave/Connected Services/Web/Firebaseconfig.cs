using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;

public static class FirebaseConfig
{
    public static void InitializeFirebase()
    {
        string pathToServiceAccountKey = "path/to/serviceAccountKey.json";

        using (var stream = new FileStream(pathToServiceAccountKey, FileMode.Open, FileAccess.Read))
        {
            var credential = GoogleCredential.FromStream(stream);
            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential
            });
        }
    }
}

