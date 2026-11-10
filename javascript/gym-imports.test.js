// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { importClassTimes, importInstructors, importMembers, importRows } from "./gym-imports.js";

// What the three importers do today. These pass before you change anything: keep them passing.

test("members are imported", () => {
  const result = importMembers("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
  assert.deepEqual(result.rows, [{ name: "Maya", plan: "basic" }, { name: "Sam", plan: "unlimited" }, { name: "Leo", plan: "basic" }], "Keep this working: the members file has Maya (basic), Sam (unlimited) and Leo (basic); the header and the blank line are skipped.");
  assert.deepEqual(result.problems, [], "Keep this working: the members file has no bad rows.");
});

test("an instructor row with a missing value is reported", () => {
  const result = importInstructors("name,class\nPriya,yoga\nOmar\n");
  assert.deepEqual(result.rows, [{ name: "Priya", gymClass: "yoga" }], "Keep this working: Priya (yoga) is imported.");
  assert.deepEqual(result.problems, ["line 3: expected 2 values, found 1"], "Keep this working: Omar's row is missing his class, on line 3.");
});

test("class times skip the header and blank lines", () => {
  const result = importClassTimes("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
  assert.deepEqual(result.rows, [{ gymClass: "yoga", time: "07:00" }, { gymClass: "spin", time: "12:00" }, { gymClass: "HIIT", time: "18:00" }], "Keep this working: three class times, yoga, spin and HIIT.");
});

test("a member row with too many values is reported", () => {
  const result = importMembers("name,plan\nMaya,basic\nLeo,basic,extra");
  assert.equal(result.rows.length, 1, "Keep this working: only Maya's row is imported.");
  assert.deepEqual(result.problems, ["line 3: expected 2 values, found 3"], "Keep this working: Leo's row has 3 values, on line 3.");
});

// The ticket's new requirements. These fail until the work is done.

test("the equipment team can import with the shared shape", () => {
  const result = importRows("item,room\nmats, studio A\n\nbikes,studio B", 2, (row) => ({ item: row[0], room: row[1] }));
  assert.deepEqual(result.rows, [{ item: "mats", room: "studio A" }, { item: "bikes", room: "studio B" }], "Not yet: the equipment file has mats (studio A) and bikes (studio B); the header and the blank line are skipped and the spaces trimmed.");
});

test("the shared shape reports bad rows", () => {
  const result = importRows("item,room\nmats,studio A\nkettlebells", 2, (row) => ({ item: row[0], room: row[1] }));
  assert.equal(result.rows.length, 1, "Not yet: only the mats row is imported.");
  assert.deepEqual(result.problems, ["line 3: expected 2 values, found 1"], "Not yet: the kettlebells row is missing its room, on line 3.");
});

test("the shared shape works for any number of columns", () => {
  const result = importRows("item,room,count\nmats,studio A,20", 3, (row) => `${row[2]} ${row[0]} in ${row[1]}`);
  assert.deepEqual(result.rows, ["20 mats in studio A"], 'Not yet: a three-column file imports "20 mats in studio A".');
});
