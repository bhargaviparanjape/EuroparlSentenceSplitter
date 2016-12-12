# EuroparlSentenceSplitter
C# Implementation of rule-based Sentence splitting written originally by Philipp Koehn and Josh Schroeder in perl

### Usage
Import as C# project
SentenceSplitter.exe [en|de|...] < textfile > < splitfile >

### Nonbreaking Prefixes Directory
Nonbreaking prefixes are loosely defined as any word ending in a
period that does NOT indicate an end of sentence marker. A basic
example is Mr. and Ms. in English.
