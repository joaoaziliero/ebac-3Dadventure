#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Metaprogramming : MonoBehaviour
{
    [SerializeField] protected string _filePathSpec;
    [SerializeField] protected string _startingPointReferenceLine;
    [SerializeField] protected string _endPointReferenceLine;

    [SerializeField] protected List<string> newestBlockOpeningLines;
    [SerializeField] protected List<string> newestBlockWindupLines;

    private List<string> allLines;

    private IEnumerable<string> LineByLineReading(string filePath)
    {
        using StreamReader dataStream = new(filePath);
        {
            while (!dataStream.EndOfStream)
            {
                yield return dataStream.ReadLine();
            }
        }
    }

    private void InsertBlockOfCode(string filePath, IEnumerable<string> newestBlockMainLines)
    {
        var i = 0;
        int? firstReferenceLineIndex = null;
        var rangeAddedAlready = false;

        foreach (var str in LineByLineReading(filePath))
        {
            allLines.Add(str);

            if (str.Contains(_startingPointReferenceLine))
            {
                firstReferenceLineIndex = i;
            }

            if (str.Contains(_endPointReferenceLine) && i > firstReferenceLineIndex && !rangeAddedAlready)
            {
                allLines.AddRange(newestBlockOpeningLines);
                allLines.AddRange(newestBlockMainLines);
                allLines.AddRange(newestBlockWindupLines);
                rangeAddedAlready = true;
            }

            allLines.TrimExcess();
            i++;
        }
    }

    private void SaveUpdatedCode(string filePath, List<string> allLines)
    {
        using StreamWriter dataStream = new(filePath, false);
        {
            foreach (var str in allLines)
            {
                dataStream.WriteLine(str);
            }
        }
    }

    protected void GenerateProgramWithNewCodeBlock(IEnumerable<string> newestBlockMainLines)
    {
        var filePath = Path.Combine(Application.dataPath, _filePathSpec);
        
        newestBlockOpeningLines ??= new List<string>();
        newestBlockWindupLines ??= new List<string>();
        allLines = new List<string>();
        
        InsertBlockOfCode(filePath, newestBlockMainLines);
        SaveUpdatedCode(filePath, allLines);
    }
}
#endif