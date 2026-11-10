using GymImports;

namespace GymImports.Tests;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

[TestClass]
public class ImportTests
{
    // What the three importers do today. These pass before you change anything: keep them passing.

    [TestMethod]
    public void MembersAreImported()
    {
        ImportResult<Member> result = MemberImport.Run("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
        CollectionAssert.AreEqual(new List<Member> { new Member("Maya", "basic"), new Member("Sam", "unlimited"), new Member("Leo", "basic") }, result.Rows, "Keep this working: the members file has Maya (basic), Sam (unlimited) and Leo (basic); the header and the blank line are skipped.");
        Assert.AreEqual(0, result.Problems.Count, "Keep this working: the members file has no bad rows.");
    }

    [TestMethod]
    public void AnInstructorRowWithAMissingValueIsReported()
    {
        ImportResult<Instructor> result = InstructorImport.Run("name,class\nPriya,yoga\nOmar\n");
        CollectionAssert.AreEqual(new List<Instructor> { new Instructor("Priya", "yoga") }, result.Rows, "Keep this working: Priya (yoga) is imported.");
        CollectionAssert.AreEqual(new List<string> { "line 3: expected 2 values, found 1" }, result.Problems, "Keep this working: Omar's row is missing his class, on line 3.");
    }

    [TestMethod]
    public void ClassTimesSkipTheHeaderAndBlankLines()
    {
        ImportResult<ClassTime> result = ClassTimeImport.Run("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
        CollectionAssert.AreEqual(new List<ClassTime> { new ClassTime("yoga", "07:00"), new ClassTime("spin", "12:00"), new ClassTime("HIIT", "18:00") }, result.Rows, "Keep this working: three class times, yoga, spin and HIIT.");
    }

    [TestMethod]
    public void AMemberRowWithTooManyValuesIsReported()
    {
        ImportResult<Member> result = MemberImport.Run("name,plan\nMaya,basic\nLeo,basic,extra");
        Assert.AreEqual(1, result.Rows.Count, "Keep this working: only Maya's row is imported.");
        CollectionAssert.AreEqual(new List<string> { "line 3: expected 2 values, found 3" }, result.Problems, "Keep this working: Leo's row has 3 values, on line 3.");
    }

    // The ticket's new requirements. These fail until the work is done.

    [TestMethod]
    public void TheEquipmentTeamCanImportWithTheSharedShape()
    {
        ImportResult<Equipment> result = Rows.Import("item,room\nmats, studio A\n\nbikes,studio B", 2, row => new Equipment(row[0], row[1]));
        CollectionAssert.AreEqual(new List<Equipment> { new Equipment("mats", "studio A"), new Equipment("bikes", "studio B") }, result.Rows, "Not yet: the equipment file has mats (studio A) and bikes (studio B); the header and the blank line are skipped and the spaces trimmed.");
    }

    [TestMethod]
    public void TheSharedShapeReportsBadRows()
    {
        ImportResult<Equipment> result = Rows.Import("item,room\nmats,studio A\nkettlebells", 2, row => new Equipment(row[0], row[1]));
        Assert.AreEqual(1, result.Rows.Count, "Not yet: only the mats row is imported.");
        CollectionAssert.AreEqual(new List<string> { "line 3: expected 2 values, found 1" }, result.Problems, "Not yet: the kettlebells row is missing its room, on line 3.");
    }

    [TestMethod]
    public void TheSharedShapeWorksForAnyNumberOfColumns()
    {
        ImportResult<string> result = Rows.Import("item,room,count\nmats,studio A,20", 3, row => $"{row[2]} {row[0]} in {row[1]}");
        CollectionAssert.AreEqual(new List<string> { "20 mats in studio A" }, result.Rows, "Not yet: a three-column file imports \"20 mats in studio A\".");
    }
}
