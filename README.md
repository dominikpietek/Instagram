# Krótki opis aplikacji

Aplikacja jest wzorowana na popularnym instagramie, którego głównymi funkcjami jest umieszczanie postów, stories (zdjęć widocznych tylko przez 24h) oraz komunikacji ze znajmomymi.

# Użyte biblioteki i wzorce projektowe

Biblioteki:
- Entity Framework Core
- Nlog
- Newtonsoft.json
- XUnit
- Bogus

Wzorce projektowe:
- MVVM
- Repositories Pattern
- Factory Pattern
- Dependency Injection

Inne:
- Szyfrowanie haseł przez hashowanie

# Zdjęcia gotowego projektu i jego funkcji

Okno logowania:
- pozwala zalogować się na konto
- pozwala stworzyć konto
- posiada walidacje loginu i hasła

<img src="ReadmePhotos/login.png"/>

Okno tworzenia konta:
- pozwala stworzyć konto wraz ze zdjęciem

<img src="ReadmePhotos/createAccount.png"/>

Główne okno:
- posiada po lewej stronie posty użytkowników
- posty posiadają: polubienia, komentarze, oraz odpowiedzi na komentarze
- posiada po prawej użytkowników, których możemy znać i zaproszenia do znajomych
- nad postami znajdują się stories (zdjęcia dostępne tylko przez 24h)
- na górze jest miejsce, w którym możemy wyszukiwać użytkowników po ich nazwach
- na górze po prawej stronie jest menu, które pozwala: wrócić na główną stronę, przenieść się do komunikatora i edytować konto

<img src="ReadmePhotos/feed.png"/>

<img src="ReadmePhotos/feedDown.png"/>

- w menu możemy zmienić kolor tła na ciemny

<img src="ReadmePhotos/darkModeProfile.png"/>

- edytowanie profilu użytkownika

<img src="ReadmePhotos/editProfile.png"/>

- znajomi w komunikatorze

<img src="ReadmePhotos/friends.png"/>

- konwersacja w komunikatorze

<img src="ReadmePhotos/messenger.png"/>

- wyszukiwanie użytkowników

<img src="ReadmePhotos/searchFriends.png"/>

- sprawdzanie profilu użytkownika

<img src="ReadmePhotos/userProfile.png"/>

Okno tworzenia postów:
- pozwala opublikować post
- posiada walidację wpisywanych danych

<img src="ReadmePhotos/createPost.png"/>

Stories oraz ich tworzenie:
- pozwala opublikować zdjęcia na 24 godziny

- tworzenie story

<img src="ReadmePhotos/createStory.png"/>

- wyświetlanie story

<img src="ReadmePhotos/story.png"/>

# Creditsy:

Zdjęcia do wygenerowania kont i postów pobrane z:
https://pixabay.com/

Algorytm hashowania:
https://www.sean-lloyd.com/post/hash-a-string/

# Jak zainstalować? (Quick start)

niedługo :)

# Kim jest kontrybutor?

Jest to moje stare konto, z którego przez prypadek dodałem commita i już tak zostało :)