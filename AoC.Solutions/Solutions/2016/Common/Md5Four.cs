using System.Buffers.Binary;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace AoC.Solutions.Solutions._2016.Common;

public static class Md5Four
{
    public static void Hash(ReadOnlySpan<byte> prefix, int firstIndex, Span<byte> destination, int additionalHashes = 0)
    {
        if (firstIndex < 0 || firstIndex > int.MaxValue - 3)
        {
            throw new ArgumentOutOfRangeException(nameof(firstIndex));
        }

        ArgumentOutOfRangeException.ThrowIfNegative(additionalHashes);

        if (destination.Length < 64 || prefix.Length > 54)
        {
            throw new ArgumentException("Invalid destination or prefix length.");
        }

        Span<byte> blocks = stackalloc byte[256];

        blocks.Clear();

        for (var lane = 0; lane < 4; lane++)
        {
            var block = blocks.Slice(lane * 64, 64);

            prefix.CopyTo(block);

            if (! Utf8Formatter.TryFormat(firstIndex + lane, block[prefix.Length..55], out var written))
            {
                throw new ArgumentException("MD5 input exceeds 55 bytes.", nameof(prefix));
            }

            var length = prefix.Length + written;

            block[length] = 0x80;

            BinaryPrimitives.WriteUInt64LittleEndian(block[56..], (ulong) length * 8);
        }

        Span<Vector128<uint>> m = stackalloc Vector128<uint>[16];
        for (var j = 0; j < 16; j++)
        {
            var offset = j * 4;
            m[j] = Vector128.Create(
                BinaryPrimitives.ReadUInt32LittleEndian(blocks[offset..]),
                BinaryPrimitives.ReadUInt32LittleEndian(blocks[(offset + 64)..]),
                BinaryPrimitives.ReadUInt32LittleEndian(blocks[(offset + 128)..]),
                BinaryPrimitives.ReadUInt32LittleEndian(blocks[(offset + 192)..]));
        }

        Compress(m, out var a, out var b, out var c, out var d);

        for (var j = 0; j < additionalHashes; j++)
        {
            m[0] = HexWord(a);
            m[1] = HexWord(a >> 16);
            m[2] = HexWord(b);
            m[3] = HexWord(b >> 16);
            m[4] = HexWord(c);
            m[5] = HexWord(c >> 16);
            m[6] = HexWord(d);
            m[7] = HexWord(d >> 16);

            Compress32(m, out a, out b, out c, out d);
        }

        for (var lane = 0; lane < 4; lane++)
        {
            var output = destination.Slice(lane * 16, 16);

            BinaryPrimitives.WriteUInt32LittleEndian(output, a.GetElement(lane));
            BinaryPrimitives.WriteUInt32LittleEndian(output[4..], b.GetElement(lane));
            BinaryPrimitives.WriteUInt32LittleEndian(output[8..], c.GetElement(lane));
            BinaryPrimitives.WriteUInt32LittleEndian(output[12..], d.GetElement(lane));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<uint> HexWord(Vector128<uint> x)
    {
        var n = ((x & Vector128.Create(0x000000F0u)) >> 4)
                | ((x & Vector128.Create(0x0000000Fu)) << 8)
                | ((x & Vector128.Create(0x0000F000u)) << 4)
                | ((x & Vector128.Create(0x00000F00u)) << 16);

        var letters = ((n + Vector128.Create(0x06060606u)) & Vector128.Create(0x10101010u)) >> 4;

        return n + Vector128.Create(0x30303030u) + letters * Vector128.Create(39u);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<uint> Rol(Vector128<uint> x, int shift)
    {
        return (x << shift) | (x >> (32 - shift));
    }

    private static void Compress(ReadOnlySpan<Vector128<uint>> m, out Vector128<uint> aa, out Vector128<uint> bb, out Vector128<uint> cc, out Vector128<uint> dd)
    {
        var a = Vector128.Create(0x67452301u);
        var b = Vector128.Create(0xEFCDAB89u);
        var c = Vector128.Create(0x98BADCFEu);
        var d = Vector128.Create(0x10325476u);

        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[0] + Vector128.Create(0xD76AA478u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[1] + Vector128.Create(0xE8C7B756u), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[2] + Vector128.Create(0x242070DBu), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[3] + Vector128.Create(0xC1BDCEEEu), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[4] + Vector128.Create(0xF57C0FAFu), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[5] + Vector128.Create(0x4787C62Au), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[6] + Vector128.Create(0xA8304613u), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[7] + Vector128.Create(0xFD469501u), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[8] + Vector128.Create(0x698098D8u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[9] + Vector128.Create(0x8B44F7AFu), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[10] + Vector128.Create(0xFFFF5BB1u), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[11] + Vector128.Create(0x895CD7BEu), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[12] + Vector128.Create(0x6B901122u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[13] + Vector128.Create(0xFD987193u), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[14] + Vector128.Create(0xA679438Eu), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[15] + Vector128.Create(0x49B40821u), 22);

        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[1] + Vector128.Create(0xF61E2562u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[6] + Vector128.Create(0xC040B340u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[11] + Vector128.Create(0x265E5A51u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[0] + Vector128.Create(0xE9B6C7AAu), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[5] + Vector128.Create(0xD62F105Du), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[10] + Vector128.Create(0x02441453u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[15] + Vector128.Create(0xD8A1E681u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[4] + Vector128.Create(0xE7D3FBC8u), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[9] + Vector128.Create(0x21E1CDE6u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[14] + Vector128.Create(0xC33707D6u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[3] + Vector128.Create(0xF4D50D87u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[8] + Vector128.Create(0x455A14EDu), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[13] + Vector128.Create(0xA9E3E905u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[2] + Vector128.Create(0xFCEFA3F8u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[7] + Vector128.Create(0x676F02D9u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[12] + Vector128.Create(0x8D2A4C8Au), 20);

        a = b + Rol(a + (b ^ c ^ d) + m[5] + Vector128.Create(0xFFFA3942u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[8] + Vector128.Create(0x8771F681u), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[11] + Vector128.Create(0x6D9D6122u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[14] + Vector128.Create(0xFDE5380Cu), 23);
        a = b + Rol(a + (b ^ c ^ d) + m[1] + Vector128.Create(0xA4BEEA44u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[4] + Vector128.Create(0x4BDECFA9u), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[7] + Vector128.Create(0xF6BB4B60u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[10] + Vector128.Create(0xBEBFBC70u), 23);
        a = b + Rol(a + (b ^ c ^ d) + m[13] + Vector128.Create(0x289B7EC6u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[0] + Vector128.Create(0xEAA127FAu), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[3] + Vector128.Create(0xD4EF3085u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[6] + Vector128.Create(0x04881D05u), 23);
        a = b + Rol(a + (b ^ c ^ d) + m[9] + Vector128.Create(0xD9D4D039u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[12] + Vector128.Create(0xE6DB99E5u), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[15] + Vector128.Create(0x1FA27CF8u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[2] + Vector128.Create(0xC4AC5665u), 23);

        a = b + Rol(a + (c ^ (b | ~d)) + m[0] + Vector128.Create(0xF4292244u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[7] + Vector128.Create(0x432AFF97u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[14] + Vector128.Create(0xAB9423A7u), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[5] + Vector128.Create(0xFC93A039u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + m[12] + Vector128.Create(0x655B59C3u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[3] + Vector128.Create(0x8F0CCC92u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[10] + Vector128.Create(0xFFEFF47Du), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[1] + Vector128.Create(0x85845DD1u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + m[8] + Vector128.Create(0x6FA87E4Fu), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[15] + Vector128.Create(0xFE2CE6E0u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[6] + Vector128.Create(0xA3014314u), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[13] + Vector128.Create(0x4E0811A1u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + m[4] + Vector128.Create(0xF7537E82u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[11] + Vector128.Create(0xBD3AF235u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[2] + Vector128.Create(0x2AD7D2BBu), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[9] + Vector128.Create(0xEB86D391u), 21);

        aa = a + Vector128.Create(0x67452301u);
        bb = b + Vector128.Create(0xEFCDAB89u);
        cc = c + Vector128.Create(0x98BADCFEu);
        dd = d + Vector128.Create(0x10325476u);
    }

    private static void Compress32(ReadOnlySpan<Vector128<uint>> m, out Vector128<uint> aa, out Vector128<uint> bb, out Vector128<uint> cc, out Vector128<uint> dd)
    {
        var a = Vector128.Create(0x67452301u);
        var b = Vector128.Create(0xEFCDAB89u);
        var c = Vector128.Create(0x98BADCFEu);
        var d = Vector128.Create(0x10325476u);

        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[0] + Vector128.Create(0xD76AA478u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[1] + Vector128.Create(0xE8C7B756u), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[2] + Vector128.Create(0x242070DBu), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[3] + Vector128.Create(0xC1BDCEEEu), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + m[4] + Vector128.Create(0xF57C0FAFu), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + m[5] + Vector128.Create(0x4787C62Au), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + m[6] + Vector128.Create(0xA8304613u), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + m[7] + Vector128.Create(0xFD469501u), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + Vector128.Create(0x69809958u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + Vector128.Create(0x8B44F7AFu), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + Vector128.Create(0xFFFF5BB1u), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + Vector128.Create(0x895CD7BEu), 22);
        a = b + Rol(a + (d ^ (b & (c ^ d))) + Vector128.Create(0x6B901122u), 7);
        d = a + Rol(d + (c ^ (a & (b ^ c))) + Vector128.Create(0xFD987193u), 12);
        c = d + Rol(c + (b ^ (d & (a ^ b))) + Vector128.Create(0xA679448Eu), 17);
        b = c + Rol(b + (a ^ (c & (d ^ a))) + Vector128.Create(0x49B40821u), 22);

        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[1] + Vector128.Create(0xF61E2562u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[6] + Vector128.Create(0xC040B340u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + Vector128.Create(0x265E5A51u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[0] + Vector128.Create(0xE9B6C7AAu), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + m[5] + Vector128.Create(0xD62F105Du), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + Vector128.Create(0x02441453u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + Vector128.Create(0xD8A1E681u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + m[4] + Vector128.Create(0xE7D3FBC8u), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + Vector128.Create(0x21E1CDE6u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + Vector128.Create(0xC33708D6u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[3] + Vector128.Create(0xF4D50D87u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + Vector128.Create(0x455A156Du), 20);
        a = b + Rol(a + (c ^ (d & (b ^ c))) + Vector128.Create(0xA9E3E905u), 5);
        d = a + Rol(d + (b ^ (c & (a ^ b))) + m[2] + Vector128.Create(0xFCEFA3F8u), 9);
        c = d + Rol(c + (a ^ (b & (d ^ a))) + m[7] + Vector128.Create(0x676F02D9u), 14);
        b = c + Rol(b + (d ^ (a & (c ^ d))) + Vector128.Create(0x8D2A4C8Au), 20);

        a = b + Rol(a + (b ^ c ^ d) + m[5] + Vector128.Create(0xFFFA3942u), 4);
        d = a + Rol(d + (a ^ b ^ c) + Vector128.Create(0x8771F701u), 11);
        c = d + Rol(c + (d ^ a ^ b) + Vector128.Create(0x6D9D6122u), 16);
        b = c + Rol(b + (c ^ d ^ a) + Vector128.Create(0xFDE5390Cu), 23);
        a = b + Rol(a + (b ^ c ^ d) + m[1] + Vector128.Create(0xA4BEEA44u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[4] + Vector128.Create(0x4BDECFA9u), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[7] + Vector128.Create(0xF6BB4B60u), 16);
        b = c + Rol(b + (c ^ d ^ a) + Vector128.Create(0xBEBFBC70u), 23);
        a = b + Rol(a + (b ^ c ^ d) + Vector128.Create(0x289B7EC6u), 4);
        d = a + Rol(d + (a ^ b ^ c) + m[0] + Vector128.Create(0xEAA127FAu), 11);
        c = d + Rol(c + (d ^ a ^ b) + m[3] + Vector128.Create(0xD4EF3085u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[6] + Vector128.Create(0x04881D05u), 23);
        a = b + Rol(a + (b ^ c ^ d) + Vector128.Create(0xD9D4D039u), 4);
        d = a + Rol(d + (a ^ b ^ c) + Vector128.Create(0xE6DB99E5u), 11);
        c = d + Rol(c + (d ^ a ^ b) + Vector128.Create(0x1FA27CF8u), 16);
        b = c + Rol(b + (c ^ d ^ a) + m[2] + Vector128.Create(0xC4AC5665u), 23);

        a = b + Rol(a + (c ^ (b | ~d)) + m[0] + Vector128.Create(0xF4292244u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[7] + Vector128.Create(0x432AFF97u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + Vector128.Create(0xAB9424A7u), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[5] + Vector128.Create(0xFC93A039u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + Vector128.Create(0x655B59C3u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + m[3] + Vector128.Create(0x8F0CCC92u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + Vector128.Create(0xFFEFF47Du), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + m[1] + Vector128.Create(0x85845DD1u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + Vector128.Create(0x6FA87ECFu), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + Vector128.Create(0xFE2CE6E0u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[6] + Vector128.Create(0xA3014314u), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + Vector128.Create(0x4E0811A1u), 21);
        a = b + Rol(a + (c ^ (b | ~d)) + m[4] + Vector128.Create(0xF7537E82u), 6);
        d = a + Rol(d + (b ^ (a | ~c)) + Vector128.Create(0xBD3AF235u), 10);
        c = d + Rol(c + (a ^ (d | ~b)) + m[2] + Vector128.Create(0x2AD7D2BBu), 15);
        b = c + Rol(b + (d ^ (c | ~a)) + Vector128.Create(0xEB86D391u), 21);

        aa = a + Vector128.Create(0x67452301u);
        bb = b + Vector128.Create(0xEFCDAB89u);
        cc = c + Vector128.Create(0x98BADCFEu);
        dd = d + Vector128.Create(0x10325476u);
    }
}