using System;

namespace SoftEtherApi.Infrastructure
{
    public class SHA0
    {
        private readonly Sha0Context _context;

        public SHA0()
        {
            _context = new Sha0Context
            {
                H =
                {
                    [0] = 0x67452301,
                    [1] = 0xefcdab89,
                    [2] = 0x98badcfe,
                    [3] = 0x10325476,
                    [4] = 0xc3d2e1f0
                },
                Length = 0
            };
        }

        public SHA0 Update(byte[] data)
        {
            var dataLength = data.Length;

            if (dataLength >= 64 - _context.Length % 64)
            {
                Array.Copy(data, 0, _context.Buffer, _context.Length % 64, 64 - _context.Length % 64);

                var dataStartIndex = 64 - _context.Length % 64;
                dataLength -= 64 - _context.Length % 64;

                Transform(_context, _context.Buffer, 0, 1);
                Transform(_context, data, dataStartIndex, dataLength / 64);

                dataStartIndex += dataLength & ~63;
                dataLength %= 64;

                Array.Copy(data, dataStartIndex, _context.Buffer, 0, dataLength);
            }
            else
            {
                Array.Copy(data, 0, _context.Buffer, _context.Length % 64, dataLength);
            }

            _context.Length += data.Length;

            return this;
        }

        public byte[] Digest()
        {
            var digest = new byte[20];

            var tmpContext = new Sha0Context();

            Array.Copy(_context.H, tmpContext.H, tmpContext.H.Length);
            Array.Copy(_context.Buffer, tmpContext.Buffer, _context.Length % 64);

            tmpContext.Buffer[_context.Length % 64] = 0x80;

            if (_context.Length % 64 < 56)
            {
                Array.Clear(tmpContext.Buffer, _context.Length % 64 + 1, 55 - _context.Length % 64);
            }
            else
            {
                Array.Clear(tmpContext.Buffer, _context.Length % 64 + 1, 63 - _context.Length % 64);

                Transform(tmpContext, tmpContext.Buffer, 0, 1);
                Array.Clear(tmpContext.Buffer, 0, 56);
            }

            UNPACK_64_BE(_context.Length * 8, tmpContext.Buffer, 56);

            Transform(tmpContext, tmpContext.Buffer, 0, 1);

            UNPACK_32_BE(tmpContext.H[0], digest, 0);
            UNPACK_32_BE(tmpContext.H[1], digest, 4);
            UNPACK_32_BE(tmpContext.H[2], digest, 8);
            UNPACK_32_BE(tmpContext.H[3], digest, 12);
            UNPACK_32_BE(tmpContext.H[4], digest, 16);
            return digest;
        }

        private static void Transform(Sha0Context context, byte[] data, long dataStartIndex, long blocks)
        {
            for (var i = 0; i < blocks; ++i)
            {
                var wv = new uint[5];
                var w = new uint[16];

                PACK_32_BE(data, dataStartIndex + (i << 6), out w[0]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 4, out w[1]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 8, out w[2]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 12, out w[3]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 16, out w[4]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 20, out w[5]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 24, out w[6]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 28, out w[7]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 32, out w[8]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 36, out w[9]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 40, out w[10]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 44, out w[11]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 48, out w[12]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 52, out w[13]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 56, out w[14]);
                PACK_32_BE(data, dataStartIndex + (i << 6) + 60, out w[15]);

                wv[0] = context.H[0];
                wv[1] = context.H[1];
                wv[2] = context.H[2];
                wv[3] = context.H[3];
                wv[4] = context.H[4];

                SHA0_PRC(wv, 0, 1, 2, 3, 4, w[0], 1);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, w[1], 1);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, w[2], 1);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, w[3], 1);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, w[4], 1);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, w[5], 1);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, w[6], 1);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, w[7], 1);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, w[8], 1);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, w[9], 1);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, w[10], 1);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, w[11], 1);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, w[12], 1);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, w[13], 1);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, w[14], 1);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, w[15], 1);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 0), 1);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 1), 1);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 2), 1);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 3), 1);

                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 4), 2);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 5), 2);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 6), 2);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 7), 2);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 8), 2);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 9), 2);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 10), 2);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 11), 2);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 12), 2);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 13), 2);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 14), 2);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 15), 2);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 0), 2);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 1), 2);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 2), 2);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 3), 2);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 4), 2);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 5), 2);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 6), 2);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 7), 2);

                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 8), 3);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 9), 3);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 10), 3);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 11), 3);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 12), 3);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 13), 3);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 14), 3);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 15), 3);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 0), 3);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 1), 3);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 2), 3);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 3), 3);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 4), 3);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 5), 3);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 6), 3);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 7), 3);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 8), 3);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 9), 3);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 10), 3);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 11), 3);

                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 12), 4);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 13), 4);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 14), 4);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 15), 4);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 0), 4);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 1), 4);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 2), 4);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 3), 4);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 4), 4);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 5), 4);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 6), 4);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 7), 4);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 8), 4);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 9), 4);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 10), 4);
                SHA0_PRC(wv, 0, 1, 2, 3, 4, SHA0_EXT(w, 11), 4);
                SHA0_PRC(wv, 4, 0, 1, 2, 3, SHA0_EXT(w, 12), 4);
                SHA0_PRC(wv, 3, 4, 0, 1, 2, SHA0_EXT(w, 13), 4);
                SHA0_PRC(wv, 2, 3, 4, 0, 1, SHA0_EXT(w, 14), 4);
                SHA0_PRC(wv, 1, 2, 3, 4, 0, SHA0_EXT(w, 15), 4);

                context.H[0] += wv[0];
                context.H[1] += wv[1];
                context.H[2] += wv[2];
                context.H[3] += wv[3];
                context.H[4] += wv[4];
            }
        }

        private static uint SHA0_R1(uint x, uint y, uint z)
        {
            return (z ^ (x & (y ^ z))) + 0x5a827999;
        }

        private static uint SHA0_R2(uint x, uint y, uint z)
        {
            return (x ^ y ^ z) + 0x6ed9eba1;
        }

        private static uint SHA0_R3(uint x, uint y, uint z)
        {
            return ((x & y) | (z & (x | y))) + 0x8f1bbcdc;
        }

        private static uint SHA0_R4(uint x, uint y, uint z)
        {
            return (x ^ y ^ z) + 0xca62c1d6;
        }

        private static void SHA0_PRC(uint[] wv, uint a, uint b, uint c, uint d, uint e, uint idx, uint rnd)
        {
            uint val = 0;
            switch (rnd)
            {
                case 1:
                {
                    val = SHA0_R1(wv[b], wv[c], wv[d]);
                    break;
                }
                case 2:
                {
                    val = SHA0_R2(wv[b], wv[c], wv[d]);
                    break;
                }
                case 3:
                {
                    val = SHA0_R3(wv[b], wv[c], wv[d]);
                    break;
                }
                case 4:
                {
                    val = SHA0_R4(wv[b], wv[c], wv[d]);
                    break;
                }
            }


            wv[e] += ROR(wv[a], 27) + val + idx;
            wv[b] = ROR(wv[b], 2);
        }

        private static uint SHA0_EXT(uint[] w, uint i)
        {
            w[i] ^= w[(i - 3) & 0x0F] ^ w[(i - 8) & 0x0F] ^ w[(i - 14) & 0x0F];
            return w[i];
        }

        private static uint ROR(uint x, int y)
        {
            return (x >> y) ^ (x << (sizeof(uint) * 8 - y));
        }

        private static void PACK_32_BE(byte[] buf, long startIndex, out uint x)
        {
            x = (uint) ((buf[startIndex] << 24)
                        ^ (buf[startIndex + 1] << 16)
                        ^ (buf[startIndex + 2] << 8)
                        ^ buf[startIndex + 3]);
        }

        private static void UNPACK_32_BE(uint x, byte[] buf, long startIndex)
        {
            buf[startIndex] = (byte) (x >> 24);
            buf[startIndex + 1] = (byte) (x >> 16);
            buf[startIndex + 2] = (byte) (x >> 8);
            buf[startIndex + 3] = (byte) x;
        }

        private static void UNPACK_64_BE(long x, byte[] buf, long startIndex)
        {
            buf[startIndex] = (byte) (x >> 56);
            buf[startIndex + 1] = (byte) (x >> 48);
            buf[startIndex + 2] = (byte) (x >> 40);
            buf[startIndex + 3] = (byte) (x >> 32);
            buf[startIndex + 4] = (byte) (x >> 24);
            buf[startIndex + 5] = (byte) (x >> 16);
            buf[startIndex + 6] = (byte) (x >> 8);
            buf[startIndex + 7] = (byte) x;
        }

        private class Sha0Context
        {
            public readonly byte[] Buffer = new byte[64];
            public uint[] H = new uint[5];
            public int Length;
        }
    }
}