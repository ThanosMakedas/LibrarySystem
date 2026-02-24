# LibrarySystem – Del 1 (OOP, Arv/Komposition & Algoritmer)

## Beskrivning
Detta är ett konsolbaserat bibliotekssystem utvecklat i C#.
Projektet demonstrerar objektorienterade principer såsom inkapsling, komposition, interface och polymorfism samt grundläggande algoritmer.

## Funktionalitet
- Hantering av böcker, medlemmar och utlåning
- Sökning via ISearchable
- Sortering av böcker
- Statistik:
  - Totalt antal böcker
  - Antal utlånade böcker
  - Mest aktiva låntagare

## Designval
Projektet använder **komposition (Alternativ B)**:
- Library
  - BookCatalog
  - MemberRegistry
  - LoanManager

## Testning
Projektet innehåller xUnit-tester:

- BookTests
- LoanTests
- SearchTests
- LibraryStatisticsTests

Totalt: 14 tester

Alla tester körs med: dotnet test
och dotnet run --project LibrarySystem
