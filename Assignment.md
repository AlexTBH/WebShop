# G + VG
- [ ] Använd mallen Blazor Web App.
- [ ] Produkter
    - [ ] Innehåller information som: ID, Namn, Beskrivning, Bild-URL, Pris.
    - [ ] Minst 10 produkter.
    - [ ] En REST-API ska användas för att få åtkomst till produkterna.
- [ ] Startsida
    - [ ] Startsida (page) med en lista av produkter (komponenter).
    - [ ] Produkterna visas upp med hjälp av Razor-komponenter på startsidan.
- [ ] Produktkomponent
    - [ ] Komponenten ska inte visa all information om produkten, utan endast en överblick.
    - [ ] Komponenten ska innehålla en knapp som lägger till produkten i en varukorg.
    - [ ] Om man klickar på produkt-komponenten ska man komma till en produktsida (page) där all information om den specika produkten visas.
- [ ] Produktsida
    - [ ] På produktsidan ska det också vara möjligt att lägga till produkten i varukorgen.
    - [ ] Man ska kunna navigera till produktsidan via sökfältet i webbläsaren (t.ex. `localhost/product/1`).
- [ ] Varukorg
    - [ ] Sidan ska visa vad som finns i varukorgen.
    - [ ] Man ska kunna navigera till varukorgen (page) via en knapp/länk.
    - [ ] Varukorgen ska innehålla ett formulär för att fylla i adressuppgifter.
    - [ ] När formuläret skickas in ska användaren komma till en bekräftelsesida (page) där informationen om beställningen visas: Vilka produkter som köpts. Namn och adress från formuläret. När beställningen är klar ska varukorgen tömmas.
- HTML/CSS/Blazor
    - [ ] Minst 2 komponenter (inte pages/layout) ska användas.
    - [ ] HTML ska användas på rätt sätt och valideras.
    - [ ] Semantiska element när möjligt.
    - [ ] CSS ska vara tydligt strukturerad och bidra till UI/UX.
    - [ ] Appen ska vara responsiv och anpassad till mobile och desktop.
    - [ ] Inget CSS ramverk (bootstrap, tailwind etc.) får användas.

# G, men inte VG
- Räcker med ETT projekt.
- All data i G-delen *får* vara statisk (hårdkodad).
- [ ] Varukorgen ska lagras i `localStorage` (tillåtet även i VG, men ej krav).

# VG
- [ ] ... G-kriterierna ska uppfyllas.
- Blazor
    - [ ] Minst 4 komponenter (inte pages/layout) ska designas och användas.
- Produkter
    - [ ] Lagras i databas (ej valutakursen).
    - [ ] Produkter ska ha kvantiteter. De ska kunna bli slutsålda.
    - [ ] Produkter ska kunna vara på rea.
- Produktkomponent
    - [ ] Ändra utseende om...
        - [ ] varan är slut.
        - [ ] varan är på rea.
- På produktsidan ska produktens pris kunna visas i olika valutor med hjälp av API:et https://api-ninjas.com/api/exchangerate.
    - [ ] API:et måste användas på ett sätt som gör att slutanvändaren inte kan komma åt API-nyckeln.
- Konton
    - [ ] Kontoregistrering användarnamn och lösenord.
    - [ ] Inloggning med användarnamn och lösenord.
    - [ ] Sidan ska komma ihåg vad en användare har lagt i sin varukorg när den loggar in nästa gång.
    - [ ] Köp kan endast slutföras som inloggad.
- Struktur
    - [ ] Delade klasser ska kunna användas från både Frontend och Backend.
    - [ ] Dela upp din app i 3 projekt, exempelvis:
        - Frontend
            - UI, sidor, komponenter.
        - Backend
            - API-endpoints och affärslogik.
        - Shared
            - Ett separat bibliotek för exempelvis delade modeller, DTO:er och gemensam valideringslogik.
- [ ] Dokumentera och demonstrera hur du använt .NET Cores felsökningsverktyg och loggningssystem för att identifiera, analysera och åtgärda minst en specifik bugg i applikationen, inklusive en kort reflektion över bugghanteringsprocessen. 
- [ ] Dokumentera i filen `Analysis.md` i projektets rot (jämte `.sln`-filen).