#! /usr/bin/env lang
//#lang js

Console.WriteLine('{0} is an interesting number.', Math.PI);
Console.WriteLine(123.45);
Util.Print(123.45);
Util.Print({"a":123.45});
var a ={"a":123.45};

Console.WriteLine(JSON.stringify(peggy, null, 2));

var syntax = `
start = add

add = left:mul '+' right:add { return left + right; }
    / mul

mul = left:val '*' right:mul { return left * right; }
    / val

val = _ nums:[0-9]+ _ { return parseInt(nums.join('')); }

_ 'whitespace' = [\s ]*
`;

var result = parse(syntax, "1 + 2 * 3");
Util.Print(result);
if (result["!"] === "error") Util.Log("error(1) " + result["?"]);
result = parse(syntax, "1 + 2 * 3xxx");
Util.Print(result);
if (result["!"] === "error") Util.Log("error(2) " + result["?"]);

random = new clr.System.Random();
Console.WriteLine(random.NextDouble());

var n = 1234567890123456789012345n;
Util.Print(n);

Util.Print(args, "args");

Util.Print(1n + 2n);
Util.Print(Util.FullName(1n + 2n));
Util.Print([1n, 2n][0]);
Console.WriteLine(1n);

Util.Print(globalThis);
var m = miniMAL(globalThis);
Util.Print(m.eval(["+", 11, 22]));