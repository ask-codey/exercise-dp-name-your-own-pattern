import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.List;
import org.junit.jupiter.api.Test;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class ImportTest {
    // What the three importers do today. These pass before you change anything: keep them passing.

    @Test
    void membersAreImported() {
        ImportResult<Member> result = MemberImport.run("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
        assertEquals(List.of(new Member("Maya", "basic"), new Member("Sam", "unlimited"), new Member("Leo", "basic")), result.rows, "Keep this working: the members file has Maya (basic), Sam (unlimited) and Leo (basic); the header and the blank line are skipped.");
        assertEquals(0, result.problems.size(), "Keep this working: the members file has no bad rows.");
    }

    @Test
    void anInstructorRowWithAMissingValueIsReported() {
        ImportResult<Instructor> result = InstructorImport.run("name,class\nPriya,yoga\nOmar\n");
        assertEquals(List.of(new Instructor("Priya", "yoga")), result.rows, "Keep this working: Priya (yoga) is imported.");
        assertEquals(List.of("line 3: expected 2 values, found 1"), result.problems, "Keep this working: Omar's row is missing his class, on line 3.");
    }

    @Test
    void classTimesSkipTheHeaderAndBlankLines() {
        ImportResult<ClassTime> result = ClassTimeImport.run("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
        assertEquals(List.of(new ClassTime("yoga", "07:00"), new ClassTime("spin", "12:00"), new ClassTime("HIIT", "18:00")), result.rows, "Keep this working: three class times, yoga, spin and HIIT.");
    }

    @Test
    void aMemberRowWithTooManyValuesIsReported() {
        ImportResult<Member> result = MemberImport.run("name,plan\nMaya,basic\nLeo,basic,extra");
        assertEquals(1, result.rows.size(), "Keep this working: only Maya's row is imported.");
        assertEquals(List.of("line 3: expected 2 values, found 3"), result.problems, "Keep this working: Leo's row has 3 values, on line 3.");
    }

    // The ticket's new requirements. These fail until the work is done.

    @Test
    void theEquipmentTeamCanImportWithTheSharedShape() {
        ImportResult<Equipment> result = Rows.importRows("item,room\nmats, studio A\n\nbikes,studio B", 2, row -> new Equipment(row[0], row[1]));
        assertEquals(List.of(new Equipment("mats", "studio A"), new Equipment("bikes", "studio B")), result.rows, "Not yet: the equipment file has mats (studio A) and bikes (studio B); the header and the blank line are skipped and the spaces trimmed.");
    }

    @Test
    void theSharedShapeReportsBadRows() {
        ImportResult<Equipment> result = Rows.importRows("item,room\nmats,studio A\nkettlebells", 2, row -> new Equipment(row[0], row[1]));
        assertEquals(1, result.rows.size(), "Not yet: only the mats row is imported.");
        assertEquals(List.of("line 3: expected 2 values, found 1"), result.problems, "Not yet: the kettlebells row is missing its room, on line 3.");
    }

    @Test
    void theSharedShapeWorksForAnyNumberOfColumns() {
        ImportResult<String> result = Rows.importRows("item,room,count\nmats,studio A,20", 3, row -> row[2] + " " + row[0] + " in " + row[1]);
        assertEquals(List.of("20 mats in studio A"), result.rows, "Not yet: a three-column file imports \"20 mats in studio A\".");
    }
}
