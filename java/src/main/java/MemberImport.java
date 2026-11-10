// The front desk's importer, written first.
class MemberImport {
    static ImportResult<Member> run(String text) {
        ImportResult<Member> result = new ImportResult<>();
        String[] lines = text.split("\n", -1);
        for (int i = 1; i < lines.length; i++) {
            String line = lines[i].strip();
            if (line.isEmpty()) { continue; }
            String[] parts = line.split(",", -1);
            if (parts.length != 2) {
                result.problems.add("line " + (i + 1) + ": expected 2 values, found " + parts.length);
                continue;
            }
            result.rows.add(new Member(parts[0].strip(), parts[1].strip()));
        }
        return result;
    }
}
