// A demo: imports a spreadsheet from each team. You don't need to change it.
import { importClassTimes, importInstructors, importMembers, importRows, type Equipment } from "./gym-imports.ts";

const show = <T>(items: T[], describe: (item: T) => string): string => items.map(describe).join(", ") || "none";

const members = importMembers("name,plan\nMaya, basic\nSam,unlimited\n\nLeo,basic");
console.log("Members: " + show(members.rows, (member) => `${member.name} (${member.plan})`));
const instructors = importInstructors("name,class\nPriya,yoga\nOmar, spin");
console.log("Instructors: " + show(instructors.rows, (instructor) => `${instructor.name} teaches ${instructor.gymClass}`));
const times = importClassTimes("class,time\nyoga,07:00\n\nspin,12:00\nHIIT,18:00");
console.log("Class times: " + show(times.rows, (time) => `${time.gymClass} at ${time.time}`));

// The equipment team's importer: a few lines, using the shared shape.
const equipment = importRows("item,room\nmats,studio A\nbikes,studio B\nkettlebells", 2, (row): Equipment => ({ item: row[0], room: row[1] }));
console.log("Equipment: " + show(equipment.rows, (item) => `${item.item} in ${item.room}`));
console.log("Equipment problems: " + show(equipment.problems, (problem) => problem));
