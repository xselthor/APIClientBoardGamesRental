# APIClientBoardGamesRental
Wypozyczalnia gier planszowych. Praca w trakcie.
<br><br>
<b>Rzeczy które działają:</b>
<ol>
<li>Klient łączy się z API i pobiera dane gier z wystawionego JSONa</li>
<li>Dodawanie nowych gier, modyfikowanie wpisów, wyświetlanie szczegółów, usuwanie wpisów. </li>
  <li>Wyświetlanie listy wszystkich użytkowników</li>
<li>Dodawanie modyfikowanie i usuwanie kont użytkowników</li>
  <li>Wyświetlanie listy ról</li>
<li>Dodawanie, modyfikowanie i usuwanie ról</li>
<li>Konta użytkowników przeniesione na MongoDB
  <ul>
    <li>Wymagany NuGet: AspNetCore.Identity.MongoDbCore (https://github.com/alexandre-spieser/AspNetCore.Identity.MongoDbCore)
    <li>ConnectionString do bazy MongoDB należy dodać w pliku Startup.cs (35 wiersz)
  </ul>
</ol>
<br><br>
<img src="apiclient.jpg">

Klient działa z api <a href="https://github.com/xselthor/APIBoardGamesRental">APIBoardGamesRental</a>
