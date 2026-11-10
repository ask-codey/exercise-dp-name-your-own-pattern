import java.util.function.Function;

class Rows {
    // The equipment team's code already calls this: it hands in the file's text, how many values each row
    // should have, and a function that turns one row's values into what they need. It does nothing yet.
    static <T> ImportResult<T> importRows(String text, int columns, Function<String[], T> makeRow) {
        return new ImportResult<>();
    }
}
