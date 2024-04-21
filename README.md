# Tree Crafting Voyager

Zaprojektowałem aplikacje typu "Sklep" gdzie kategorie to węzły a produkty to liście.  
Menu jest budowane rekurencyjnie.  
Kliknięcie kategorii pobierze produkty należące do niej i jej wszystkich dzieci.  
  
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
