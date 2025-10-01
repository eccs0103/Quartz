## Programming Language

Custom simplified programming language model.

[Change log](./CHANGELOG.md), [Grammar](./Grammar.ebnf)

#### Variables
```
var1 Number;
var2 Number(3);
var3 Boolean(true);
var4 String("string value");
```

#### Simple math
```
celsius Numebr(36.6);
fahrenheit Number(32 + celsius * 1.8);
write(fahrenheit);
```

#### Typed core
```
write(Type);
write(Function);
write(Number);
write(Boolean);
write(String);
```

#### If-else statements
```
var1 Number(10);
var2 Number(20);
max Number;
indication Number;

min Number(var1);
if (var2 < var1) {
	min: var2;
};
write(min);

if (var1 > var2) {
	max: var1;
} else { 
	max: var2;
};
write(max);

if (var1 > var2) {
	indication: -1;
} else if (var1 = var2) {
	indication: 0;
} else {
	indication: 1;
};
write(indication);
```
