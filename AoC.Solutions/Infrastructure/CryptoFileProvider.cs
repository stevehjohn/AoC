using System.Diagnostics.CodeAnalysis;
using Security.Crypto;

namespace AoC.Solutions.Infrastructure;

[ExcludeFromCodeCoverage]
public static class CryptoFileProvider
{
    public static string[] LoadFile(string path, string filename)
    {
        if (GetKeyPath() == null)
        {
            Console.Write("Please provide input decryption credentials in ./AoC.Solutions/AoC.Key\n\n");

            Environment.Exit(0);
        }
        
        var clearPath = $"{path}{filename}";

        var encryptedPath = $"{path}{Path.GetFileNameWithoutExtension(filename)}.encrypted";

        if (File.Exists(clearPath))
        {
            if (! File.Exists(encryptedPath))
            {
                var tempPath = $"{path}{Path.GetFileNameWithoutExtension(filename)}.backup";

                File.Copy(clearPath, tempPath);

                Encrypt(clearPath, encryptedPath);

                File.Delete(clearPath);

                Decrypt(encryptedPath, clearPath);

                if (! File.ReadAllBytes(clearPath).SequenceEqual(File.ReadAllBytes(tempPath)))
                {
                    Console.WriteLine("Encryption verification failure.");

                    return null;
                }

                File.Delete(tempPath);
            }

            return File.ReadAllLines(clearPath);
        }

        if (File.Exists(encryptedPath) && ! File.Exists(clearPath))
        {
            Decrypt(encryptedPath, clearPath);

            return File.ReadAllLines(clearPath);
        }

        return null;
    }

    private static void Encrypt(string clearPath, string encryptedPath)
    {
        var data = File.ReadAllBytes(clearPath);

        var cipherProvider = new SymmetricCipher();

        var keyData = File.ReadLines(GetKeyPath()).Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        var iv = Convert.FromBase64String(keyData[1]);

        var salt = Convert.FromBase64String(keyData[2]);

        var key = Convert.FromBase64String(keyData[0]);

        var encrypted = cipherProvider.Encrypt(data, key, iv, salt);

        File.WriteAllBytes(encryptedPath, encrypted);
    }

    private static void Decrypt(string encryptedPath, string clearPath)
    {
        var cipherProvider = new SymmetricCipher();

        var keyData = File.ReadLines(GetKeyPath()).Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        var iv = Convert.FromBase64String(keyData[1]);

        var salt = Convert.FromBase64String(keyData[2]);

        var key = Convert.FromBase64String(keyData[0]);

        var decrypted = cipherProvider.Decrypt(File.ReadAllBytes(encryptedPath), key, iv, salt);

        File.WriteAllBytes(clearPath, decrypted);
    }

    private static string GetKeyPath()
    {
        if (File.Exists("./AoC.Key"))
        {
            return "./AoC.Key";
        }

        if (File.Exists("./AoC.Solutions/AoC.Key"))
        {
            return "./AoC.Solutions/AoC.Key";
        }

        return null;
    }
}