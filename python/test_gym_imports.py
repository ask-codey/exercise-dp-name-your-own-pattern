"""The tests for your code. You don't need to change this file.
Each message says what was expected, so a failing test tells you what to look at."""
from gym_imports import ClassTime, Equipment, Instructor, Member, import_class_times, import_instructors, import_members, import_rows


# What the three importers do today. These pass before you change anything: keep them passing.

def test_members_are_imported():
    result = import_members("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic")
    assert result.rows == [Member("Maya", "basic"), Member("Sam", "unlimited"), Member("Leo", "basic")], \
        "Keep this working: the members file has Maya (basic), Sam (unlimited) and Leo (basic); the header and the blank line are skipped."
    assert result.problems == [], "Keep this working: the members file has no bad rows."


def test_an_instructor_row_with_a_missing_value_is_reported():
    result = import_instructors("name,class\nPriya,yoga\nOmar\n")
    assert result.rows == [Instructor("Priya", "yoga")], "Keep this working: Priya (yoga) is imported."
    assert result.problems == ["line 3: expected 2 values, found 1"], "Keep this working: Omar's row is missing his class, on line 3."


def test_class_times_skip_the_header_and_blank_lines():
    result = import_class_times("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00")
    assert result.rows == [ClassTime("yoga", "07:00"), ClassTime("spin", "12:00"), ClassTime("HIIT", "18:00")], \
        "Keep this working: three class times, yoga, spin and HIIT."


def test_a_member_row_with_too_many_values_is_reported():
    result = import_members("name,plan\nMaya,basic\nLeo,basic,extra")
    assert len(result.rows) == 1, "Keep this working: only Maya's row is imported."
    assert result.problems == ["line 3: expected 2 values, found 3"], "Keep this working: Leo's row has 3 values, on line 3."


# The ticket's new requirements. These fail until the work is done.

def test_the_equipment_team_can_import_with_the_shared_shape():
    result = import_rows("item,room\nmats, studio A\n\nbikes,studio B", 2, lambda row: Equipment(row[0], row[1]))
    assert result.rows == [Equipment("mats", "studio A"), Equipment("bikes", "studio B")], \
        "Not yet: the equipment file has mats (studio A) and bikes (studio B); the header and the blank line are skipped and the spaces trimmed."


def test_the_shared_shape_reports_bad_rows():
    result = import_rows("item,room\nmats,studio A\nkettlebells", 2, lambda row: Equipment(row[0], row[1]))
    assert len(result.rows) == 1, "Not yet: only the mats row is imported."
    assert result.problems == ["line 3: expected 2 values, found 1"], "Not yet: the kettlebells row is missing its room, on line 3."


def test_the_shared_shape_works_for_any_number_of_columns():
    result = import_rows("item,room,count\nmats,studio A,20", 3, lambda row: f"{row[2]} {row[0]} in {row[1]}")
    assert result.rows == ["20 mats in studio A"], 'Not yet: a three-column file imports "20 mats in studio A".'
