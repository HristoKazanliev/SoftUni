function solve(array) {
    return array.sort((a, b) => a.length - b.length || a.localeCompare(b)).join(`\n`);
}

console.log(solve(
    ['alpha',
    'beta',
    'gamma']));

console.log(solve(
    ['Isacc',
    'Theodor',
    'Jack',
    'Harrison',
    'George']));    
