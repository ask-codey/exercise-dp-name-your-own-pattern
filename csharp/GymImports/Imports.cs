namespace GymImports;

// What an import hands back: the rows it read, and a message for each line it couldn't.
public class ImportResult<T>
{
    public List<T> Rows { get; } = new List<T>();
    public List<string> Problems { get; } = new List<string>();
}

public record Member(string Name, string Plan);
public record Instructor(string Name, string GymClass);
public record ClassTime(string GymClass, string Time);
public record Equipment(string Item, string Room);

// The front desk's importer, written first.
public static class MemberImport
{
    public static ImportResult<Member> Run(string text)
    {
        ImportResult<Member> result = new ImportResult<Member>();
        string[] lines = text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line == "") { continue; }
            string[] parts = line.Split(',');
            if (parts.Length != 2)
            {
                result.Problems.Add($"line {i + 1}: expected 2 values, found {parts.Length}");
                continue;
            }
            result.Rows.Add(new Member(parts[0].Trim(), parts[1].Trim()));
        }
        return result;
    }
}

// The schedule team's copy, a few weeks later.
public static class InstructorImport
{
    public static ImportResult<Instructor> Run(string text)
    {
        ImportResult<Instructor> imported = new ImportResult<Instructor>();
        string[] allLines = text.Split('\n');
        int lineNumber = 1;
        foreach (string raw in allLines.Skip(1))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(raw)) { continue; }
            string[] values = raw.Trim().Split(',');
            if (values.Length != 2)
            {
                imported.Problems.Add("line " + lineNumber + ": expected 2 values, found " + values.Length);
                continue;
            }
            imported.Rows.Add(new Instructor(values[0].Trim(), values[1].Trim()));
        }
        return imported;
    }
}

// The timetable team's copy of the copy.
public static class ClassTimeImport
{
    public static ImportResult<ClassTime> Run(string text)
    {
        ImportResult<ClassTime> times = new ImportResult<ClassTime>();
        string[] rows = text.Split('\n');
        for (int index = 1; index < rows.Length; index++)
        {
            string row = rows[index].Trim();
            if (row.Length == 0) { continue; }
            string[] cells = row.Split(',');
            if (cells.Length != 2)
            {
                times.Problems.Add($"line {index + 1}: expected 2 values, found {cells.Length}");
                continue;
            }
            times.Rows.Add(new ClassTime(cells[0].Trim(), cells[1].Trim()));
        }
        return times;
    }
}

public static class Rows
{
    // The equipment team's code already calls this: it hands in the file's text, how many values each row
    // should have, and a function that turns one row's values into what they need. It does nothing yet.
    public static ImportResult<T> Import<T>(string text, int columns, Func<string[], T> makeRow)
    {
        return new ImportResult<T>();
    }
}
