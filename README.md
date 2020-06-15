# APIClientBoardGamesRental
Wypozyczalnia gier planszowych. Praca w trakcie.
<br><br>
<b>Działa:</b>
<ul>
<li>Klient łączy się z API i pobiera dane gier z wystawionego JSONa</li>
<li>Dodawanie nowych gier, modyfikowanie wpisów, wyświetlanie szczegółów, usuwanie wpisów. </li>
<li>Dodawanie modyfikowanie i usuwanie kont użytkowników</li>
<li>Dodawanie modyfikowanie i usuwanie ról użytkowników</li>
<li>Konta użytkowników przeniesione na MongoDB
  <ol>
    <li>Wymagany NuGet: AspNetCore.Identity.MongoDbCore (https://github.com/alexandre-spieser/AspNetCore.Identity.MongoDbCore)
    <li>ConnectionString do bazy MongoDB należy dodać w pliku Sturtup.cs (35 wiersz)
  </ol>
</ul>
<br><br>
<img src="apiclient.jpg">
