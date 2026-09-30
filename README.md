# 🚗 RideLinker

RideLinker is een carpool-platform ontwikkeld voor CM.com, waarmee medewerkers op één centrale plek alle 
geplande ritten van punt A naar punt B kunnen bekijken. Per rit is zichtbaar wie 
de bestuurder is, in welke auto wordt gereden en wie er al meerijden, inclusief 
of er nog plaats is.

Als er geen geschikte rit gepland staat, kan een gebruiker (mits er voldoende 
capaciteit is) zelf een rit inplannen tussen twee locaties op een specifiek 
tijdstip. Andere medewerkers kunnen zich vervolgens aanmelden voor deze rit om 
samen te carpoolen.

---

## ✨ Functionaliteit

- **Overzicht van ritten** — alle geplande ritten van A naar B op één plek.
- **Ritdetails** — bestuurder, auto en deelnemers per rit, inclusief beschikbare plaatsen.
- **Rit aanmaken** — indien er geen geschikte rit is en er capaciteit beschikbaar is, 
  kan een gebruiker zelf een rit inplannen tussen twee locaties op een gekozen tijdstip.
- **Aanmelden voor een rit** — anderen kunnen zich inschrijven om mee te rijden.
- **Rolgebaseerde toegang** — admins, employees en gebruikers zien en mogen verschillende dingen.
- **Authenticatie** — inloggen met ASP.NET Core Identity, beveiligd met JWT.

---

## 🧱 Architectuur

| Laag | Technologie |
|---|---|
| Frontend | Angular 17, TypeScript, Bootstrap 5, ng-bootstrap |
| Backend | ASP.NET Core 6 Web API |
| Database | SQL Server |
| Authenticatie | ASP.NET Core Identity + JWT |
| ORM | Entity Framework Core |
| Testing (frontend) | Jest, Testing Library Angular |
