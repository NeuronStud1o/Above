using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class CryptoHelper
{
    private const int KeySize = 256;
    private const int BlockSize = 128;
    private const int DerivationIterations = 1000;

    public static string GenerateKeyFromUid(string uid)
    {
        byte[] uidBytes = Encoding.UTF8.GetBytes(uid);
        
        using (HMACSHA256 hmac = new HMACSHA256(uidBytes))
        {
            byte[] key = new byte[hmac.HashSize / 8];
            string keyString = Convert.ToBase64String(hmac.ComputeHash(uidBytes));

            return keyString;
        }
    }

    public static void Encrypt(object jsonData, string password)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        string jsonString = JsonUtility.ToJson(jsonData, true);
        EncryptInternal(filePath, jsonString, password);
    }

    public static T LoadAndDecrypt<T>(string filePath, string password)
    {
        string decryptedJson = LoadAndDecryptInternal(filePath, password);
        return JsonUtility.FromJson<T>(decryptedJson);
    }

    private static void EncryptInternal(string filePath, string json, string password)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = KeySize;
            aesAlg.BlockSize = BlockSize;

            Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(password, salt: GenerateSalt(password), iterations: DerivationIterations);
            aesAlg.Key = keyDerivation.GetBytes(KeySize / 8);
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(json);
                    }
                }

                byte[] encryptedData = msEncrypt.ToArray();

                using (FileStream fileStream = File.Create(filePath))
                {
                    fileStream.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    fileStream.Write(encryptedData, 0, encryptedData.Length);
                }
            }
        }
    }

    private static string LoadAndDecryptInternal(string filePath, string password)
    {
        try
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byte[] iv = new byte[BlockSize / 8];
                fileStream.Read(iv, 0, iv.Length);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.KeySize = KeySize;
                    aesAlg.BlockSize = BlockSize;

                    Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(password, salt: GenerateSalt(password), iterations: DerivationIterations);
                    aesAlg.Key = keyDerivation.GetBytes(KeySize / 8);
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            fileStream.CopyTo(csDecrypt);
                        }

                        byte[] decryptedData = msDecrypt.ToArray();
                        return System.Text.Encoding.UTF8.GetString(decryptedData);
                    }
                }
            }
        }
        catch (Exception)
        {
            return default;
        }
        
    }

    private static byte[] GenerateSalt(string pass)
    {
        if (pass == null)
        {
            throw new ArgumentNullException(nameof(pass));
        }

        byte[] salt = Encoding.UTF8.GetBytes(pass);

        using (HMACSHA256 hmac = new HMACSHA256(salt))
        {
            byte[] key = new byte[hmac.HashSize / 8];

            return hmac.ComputeHash(salt);
        }
    }
}