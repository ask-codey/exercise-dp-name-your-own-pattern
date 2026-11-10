import java.util.ArrayList;
import java.util.List;

// What an import hands back: the rows it read, and a message for each line it couldn't.
class ImportResult<T> {
    final List<T> rows = new ArrayList<>();
    final List<String> problems = new ArrayList<>();
}
