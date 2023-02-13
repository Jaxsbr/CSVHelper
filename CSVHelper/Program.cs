
using System.Text;

var running = true;
while (running)
{
    Console.WriteLine("Option 1 - Find and print a specific line number from your CSV file");
    Console.WriteLine("Option 2 - Specify a batch of lines from your current CSV file and save to a new CSV file");
    Console.WriteLine("Option 3 - Count the number of lines in a CSV file");
    Console.WriteLine("Option 4 - Detect the encoding type of a CSV files content");
    Console.WriteLine("Please choose: 1 | 2 | 3 | 4 and press enter:");
    var selection = Console.ReadLine();

    if (selection == "1")
    {
        FindAndPrintLine();
    }
    else if (selection == "2")
    {
        ExtractAndWriteBatchOfLines();
    }
    else if (selection == "3")
    {
        CountLines();
    }
    else if (selection == "4")
    {
        DetectFileEncoding();
    }
    else
    {
        Console.WriteLine("Nothing was selected");
    }

    Console.WriteLine("Try again? y | n");
    var repeat = Console.ReadLine();
    if (repeat == "y")
    {
        Console.Clear();
        continue;
    }
    running = false;
}

void FindAndPrintLine()
{
    Console.WriteLine("Provide the file path to your CSV:");
    var filePath = Console.ReadLine();

    Console.WriteLine("Provide the line number you'd like to search:");
    var lineNumberToPrint = Convert.ToInt64(Console.ReadLine());

    using (var reader = new StreamReader(filePath))
    {
        var currentLine = 1;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (currentLine == lineNumberToPrint)
            {
                Console.WriteLine("Line " + lineNumberToPrint + ": " + line);
                break;
            }

            currentLine++;
        }
    }
}

void ExtractAndWriteBatchOfLines()
{
    Console.WriteLine("Provide the file path to your CSV:");
    var filePath = Console.ReadLine();

    Console.WriteLine("Provide the file path to your new CSV:");
    var newFilePath = Console.ReadLine();

    if (filePath == newFilePath)
    {
        Console.WriteLine("The input and output file paths are equal, this is not allowed as the input would be overwritten");
        return;
    }

    Console.WriteLine("Provide the line number you'd like as a Starting line index:");
    var startLineNumber = Convert.ToInt64(Console.ReadLine());

    Console.WriteLine("Provide the number of lines to include:");
    var desiredLineCount = Convert.ToInt64(Console.ReadLine());

    var newFileContentLines = new List<string>();

    using (var reader = new StreamReader(filePath))
    {
        var currentLine = 1;
        var counter = 0;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var lastLineReached = currentLine == startLineNumber + desiredLineCount;

            if (currentLine >= startLineNumber || lastLineReached)
            {
                newFileContentLines.Add(line);
            }

            if (lastLineReached)
            {
                Console.WriteLine($"Gather lines: {desiredLineCount}");
                break;
            }

            currentLine++;
        }
    }

    File.WriteAllLines(newFilePath, newFileContentLines);
}

void CountLines()
{
    Console.WriteLine("Provide the file path to your CSV:");
    var filePath = Console.ReadLine();

    using (var reader = new StreamReader(filePath))
    {
        var counter = 1;

        while (!reader.EndOfStream)
        {
            reader.ReadLine();
            counter++;
        }

        Console.WriteLine($"Lines count: {counter}");
    }
}

void DetectFileEncoding()
{
    Console.WriteLine("Provide the file path to your CSV:");
    var filePath = Console.ReadLine();

    using (var reader = new StreamReader(filePath, Encoding.Default, true))
    {
        reader.Peek();
        Console.WriteLine($"Current encoding: {reader.CurrentEncoding}");
    }
}