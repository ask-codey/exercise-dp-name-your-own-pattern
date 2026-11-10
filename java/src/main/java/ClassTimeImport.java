// The timetable team's copy of the copy.
class ClassTimeImport {
    static ImportResult<ClassTime> run(String text) {
        ImportResult<ClassTime> times = new ImportResult<>();
        String[] rows = text.split("\n", -1);
        for (int index = 1; index < rows.length; index++) {
            String row = rows[index].strip();
            if (row.length() == 0) { continue; }
            String[] cells = row.split(",", -1);
            if (cells.length != 2) {
                times.problems.add("line " + (index + 1) + ": expected 2 values, found " + cells.length);
                continue;
            }
            times.rows.add(new ClassTime(cells[0].strip(), cells[1].strip()));
        }
        return times;
    }
}
