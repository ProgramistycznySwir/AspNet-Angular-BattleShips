# Happy Team - BattleShips
## Opis przebiegu wykonywania projektu:
Na początku wybrałem techstack: ASP.NET + Angular + SQLite.
Z bazą postanowiłem łączyć się przy pomocy Entity Framework Code First, a komunikację serwer-klient postanowiłem zrealizować przy pomocy WebSocket'ów.
WebSockety są dla mnie nową technologią, więc musiałem najpierw się ich nauczyć.

Po wybraniu techstacku przyszedł czas na zarysowanie modeli potrzebnych w projekcie.
Dwa główne modele to Game i Player, pierwszy przechowuje informacje odnośnie przebiegu rozgrywki, oraz graczy w niej uczestniczących, zaś główną rolą Player'a jest przechowywanie 2 id, jednego prywatnego używanego do autoryzacji ruchów, oraz drugiego publicznego używanego jako właściwe id by nie ujawnić prywatnego id.
Po pewnym czasie odkryłem, że powinienem zwyczajnie publiczne id nazwać id, zaś id nazwać np. AuthToken.
Całe skomplikowanie tego podejścia wzięło się z tego, że sądziłem, że zrobienie autoryzacji in-house będzie prostsze niż integrowanie autoryzacji ASP.NET.

Postanowiłem także skorzystać z dependancy injection do wyparcia logiki z kontrolerów, jednak z racji na małą skalę projektu zdecydowałem by połączyć serwisy infrastruktury i domeny, by nie pisać zbyt dużo boilerplate'u (poza tym, żeby prawidłowo zrobić podział N-Tier'owy powinienem podzielić projekt na kilka .sln, czego nie chciało mi się robić w tak małym projekcie).

Id w modelach chciałem zrealizować przy pomocy StronglyTypedIds zgodnie z DDD, jednak w połowie robienia serwisów domeny natrafiłem na problemy jakie EF ma z typami stworzonymi przez programistę i musiałem zaniechać tego podejścia.


W środę zaczęło mi się mocno rzucać w oczy, że przeszacowałem siły nad zamiary, gdzie z powodu perfekcjonizmu dalej próbuję zrobić jak najlepszy back-end, zamiast skupić się nad tym by aplikacjia jakkolwiek działała.

Sądzę, że powinienem jednak dokonać większego rozdzielenia domeny, chociażby po to, że otrzymałbym za darmo wycięcie obiektów z modeli kiedy chciałbym je wysyłać jeśli bym skorzystał z AutoMapper'a.

TODO: Error checki są w projekcie zaimplementowane na odwal (serwer sprawdza wszystkie możliwe błędy, ale w razie wykrycia jakiegoś, nie pisze żadnych informacji odnośnie jaki błąd wykrył). Jeśli będę miał czas to postaram się to poprawić.

TODO: Zauważyłem, że w kodzie jednego serwisu piszę kod innego serwisu (aka. wyszukuje graczy w GameService, zamiast skorzystać z już gotowych metod serwisu PlayerService). Warto to skorygować, by kod był DRY.


W czwartek wraz ze zbliżaniem się terminu jaki zadeklarowałem, jakość kodu front-end'owego zaczęła tracić na jakości kiedy próbowałem sprawić by projekt w jakimkolwiek stanie działał zostawiając tylko po sobie komentarze z TODO.

Na chwilę obecną API jest w głównej mierze skończone. Front-end podobnie, choć pozostawia wiele do życzenia. Można przeprowadzić rozgrywkę, ale system nie komunikuje w żaden sposób wygranej, oraz nawet tego, że przeciwnik dokonał ruchu (nie udało mi się jeszcze zaimplementować WebSocket'ów które są niezbędne do tego).
Sama komunikacja z API REST'owym jest nie do zarzucenia (pomijając brak checków po stronie klient'a).

Po weekendzie jedyne co udało mi się zrobić, to zachorować, przez co nie miałem sił na uczenie się WebSocket'ów i zamiast tego zaimplementowałem tymczasowe, naiwne podejście pytania serwer co 5 sekund czy nie ma aktualizacji (endpoint "Game/CheckUpdate/"). Sama próba implementacji WebSocket'ów sprawiła, że ruszyłem całą logikę z manipulacją danymi do serwisów, co polepszyło jakość kodu, co jest na plus.

# Funkcjonalności:
- Użytkownik może strzorzyć nowe konto.
- Użytkownik może "zalogować" się do już utworzonego konta jeśli posiada jego prywatne ID.
- Aplikacja automatycznie przechowuje w danych przeglądarki prywatne ID użytkownika by ten nie musiał się "logować" więcej niż raz.
- Zalogowany użytkownik może utworzyć rozgrywkę z dowolnym graczem po wprowadzeniu jego publicznego ID.
- Zalogowany użytkownik może dołączyć do rozgrywek których jest członkiem.
- Zalogowany użytkownik może dokonywać ruchów na planszy gry w której uczestniczy.
- Aplikacja co 5 sekund odświeża swoje informacje o grze pozwalając na rozgrywkę w czasie pseudo-rzeczywistym.
