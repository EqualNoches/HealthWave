using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;

namespace WebApiHealthWave.Services.Firestore
{
    public class FirestoreService
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreService()
        {
            string pathToServiceAccountKey = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Credentials", "serviceAccountKey.json");
            GoogleCredential credential;
            using (var jsonStream = new FileStream(pathToServiceAccountKey, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(jsonStream);
            }
            FirestoreClientBuilder clientBuilder = new FirestoreClientBuilder
            {
                Credential = credential
            };
            _firestoreDb = FirestoreDb.Create("healthwave-web-database", clientBuilder.Build());
        }

        public async Task AddDocumentAsync(string collectionName, Dictionary<string, object> documentData)
        {
            CollectionReference collection = _firestoreDb.Collection(collectionName);
            await collection.AddAsync(documentData);
        }

        public async Task<Dictionary<string, object>?> GetDocumentAsync(string collectionName, string documentId)
        {
            DocumentReference document = _firestoreDb.Collection(collectionName).Document(documentId);
            DocumentSnapshot snapshot = await document.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ToDictionary();
            }

            return null;
        }

        public async Task UpdateDocumentAsync(string collectionName, string documentId, Dictionary<string, object> updates)
        {
            DocumentReference document = _firestoreDb.Collection(collectionName).Document(documentId);
            await document.UpdateAsync(updates);
        }

        public async Task DeleteDocumentAsync(string collectionName, string documentId)
        {
            DocumentReference document = _firestoreDb.Collection(collectionName).Document(documentId);
            await document.DeleteAsync();
        }

        public async Task<List<Dictionary<string, object>>> GetAllDocumentsAsync(string collectionName)
        {
            CollectionReference collection = _firestoreDb.Collection(collectionName);
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            var documents = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                documents.Add(document.ToDictionary());
            }

            return documents;
        }
    }
}
