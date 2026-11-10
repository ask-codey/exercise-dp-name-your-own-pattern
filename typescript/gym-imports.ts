// Summit Gym's spreadsheet importers.

// What an import hands back: the rows it read, and a message for each line it couldn't.
export interface ImportResult<T> {
  rows: T[];
  problems: string[];
}

export interface Member { name: string; plan: string; }
export interface Instructor { name: string; gymClass: string; }
export interface ClassTime { gymClass: string; time: string; }
export interface Equipment { item: string; room: string; }

// The front desk's importer, written first.
export function importMembers(text: string): ImportResult<Member> {
  const result: ImportResult<Member> = { rows: [], problems: [] };
  const lines = text.split("\n");
  for (let i = 1; i < lines.length; i++) {
    const line = lines[i].trim();
    if (line === "") { continue; }
    const parts = line.split(",");
    if (parts.length !== 2) {
      result.problems.push(`line ${i + 1}: expected 2 values, found ${parts.length}`);
      continue;
    }
    result.rows.push({ name: parts[0].trim(), plan: parts[1].trim() });
  }
  return result;
}

// The schedule team's copy, a few weeks later.
export function importInstructors(text: string): ImportResult<Instructor> {
  const imported: ImportResult<Instructor> = { rows: [], problems: [] };
  let lineNumber = 1;
  for (const raw of text.split("\n").slice(1)) {
    lineNumber++;
    if (!raw.trim()) { continue; }
    const values = raw.trim().split(",");
    if (values.length !== 2) {
      imported.problems.push("line " + lineNumber + ": expected 2 values, found " + values.length);
      continue;
    }
    imported.rows.push({ name: values[0].trim(), gymClass: values[1].trim() });
  }
  return imported;
}

// The timetable team's copy of the copy.
export function importClassTimes(text: string): ImportResult<ClassTime> {
  const times: ImportResult<ClassTime> = { rows: [], problems: [] };
  text.split("\n").forEach((row, index) => {
    if (index === 0 || row.trim().length === 0) { return; }
    const cells = row.trim().split(",");
    if (cells.length !== 2) {
      times.problems.push(`line ${index + 1}: expected 2 values, found ${cells.length}`);
      return;
    }
    times.rows.push({ gymClass: cells[0].trim(), time: cells[1].trim() });
  });
  return times;
}

// The equipment team's code already calls this: it hands in the file's text, how many values each row
// should have, and a function that turns one row's values into what they need. It does nothing yet.
export function importRows<T>(text: string, columns: number, makeRow: (row: string[]) => T): ImportResult<T> {
  return { rows: [], problems: [] };
}
