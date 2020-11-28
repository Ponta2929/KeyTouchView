using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KeyTouchView.Utility.IO
{
    public static class BinaryAccessExtensions
    {
        // カスタム属性キャッシュ
        private static CustomAttributeCache cac = new CustomAttributeCache();

        #region Read / Write

        public static object TestRead(this BinaryReader br, Type type)
        {
            // 結果
            var result = Activator.CreateInstance(type);

            var prop = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var info in prop)
            {
                object value = null;

                if (info.PropertyType.IsArray)
                {
                    var attr = cac.GetCache(type, info);

                    if ((attr?.Count() ?? 0) > 0)
                    {
                        if (info.PropertyType == typeof(char[]))
                            value = br.ReadChars(attr[0].SizeOf);
                        else if (info.PropertyType == typeof(byte[]))
                            value = br.ReadBytes(attr[0].SizeOf);
                        else
                        {
                            var al = new ArrayList();
                            var et = info.PropertyType.GetElementType();

                            for (var i = 0; i < attr[0].SizeOf; i++)
                                al.Add(br.Read(et));

                            value = al.ToArray(et);
                        }
                    }
                }
                else if (info.PropertyType == typeof(bool))
                    value = br.ReadBoolean();
                else if (info.PropertyType == typeof(char))
                    value = br.ReadChar();
                else if (info.PropertyType == typeof(string))
                    value = br.ReadString();

                else if (info.PropertyType == typeof(byte))
                    value = br.ReadByte();
                else if (info.PropertyType == typeof(short))
                    value = br.ReadInt16();
                else if (info.PropertyType == typeof(int))
                    value = br.ReadInt32();
                else if (info.PropertyType == typeof(long))
                    value = br.ReadInt64();
                else if (info.PropertyType == typeof(float))
                    value = br.ReadSingle();

                else if (info.PropertyType == typeof(sbyte))
                    value = br.ReadSByte();
                else if (info.PropertyType == typeof(ushort))
                    value = br.ReadUInt16();
                else if (info.PropertyType == typeof(uint))
                    value = br.ReadUInt32();
                else if (info.PropertyType == typeof(ulong))
                    value = br.ReadUInt64();
                else if (info.PropertyType == typeof(double))
                    value = br.ReadDouble();
                else
                    value = br.Read(info.PropertyType);

                // セット
                info.SetValue(result, value, null);
            }

            return result;
        }

        /// <summary>
        /// ストリームをマネージドオブジェクトとして読み取ります。
        /// </summary>
        /// <param name="br"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Read(this BinaryReader br, Type type)
        {
            // 結果
            var result = Activator.CreateInstance(type);

            var prop = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var info in prop)
            {
                object value = null;

                if (info.PropertyType.IsArray)
                {
                    var attr = cac.GetCache(type, info);

                    if ((attr?.Count() ?? 0) > 0)
                    {
                        var size = 0;

                        switch (attr[0].SizeOfMember)
                        {
                            case string member:
                                var p = prop.Where((x) => { return x.Name == member; });
                                size = int.Parse(p.First().GetValue(result).ToString());
                                break;
                            default:
                                size = attr[0].SizeOf;
                                break;
                        }

                        if (info.PropertyType == typeof(char[]))
                            value = br.ReadChars(size);
                        else if (info.PropertyType == typeof(byte[]))
                            value = br.ReadBytes(size);
                        else
                        {
                            var al = new ArrayList();
                            var et = info.PropertyType.GetElementType();

                            for (var i = 0; i < size; i++)
                                al.Add(br.Read(et));

                            value = al.ToArray(et);
                        }
                    }
                }
                else if (info.PropertyType == typeof(bool))
                    value = br.ReadBoolean();
                else if (info.PropertyType == typeof(char))
                    value = br.ReadChar();
                else if (info.PropertyType == typeof(string))
                    value = br.ReadString();

                else if (info.PropertyType == typeof(byte))
                    value = br.ReadByte();
                else if (info.PropertyType == typeof(short))
                    value = br.ReadInt16();
                else if (info.PropertyType == typeof(int))
                    value = br.ReadInt32();
                else if (info.PropertyType == typeof(long))
                    value = br.ReadInt64();
                else if (info.PropertyType == typeof(float))
                    value = br.ReadSingle();

                else if (info.PropertyType == typeof(sbyte))
                    value = br.ReadSByte();
                else if (info.PropertyType == typeof(ushort))
                    value = br.ReadUInt16();
                else if (info.PropertyType == typeof(uint))
                    value = br.ReadUInt32();
                else if (info.PropertyType == typeof(ulong))
                    value = br.ReadUInt64();
                else if (info.PropertyType == typeof(double))
                    value = br.ReadDouble();
                else
                    value = br.Read(info.PropertyType);

                // セット
                info.SetValue(result, value, null);
            }

            return result;
        }

        public static object BRead(this BinaryReader br, Type type)
        {
            // 結果
            var result = Activator.CreateInstance(type);

            var prop = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var info in prop)
            {
                object value = null;

                if (info.PropertyType.IsArray)
                {
                    var attr = cac.GetCache(type, info);

                    if ((attr?.Count() ?? 0) > 0)
                    {
                        var size = 0;

                        switch (attr[0].SizeOfMember)
                        {
                            case string member:
                                var p = prop.Where((x) => { return x.Name == member; });
                                size = int.Parse(p.First().GetValue(result).ToString());
                                break;
                            default:
                                size = attr[0].SizeOf;
                                break;
                        }

                        if (info.PropertyType == typeof(char[]))
                            value = br.ReadChars(size);
                        else if (info.PropertyType == typeof(byte[]))
                            value = br.ReadBytes(size);
                        else
                        {
                            var al = new ArrayList();
                            var et = info.PropertyType.GetElementType();

                            for (var i = 0; i < size; i++)
                                al.Add(br.BRead(et));

                            value = al.ToArray(et);
                        }
                    }
                }
                else if (info.PropertyType == typeof(bool))
                    value = br.ReadBoolean();
                else if (info.PropertyType == typeof(char))
                    value = br.ReadChar();
                else if (info.PropertyType == typeof(string))
                    value = br.ReadString();

                else if (info.PropertyType == typeof(byte))
                    value = br.ReadByte();
                else if (info.PropertyType == typeof(short))
                    value = br.BReadInt16();
                else if (info.PropertyType == typeof(int))
                    value = br.BReadInt32();
                else if (info.PropertyType == typeof(long))
                    value = br.BReadInt64();
                else if (info.PropertyType == typeof(float))
                    value = br.BReadSingle();

                else if (info.PropertyType == typeof(sbyte))
                    value = br.ReadSByte();
                else if (info.PropertyType == typeof(ushort))
                    value = br.BReadUInt16();
                else if (info.PropertyType == typeof(uint))
                    value = br.BReadUInt32();
                else if (info.PropertyType == typeof(ulong))
                    value = br.BReadUInt64();
                else if (info.PropertyType == typeof(double))
                    value = br.BReadDouble();
                else
                    value = br.BRead(info.PropertyType);

                // セット
                info.SetValue(result, value, null);
            }

            return result;
        }

        public static void Write(this BinaryWriter bw, object data)
        {
            var dtype = data.GetType();
            var prop = dtype.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo info in prop)
            {
                var value = info.GetValue(data, null);

                if (info.PropertyType.IsArray)
                {
                    var attr = cac.GetCache(dtype, info);

                    if ((attr?.Count() ?? 0) > 0)
                    {
                        if (info.PropertyType == typeof(char[]))
                            bw.Write((char[])value);
                        else if (info.PropertyType == typeof(byte[]))
                            bw.Write((byte[])value);
                    }
                }
                else if (info.PropertyType == typeof(bool))
                    bw.Write((bool)value);
                else if (info.PropertyType == typeof(char))
                    bw.Write((char)value);
                else if (info.PropertyType == typeof(string))
                    bw.Write((string)value);

                else if (info.PropertyType == typeof(byte))
                    bw.Write((byte)value);
                else if (info.PropertyType == typeof(short))
                    bw.Write((short)value);
                else if (info.PropertyType == typeof(int))
                    bw.Write((int)value);
                else if (info.PropertyType == typeof(long))
                    bw.Write((long)value);
                else if (info.PropertyType == typeof(float))
                    bw.Write((float)value);

                else if (info.PropertyType == typeof(sbyte))
                    bw.Write((sbyte)value);
                else if (info.PropertyType == typeof(ushort))
                    bw.Write((ushort)value);
                else if (info.PropertyType == typeof(uint))
                    bw.Write((uint)value);
                else if (info.PropertyType == typeof(ulong))
                    bw.Write((ulong)value);
                else if (info.PropertyType == typeof(double))
                    bw.Write((double)value);
                else
                    bw.Write(value);
            }
        }

        #endregion

        #region Bigendian - Read

        public static byte[] BReadBytes(this BinaryReader br, int count)
        {
            var value = br.ReadBytes(count);

            Array.Reverse(value);

            return value;
        }
        public static short BReadInt16(this BinaryReader br)
        {
            var value = br.ReadBytes(2);

            Array.Reverse(value);

            return BitConverter.ToInt16(value, 0);
        }

        public static int BReadInt32(this BinaryReader br)
        {
            var value = br.ReadBytes(4);

            Array.Reverse(value);

            return BitConverter.ToInt32(value, 0);
        }

        public static long BReadInt64(this BinaryReader br)
        {
            var value = br.ReadBytes(8);

            Array.Reverse(value);

            return BitConverter.ToInt64(value, 0);
        }

        public static ushort BReadUInt16(this BinaryReader br)
        {
            var value = br.ReadBytes(2);

            Array.Reverse(value);

            return BitConverter.ToUInt16(value, 0);
        }

        public static uint BReadUInt32(this BinaryReader br)
        {
            var value = br.ReadBytes(4);

            Array.Reverse(value);

            return BitConverter.ToUInt32(value, 0);
        }

        public static ulong BReadUInt64(this BinaryReader br)
        {
            var value = br.ReadBytes(8);

            Array.Reverse(value);

            return BitConverter.ToUInt64(value, 0);
        }

        public static float BReadSingle(this BinaryReader br)
        {
            var value = br.ReadBytes(4);

            Array.Reverse(value);

            return BitConverter.ToSingle(value, 0);
        }

        public static double BReadDouble(this BinaryReader br)
        {
            var value = br.ReadBytes(8);

            Array.Reverse(value);

            return BitConverter.ToDouble(value, 0);
        }

        #endregion
    }
}