using System.Diagnostics.CodeAnalysis;
using Security.Crypto;
using Security.Random;

namespace AoC.Solutions.Infrastructure;

[ExcludeFromCodeCoverage]
public static class CryptoFileProvider
{
    private static IRng _rng = new Rng();
    
    public static string[] LoadFile(string path, string filename)
    {
        if (GetKeyData() == null)
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
                
                Console.WriteLine("Encryption complete.");

                File.Delete(tempPath);
            }

            return File.ReadAllLines(clearPath);
        }

        if (File.Exists(encryptedPath) && ! File.Exists(clearPath))
        {
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

        var iv = new byte[16];
        
        _rng.GetBytes(iv);
        
        var key = Convert.FromBase64String(keyData[0]);

        var encrypted = cipherProvider.Encrypt(data, key, iv);

        var fileData = new byte[iv.Length + encrypted.Length];

        Buffer.BlockCopy(iv, 0, fileData, 0, iv.Length);
        
        Buffer.BlockCopy(encrypted, 0, fileData, 16, encrypted.Length);

        File.WriteAllBytes(encryptedPath, fileData);
    }

    private static void Decrypt(string encryptedPath, string clearPath)
    {
        var cipherProvider = new SymmetricCipher();

        var keyData = GetKeyData().Select(l => l.Split(":", StringSplitOptions.TrimEntries)[1]).ToArray();

        var key = Convert.FromBase64String(keyData[0]);

        var fileData = File.ReadAllBytes(encryptedPath);
        
        var iv = new byte[16];

        Buffer.BlockCopy(fileData, 0, iv, 0, iv.Length);

        var encryptedSize = fileData.Length - iv.Length;

        var encrypted = new byte[encryptedSize];
        
        Buffer.BlockCopy(fileData, iv.Length, encrypted, 0, encryptedSize);

        var decrypted = cipherProvider.Decrypt(encrypted, key, iv);

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
            var lines = keyData.Split(Environment.NewLine);

            return lines;
        }
        
        Console.WriteLine("Decryption keys not found.");
        
        return null;
    }
}