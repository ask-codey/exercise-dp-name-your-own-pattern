import java.util.List;
import java.util.function.Function;

// A demo: imports a spreadsheet from each team. You don't need to change it.
// Run it with: java src/main/java/Demo.java
public class Demo {
    static <T> String show(List<T> items, Function<T, String> describe) {
        return items.isEmpty() ? "none" : String.join(", ", items.stream().map(describe).toList());
    }

    public static void main(String[] args) {
        ImportResult<Member> members = MemberImport.run("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
        System.out.println("Members: " + show(members.rows, member -> member.name() + " (" + member.plan() + ")"));
        ImportResult<Instructor> instructors = InstructorImport.run("name,class\nPriya,yoga\nOmar, spin");
        System.out.println("Instructors: " + show(instructors.rows, instructor -> instructor.name() + " teaches " + instructor.gymClass()));
        ImportResult<ClassTime> times = ClassTimeImport.run("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
        System.out.println("Class times: " + show(times.rows, time -> time.gymClass() + " at " + time.time()));

        // The equipment team's importer: a few lines, using the shared shape.
        ImportResult<Equipment> equipment = Rows.importRows("item,room\nmats,studio A\nbikes,studio B\nkettlebells", 2, row -> new Equipment(row[0], row[1]));
        System.out.println("Equipment: " + show(equipment.rows, item -> item.item() + " in " + item.room()));
        System.out.println("Equipment problems: " + show(equipment.problems, problem -> problem));
    }
}
