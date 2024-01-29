using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recordList;

    private void Awake()
    {
        try
        {
            string result = "";
            int count = 1;
            List<Binary.Record> records = Binary.ReadBinaryFile(Binary.filename);
            Binary.Record.Sort(ref records);
            foreach (Binary.Record record in records)
            {
                result += $"{count}. {record.Score}\n";
                count++;
                if (count == 11)
                {
                    break;
                }
            }
            if (result.Length > 0)
            {
                recordList.text = result;
            }
            else
            {
                recordList.text = "пейнпдш нрясрярбсчр!\nяшцпюире ябнч оепбсч хцпс!";
            }
        }
        catch(Exception e)
        {
            recordList.text = e.Message;
        }
    }
}
