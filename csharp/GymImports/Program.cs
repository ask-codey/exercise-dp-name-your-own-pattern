using GymImports;

// A demo: imports a spreadsheet from each team. You don't need to change it.
string Show<T>(List<T> items, Func<T, string> describe) => items.Count > 0 ? string.Join(", ", items.Select(describe)) : "none";

ImportResult<Member> members = MemberImport.Run("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
Console.WriteLine("Members: " + Show(members.Rows, member => $"{member.Name} ({member.Plan})"));
ImportResult<Instructor> instructors = InstructorImport.Run("name,class\nPriya,yoga\nOmar, spin");
Console.WriteLine("Instructors: " + Show(instructors.Rows, instructor => $"{instructor.Name} teaches {instructor.GymClass}"));
ImportResult<ClassTime> times = ClassTimeImport.Run("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
Console.WriteLine("Class times: " + Show(times.Rows, time => $"{time.GymClass} at {time.Time}"));

// The equipment team's importer: a few lines, using the shared shape.
ImportResult<Equipment> equipment = Rows.Import("item,room\nmats,studio A\nbikes,studio B\nkettlebells", 2, row => new Equipment(row[0], row[1]));
Console.WriteLine("Equipment: " + Show(equipment.Rows, item => $"{item.Item} in {item.Room}"));
Console.WriteLine("Equipment problems: " + Show(equipment.Problems, problem => problem));
