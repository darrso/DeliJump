using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieSceneController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statistic;
    [SerializeField] Animator BlackScreenAnimator;
    private void Awake()
    {
        BlackScreenAnimator.SetTrigger("Hide");
        List<Binary.Record> records = Binary.ReadBinaryFile(Binary.filename);
        Binary.Record record = records[records.Count - 1];
        Binary.Record.Sort(ref records);
        if (records.Count == 1 || records[1].Score < record.Score)
        {
            statistic.text = $"ÌÎÍÑÒÐÛ: {record.MosterKills}\n" +
                $"ÌÅÒÐÛ: {record.Meters}\n" +
                $"\n\nÐÅÇÓËÜÒÀÒ: {record.Score}" +
                $"\nÍÎÂÛÉ ÐÅÊÎÐÄ!";
        }
        else
        {
            statistic.text = $"ÌÎÍÑÒÐÛ: {record.MosterKills}\n" +
                $"ÌÅÒÐÛ: {record.Meters}\n" +
                $"\n\nÐÅÇÓËÜÒÀÒ: {record.Score}";
        }
    }
}
