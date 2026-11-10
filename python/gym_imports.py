"""Summit Gym's spreadsheet importers."""
from dataclasses import dataclass, field


@dataclass
class ImportResult:
    """What an import hands back: the rows it read, and a message for each line it couldn't."""
    rows: list = field(default_factory=list)
    problems: list = field(default_factory=list)


@dataclass
class Member:
    name: str
    plan: str


@dataclass
class Instructor:
    name: str
    gym_class: str


@dataclass
class ClassTime:
    gym_class: str
    time: str


@dataclass
class Equipment:
    item: str
    room: str


def import_members(text):
    """The front desk's importer, written first."""
    result = ImportResult()
    lines = text.split("\n")
    for i in range(1, len(lines)):
        line = lines[i].strip()
        if line == "":
            continue
        parts = line.split(",")
        if len(parts) != 2:
            result.problems.append(f"line {i + 1}: expected 2 values, found {len(parts)}")
            continue
        result.rows.append(Member(parts[0].strip(), parts[1].strip()))
    return result


def import_instructors(text):
    """The schedule team's copy, a few weeks later."""
    imported = ImportResult()
    line_number = 1
    for raw in text.split("\n")[1:]:
        line_number += 1
        if not raw.strip():
            continue
        values = raw.strip().split(",")
        if len(values) != 2:
            imported.problems.append("line " + str(line_number) + ": expected 2 values, found " + str(len(values)))
            continue
        imported.rows.append(Instructor(values[0].strip(), values[1].strip()))
    return imported


def import_class_times(text):
    """The timetable team's copy of the copy."""
    times = ImportResult()
    for index, row in enumerate(text.split("\n")):
        if index == 0 or len(row.strip()) == 0:
            continue
        cells = row.strip().split(",")
        if len(cells) != 2:
            times.problems.append(f"line {index + 1}: expected 2 values, found {len(cells)}")
            continue
        times.rows.append(ClassTime(cells[0].strip(), cells[1].strip()))
    return times


def import_rows(text, columns, make_row):
    """The equipment team's code already calls this: it hands in the file's text, how many values each row
    should have, and a function that turns one row's values into what they need. It does nothing yet."""
    return ImportResult()
