# Happy Team - BattleShips
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
