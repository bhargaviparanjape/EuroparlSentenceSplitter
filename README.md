# EuroparlSentenceSplitter
C# Implementation of rule-based Sentence splitting written originally by Philipp Koehn and Josh Schroeder in perl

### Usage
Import as C# project
Replace test.txt contents with text to be split
In bin/Debug,
SentenceSplitter.exe [en|de|...] < textfile > < full_path_to_splitfile >

### Nonbreaking Prefixes Directory
Nonbreaking prefixes are loosely defined as any word ending in a
period that does NOT indicate an end of sentence marker. A basic
example is Mr. and Ms. in English. Currently supports English,
French and Spanish.

