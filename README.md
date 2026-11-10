# Exercise: Name Your Own Pattern

Exercise code for the lesson **Exercise: Name Your Own Pattern** in the course *Design Patterns, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the front-desk team lead, Summit Gym
>
> 1. **The equipment team wants to import their spreadsheet too.** We already have three importers, for members, instructors and class times, and each is a copy of the one before, a little different every time. Before anyone makes a fourth copy: the equipment team has written their side already. They call `importRows(text, columns, makeRow)` with the file's text, how many values each row should have, and a function that turns one row's values into an `Equipment`. Make that work exactly the way our three importers do: skip the header line and blank lines, trim the spaces off each value, and report a row with the wrong number of values as `line 3: expected 2 values, found 1`, counting the header as line 1.
> 2. **One fix, one place.** The three importers we have should use the same code, so the next fix is made once.
> 3. **Write it down.** Fill in the pattern card under *Our pattern card*, below, so the next team that loads a spreadsheet finds it, and knows when to use it and when not to.
>
> The three importers have to keep working exactly as they do.

`importRows` is already there, because the equipment team's code calls it, but it doesn't do anything yet.

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | The importers | The tests |
|---|---|---|
| `csharp/` | `GymImports/Imports.cs` | `GymImports.Tests/ImportTests.cs` |
| `java/` | `src/main/java/` (`MemberImport.java`, `InstructorImport.java`, `ClassTimeImport.java`, `Rows.java` and the records) | `src/test/java/ImportTest.java` |
| `python/` | `gym_imports.py` | `test_gym_imports.py` |
| `javascript/` | `gym-imports.js` | `gym-imports.test.js` |
| `typescript/` | `gym-imports.ts` | `gym-imports.test.ts` |

The names follow each language's style: `Rows.Import` and `MemberImport.Run` in C#, `Rows.importRows` and `MemberImport.run` in Java, `import_rows` and `import_members` in Python, and `importRows` and `importMembers` in JavaScript and TypeScript.

Every folder also has a small demo program that imports one spreadsheet from each team, including the equipment team's. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project GymImports
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first, 4 tests pass and 3 fail. The 4 that pass are what the three importers do today: keep them green the whole time, and run the tests after every small change. The 3 that fail are the ticket. A failing test's message starts with **Not yet:** (or **Keep this working:** if you broke something that worked) and says what it expected.

## When you're done

All 7 tests are green, the card below is filled in, and the demo prints:

```text
Members: Maya (basic), Sam (unlimited), Leo (basic)
Instructors: Priya teaches yoga, Omar teaches spin
Class times: yoga at 07:00, spin at 12:00, HIIT at 18:00
Equipment: mats in studio A, bikes in studio B
Equipment problems: line 4: expected 2 values, found 1
```

## Our pattern card

Fill this in once the shared code works. Keep each answer to a sentence or two: a teammate should be able to read the whole card in two minutes.

| Field | Your answer |
|---|---|
| Name | |
| The problem | |
| Smells that point here | |
| The idea | |
| Example | |
| Use it when | |
| Skip it when | |
| Where it's used | |
| Look-alikes | |
| Owner | |

<details>
<summary>Stuck?</summary>

- **Find what's the same.** Put the three importers side by side. Which steps does every copy do? Which one line is really different in each? (*When Repetition Becomes a Pattern: The Rule of Three*.)
- **Write the shared steps once, and hand in the part that differs.** `importRows` gets the text, the number of values a row should have, and a function that builds one row. That's the same routine with your own step handed in: the idea behind **Template Method**, with the step passed as a function, the way Pass 1's weekly deal was. (*Template Method: Same Routine, Your Own Step*.)
- **Then make each importer one line** that calls `importRows`, and run the tests after each one.
- **For the card,** the course's own pattern cards are the model, and *Skip it when* matters most: what kind of spreadsheet would this code read wrongly? (*Writing a Pattern Card Your Team Will Read*.)

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
