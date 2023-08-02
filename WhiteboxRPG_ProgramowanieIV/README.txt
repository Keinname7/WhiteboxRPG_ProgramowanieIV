Projekt ten to raczej niedoszlifowana aplikacja WPF którą napisałem na jeden z moich przedmitów na studiach.
Jest to prosta tekstowa gra RPG inspirowana starymi wersjami gry tabletop-RPG Dungeons&Dragons. Dostępne są trzy klasy postaci, i gra polega na wchodzeniu do lochów, zdobywania skarbów i doświadczenia, oraz powrocie to karczmy po leczenie i zapis stanu gry - i okazyjnym zwiedzeniem zamku na level up.
Przyznaję że nie jestem zbyt dumny z tego programu: wygląd aplikacji jest zbyt minimalistyczny i byłem zbyt zajęty na ostatnim semestrze by go dopracować.
Miałem zrobione 'czystsze' i bardziej jakościowe projekty z poprzednich lat, ale niestety utraciłem ich pliki po zeszłorocznej awarii mojego laptopa...
Możecie sobie przejrzeć ten kod, może pobrać projekt i spróbować go uruchomić (poniżej instrukcje jak to zrobić).

Dla działania zapisu i wczytu danych w aplikacji potrzebne jest połączenie z bazą danych zwaną Database1.mdf.
Konieczne będzie odkomentowanie 155-tej linjki MainWindow.xaml.cs , i zakomentowanie / lub modyfikacja 156 linijki - są odpowiedzialne za zmienną cnString , czyli ścieżka do połączenia z bazą danych.
Samą baże danych nie umiem wrzucić na Gita, więc daję poniżej link do pobrania jej z dysku google:


Alternatywa to stworzenie jej np. w visualu: poniżej jej dwie tabele.
(Gdyby połączenie z bazą nie było wymaganą częścią projektu użyłbym zwykłego pliku txt..)



CREATE TABLE [dbo].[Character] (
    [Id]    INT   NOT NULL,
    [Class] NTEXT NOT NULL,
    [Lv]    INT   NOT NULL,
    [Exp]   INT   NOT NULL,
    [Gold]  INT   NOT NULL,
    [maxHP] INT   NOT NULL,
    [HP]    INT   NOT NULL,
    [maxMP] INT   NOT NULL,
    [MP]    INT   NOT NULL,
    [BAB]   INT   NOT NULL,
    [MR]    INT   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Items] (
    [Type]  INT   NOT NULL,
    [Name]  NTEXT NOT NULL,
    [hit]   INT   NOT NULL,
    [dmg]   INT   NOT NULL,
    [ac]    INT   NOT NULL,
    [mr]    INT   NOT NULL,
    [value] INT   NULL,
    PRIMARY KEY CLUSTERED ([Type] ASC)
);
