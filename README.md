# Tree Crafting Voyager  
  
Ten projekt to aplikacja typu "Sklep", która wykorzystuje zaawansowane zarządzanie strukturą drzewiastą do reprezentowania kategorii i produktów. Aplikacja składa się z dwóch głównych komponentów: backendu napisanego w ASP.NET Core (Web API) z wykorzystaniem .NET 8 oraz frontendu stworzonego przy użyciu Vue.js z bibliotekami Bootstrap.  
  
W projekcie zastosowano wzorce projektowe takie jak: repository, strategy.
  
## Funkcjonalności  
  
Zarządzanie Strukturą Drzewiastą: Aplikacja pozwala na dodawanie, edycję, usuwanie oraz sortowanie węzłów (kategorii) i liści (produktów). Użytkownicy mogą również przenosić węzły między różnymi gałęziami drzewa.  
  
Dynamiczne Menu: Menu jest budowane rekurencyjnie i umożliwia łatwe nawigowanie po kategoriach i produktach.  
Ładowanie Produktów: Kliknięcie w kategorię spowoduje załadowanie produktów należących do tej kategorii oraz wszystkich jej podkategorii.  
Walidacja Danych: Aplikacja zawiera zabezpieczenia uniemożliwiające wprowadzenie nieprawidłowych danych.  
Dane Przykładowe: Aplikacja zawiera funkcjonalność do wypełnienia baze danych przykładowymi danymi, co ułatwia testowanie i demonstrację funkcjonalności.  


## Technologie  
  
Backend: ASP.NET Core Web API (.NET 8)  
Frontend: Vue.js, Bootstrap  
Baza danych: PostgreSQL  
  
## Jak uruchomić aplikację  

Zaktualizuj Visual Studio do najnowszej wersji  
  
1. W projekcie "**TreeCraftingVoyager.Server**" uzupełnij plik "**appsettings.json**":  
```  
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TreeCraftingVoyager;Username=postgres;Password=your_password"
  },
}
```  
2. W wartości klucza "**DefaultConnection**" wprowadź informacje dotyczące połączenia z bazą danych PostgreSQL.
3. Na górnym pasku menu wybierz "Narzędzia", a następnie z rozwijanej listy najedź na "Menedżer pakietów NuGet" i z kolejnej listy wybierz "Konsola menedżera pakietów".  
4. W konsoli menedżera pakietów dla pola "Projekt domyślny" z rozwijanej listy wybierz "TreeCraftingVoyager.Server".  
5. W konsoli menedżera pakietów wpisz komende "Update-Database" i wciśnij Enter.  
6. Uruchom aplikację (Ctrl+F5).  
7. Frontend uruchomi się automatycznie, wystarczy po załadowaniu backendu uruchomić adres https://localhost:5173

Jeśli projekty nadal się nie uruchamiają, skorzystaj z bardziej szczegółowych poradników poniżej.  

Projekt zawiera kilka komponentów, z których każdy ma własny plik README z bardziej szczegółowymi informacjami:  
  
- [Server - ASP.NET Core](TreeCraftingVoyager.Server/README.md)
- [Client - Vue](treecraftingvoyager.client/README.md)
