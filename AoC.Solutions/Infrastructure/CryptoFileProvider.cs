using System.Diagnostics.CodeAnalysis;
using Security.Crypto;

namespace AoC.Solutions.Infrastructure;

[ExcludeFromCodeCoverage]
public static class CryptoFileProvider
{
    public static string[] LoadFile(string path, string filename)
    {
        Console.WriteLine($"Loading {filename}.");
            
        if (GetKeyData() == null)
        {
            Console.Write("Please provide input decryption credentials in ./AoC.Solutions/AoC.Key\n\n");

            Environment.Exit(0);
        }
        
        var clearPath = $"./AoC.Solutions/{path}{filename}";

        var encryptedPath = $"{path}{Path.GetFileNameWithoutExtension(filename)}.encrypted";
        
        Console.WriteLine($"Clear path: {clearPath}.");
        
        Console.WriteLine($"Encrypted path: {encryptedPath}.");

        if (File.Exists(clearPath))
        {
            Console.WriteLine($"{filename}.clear found.");
            
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
            Console.WriteLine($"Decrypting {filename}.encrypted.");
            
            Decrypt(encryptedPath, clearPath);

            return File.ReadAllLines(clearPath);
        }
        
        Console.WriteLine($"Failure loading {encryptedPath}.");

        return null;
    }

    private static void Encrypt(string clearPath, string encryptedPath)
    {
        var data = File.ReadAllBytes(clearPath);

        var cipherProvider = new SymmetricCipher();

        var keyData = GetKeyData().Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        var iv = Convert.FromBase64String(keyData[1]);

        var salt = Convert.FromBase64String(keyData[2]);

        var key = Convert.FromBase64String(keyData[0]);

        var encrypted = cipherProvider.Encrypt(data, key, iv, salt);

        File.WriteAllBytes(encryptedPath, encrypted);
    }

    private static void Decrypt(string encryptedPath, string clearPath)
    {
        var cipherProvider = new SymmetricCipher();

        var keyData = GetKeyData().Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        var iv = Convert.FromBase64String(keyData[1]);

        var salt = Convert.FromBase64String(keyData[2]);

        var key = Convert.FromBase64String(keyData[0]);

        var decrypted = cipherProvider.Decrypt(File.ReadAllBytes(encryptedPath), key, iv, salt);

        File.WriteAllBytes(clearPath, decrypted);
    }

    private static IEnumerable<string> GetKeyData()
    {
        if (File.Exists("./AoC.Key"))
        {
            return File.ReadLines("./AoC.Key");
        }

        if (File.Exists("./AoC.Solutions/AoC.Key"))
        {
            return File.ReadLines("./AoC.Solutions/AoC.Key");
        }

        var keyData = Environment.GetEnvironmentVariable("KEY");
        
        if (! string.IsNullOrWhiteSpace(keyData))
        {
            Console.WriteLine("Keys found in environment.");
            
            var lines = keyData.Split(Environment.NewLine);

            return lines;
        }
        
        return null;
    }
}