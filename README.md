# TaskBoard – app demo .NET (Blazor + Minimal API)

Piccola to-do list pensata per una demo live di 5 minuti con due issue da risolvere in diretta.

## Requisiti
- .NET 8 SDK (il progetto è `net8.0`). Con SDK 9 o 10 compila comunque; per passare a una versione
  più recente basta cambiare `<TargetFramework>` in `TaskBoard.csproj`.
- Nessun pacchetto NuGet esterno, nessun database: i dati stanno in memoria e si azzerano a ogni riavvio
  (comodo per "resettare" la demo).

## Avvio
```bash
dotnet watch
```
L'app parte su http://localhost:5080. Con `dotnet watch` le modifiche vengono applicate con hot reload.
Se `dotnet watch` chiede di riavviare per una modifica non supportata, rispondi "a" (sempre) oppure
avvialo con la variabile `DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true`.

## Struttura
| File | Contenuto |
|---|---|
| `Program.cs` | Registrazione servizi, Minimal API `/api/tasks`, mapping Blazor |
| `Services/TaskService.cs` | Archivio in memoria (singleton) |
| `Models/TaskItem.cs` | Modello e DTO di richiesta |
| `Components/Pages/Home.razor` | Unica pagina: inserimento, filtri, lista, contatore |
| `wwwroot/app.css` | Stili |
| `TaskBoard.http` | Chiamate API pronte (VS / VS Code REST Client / Rider) |

## Branch
- `main`: versione con i due bug, da cui parte la demo.
- `solution`: le fix già fatte (piano B). Per vedere esattamente cosa cambia: `git diff main solution`.

## Scaletta (5 minuti)
1. **0:00–1:00** – Giro dell'app e del codice: `Home.razor` → `TaskService` → `Program.cs`.
2. **1:00–2:30** – Issue #1: il contatore dice 1 invece di 2. Fix in `Home.razor`.
3. **2:30–4:00** – Issue #2: si possono creare attività vuote, sia dalla UI sia via API (`TaskBoard.http`).
   Fix nel service (validazione lato server) e nel bottone (UI).
4. **4:00–5:00** – Commit con `Fixes #1, fixes #2` e chiusura.

## Prima di salire sul palco
- `dotnet build` una volta, così la prima compilazione non pesa in diretta.
- Browser già aperto su http://localhost:5080, zoom al 125–150% per la leggibilità a distanza.
- Le issue create su GitHub/Azure DevOps con il testo di `ISSUES.md`.
