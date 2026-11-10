import java.util.Arrays;
import java.util.List;

// The schedule team's copy, a few weeks later.
class InstructorImport {
    static ImportResult<Instructor> run(String text) {
        ImportResult<Instructor> imported = new ImportResult<>();
        List<String> allLines = Arrays.asList(text.split("\n", -1));
        int lineNumber = 1;
        for (String raw : allLines.subList(1, allLines.size())) {
            lineNumber++;
            if (raw.isBlank()) { continue; }
            String[] values = raw.strip().split(",", -1);
            if (values.length != 2) {
                imported.problems.add("line " + lineNumber + ": expected 2 values, found " + values.length);
                continue;
            }
            imported.rows.add(new Instructor(values[0].strip(), values[1].strip()));
        }
        return imported;
    }
}
