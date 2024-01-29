using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Binary 
{
    public static string filename = Path.Combine(Application.persistentDataPath, "data.dat");
    [Serializable]
    public class Record
    {
        public int Score { get; set;}
        public int MosterKills { get; set;}
        public int Meters { get; set;}
        public Record(int score, int mosterKills, int meters)
        {
            Score = score;
            MosterKills = mosterKills;
            Meters = meters;
        }
        public static void Sort(ref List<Record> records)
        {
            records.Sort((a,b) => -a.Score.CompareTo(b.Score));
        }
    }
    public static void ClearBinaryFile(string path_to_binary_file)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(path_to_binary_file, FileMode.Truncate)))
        {
        }
    }
    public static List<Record> ReadBinaryFile(string path_to_binary_file)
    {
        List<Record> dataList = new List<Record>();
        using (BinaryReader reader = new BinaryReader(File.Open(path_to_binary_file, FileMode.OpenOrCreate)))
        {
            while (true)
            {
                try
                {
                    int score = reader.ReadInt32();
                    int monsters = reader.ReadInt32();
                    int meters = reader.ReadInt32();
                    dataList.Add(new Record(score, monsters, meters));
                }
                catch (EndOfStreamException) { break; }
            }
        }
        return dataList;
    }
    public static void WriteBinaryFile(List<Record> dataList, string path_to_binary_file)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(path_to_binary_file, FileMode.OpenOrCreate)))
        {
            foreach (Record rec in dataList)
            {
                writer.Write(rec.Score);
                writer.Write(rec.MosterKills);
                writer.Write(rec.Meters);
            }
        }
    }
    public static void AppendBinaryFile(Record rec, string path_to_binary_file)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(path_to_binary_file, FileMode.Append)))
        {
            writer.Write(rec.Score);
            writer.Write(rec.MosterKills);
            writer.Write(rec.Meters);
        }
    }
}
