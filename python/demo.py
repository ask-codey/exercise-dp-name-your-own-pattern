"""A demo: imports a spreadsheet from each team. You don't need to change it."""
from gym_imports import Equipment, import_class_times, import_instructors, import_members, import_rows


def show(items, describe):
    return ", ".join(describe(item) for item in items) or "none"


members = import_members("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic")
print("Members: " + show(members.rows, lambda member: f"{member.name} ({member.plan})"))
instructors = import_instructors("name,class\nPriya,yoga\nOmar, spin")
print("Instructors: " + show(instructors.rows, lambda instructor: f"{instructor.name} teaches {instructor.gym_class}"))
times = import_class_times("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00")
print("Class times: " + show(times.rows, lambda time: f"{time.gym_class} at {time.time}"))

# The equipment team's importer: a few lines, using the shared shape.
equipment = import_rows("item,room\nmats,studio A\nbikes,studio B\nkettlebells", 2, lambda row: Equipment(row[0], row[1]))
print("Equipment: " + show(equipment.rows, lambda item: f"{item.item} in {item.room}"))
print("Equipment problems: " + show(equipment.problems, lambda problem: problem))
