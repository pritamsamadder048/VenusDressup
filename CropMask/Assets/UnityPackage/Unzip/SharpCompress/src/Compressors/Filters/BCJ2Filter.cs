﻿using System;
using System.IO;

namespace SharpCompress.Compressors.Filters
{
    internal class BCJ2Filter : Stream
    {
        private readonly Stream baseStream;
        private readonly byte[] input = new byte[4096];
        private int inputOffset;
        private int inputCount;
        private bool endReached;

        private long position;
        private readonly byte[] output = new byte[4];
        private int outputOffset;
        private int outputCount;

        private readonly byte[] control;
        private readonly byte[] data1;
        private readonly byte[] data2;

        private int controlPos;
        private int data1Pos;
        private int data2Pos;

        private readonly ushort[] p = new ushort[256 + 2];
        private uint range, code;
        private byte prevByte;
        private bool isDisposed;

        private const int kNumTopBits = 24;
        private const int kTopValue = 1 << kNumTopBits;

        private const int kNumBitModelTotalBits = 11;
        private const int kBitModelTotal = 1 << kNumBitModelTotalBits;
        private const int kNumMoveBits = 5;

        private static bool IsJ(byte b0, byte b1)
        {
            return (b1 & 0xFE) == 0xE8 || IsJcc(b0, b1);
        }

        private static bool IsJcc(byte b0, byte b1)
        {
            return b0 == 0x0F && (b1 & 0xF0) == 0x80;
        }

        public BCJ2Filter(byte[] control, byte[] data1, byte[] data2, Stream baseStream)
        {
            this.control = control;
            this.data1 = data1;
            this.data2 = data2;
            this.baseStream = baseStream;

            int i;
            for (i = 0; i < p.Length; i++)
            {
                p[i] = kBitModelTotal >> 1;
            }

            code = 0;
            range = 0xFFFFFFFF;
            for (i = 0; i < 5; i++)
            {
                code = (code << 8) | control[controlPos++];
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            base.Dispose(disposing);
            baseStream.Dispose();
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length => baseStream.Length + data1.Length + data2.Length;

        public override long Position { get { return position; } set { throw new NotSupportedException(); } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int size = 0;
            byte b = 0;

            while (!endReached && size < count)
            {
                while (outputOffset < outputCount)
                {
                    b = output[outputOffset++];
                    buffer[offset++] = b;
                    size++;
                    position++;

                    prevByte = b;
                    if (size == count)
                    {
                        return size;
                    }
                }

                if (inputOffset == inputCount)
                {
                    inputOffset = 0;
                    inputCount = baseStream.Read(input, 0, input.Length);
                    if (inputCount == 0)
                    {
                        endReached = true;
                        break;
                    }
                }

                b = input[inputOffset++];
                buffer[offset++] = b;
                size++;
                position++;

                if (!IsJ(prevByte, b))
                {
                    prevByte = b;
                }
                else
                {
                    int prob;
                    if (b == 0xE8)
                    {
                        prob = prevByte;
                    }
                    else if (b == 0xE9)
                    {
                        prob = 256;
                    }
                    else
                    {
                        prob = 257;
                    }

                    uint bound = (range >> kNumBitModelTotalBits) * p[prob];
                    if (code < bound)
                    {
                        range = bound;
                        p[prob] += (ushort)((kBitModelTotal - p[prob]) >> kNumMoveBits);
                        if (range < kTopValue)
                        {
                            range <<= 8;
                            code = (code << 8) | control[controlPos++];
                        }
                        prevByte = b;
                    }
                    else
                    {
                        range -= bound;
                        code -= bound;
                        p[prob] -= (ushort)(p[prob] >> kNumMoveBits);
                        if (range < kTopValue)
                        {
                            range <<= 8;
                            code = (code << 8) | control[controlPos++];
                        }

                        uint dest;
                        if (b == 0xE8)
                        {
                            dest =
                                (uint)
                                ((data1[data1Pos++] << 24) | (data1[data1Pos++] << 16) | (data1[data1Pos++] << 8) |
                                 data1[data1Pos++]);
                        }
                        else
                        {
                            dest =
                                (uint)
                                ((data2[data2Pos++] << 24) | (data2[data2Pos++] << 16) | (data2[data2Pos++] << 8) |
                                 data2[data2Pos++]);
                        }
                        dest -= (uint)(position + 4);

                        output[0] = (byte)dest;
                        output[1] = (byte)(dest >> 8);
                        output[2] = (byte)(dest >> 16);
                        output[3] = (byte)(dest >> 24);
                        outputOffset = 0;
                        outputCount = 4;
                    }
                }
            }

            return size;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}