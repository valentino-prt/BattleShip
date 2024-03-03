# Projet BattleShip

## Aperçu

Ce projet BattleShip est une implémentation moderne et interactive du classique jeu de bataille navale. Il utilise les technologies Blazor WebAssembly pour le frontend et ASP.NET Core pour le backend, offrant une expérience utilisateur riche et réactive entièrement basée sur le web. Le jeu permet d'affronter l'ordinateur tentant de couler les navires adverses.

## Caractéristiques Principales

- **Interface Utilisateur Intuitive** : Une interface Blazor WebAssembly qui rend le jeu accessible depuis n'importe quel navigateur moderne.
- **Communication Grpc** : Utilisation de gRPC pour une communication rapide et efficace entre le client et le serveur.
- **Validation des Requêtes** : Intégration de FluentValidation pour s'assurer que toutes les requêtes envoyées au serveur sont valides.
- **Persistance des Données** : Un système de leaderboard et d'historique des coups joués, permettant aux joueurs de suivre leurs progrès.
- **Implémentation de signalR** : Implémentation de signalR pour une mise à jour en temps réel de la grille des joueurs.
- **Implémentation du Multiplayer** : Possibilité de joué en multijoueur depuis deux navigateurs différents.


## Technologies Utilisées

- **Frontend** : Blazor WebAssembly
- **Library CSS** : Bootstrap
- **Backend** : ASP.NET Core
- **Communication Client-Serveur** : gRPC avec support de gRPC-Web pour la compatibilité avec les navigateurs web.
- **Validation** : FluentValidation
- **SignalR**

## Configuration et Installation
0. **Prérequis** :  Exécutez la commande suivante pour générer et faire confiance à un certificat de développement HTTPS sur votre machine
   ```
   dotnet dev-certs https --trust
   ```

2. **Lancement du Serveur Backend** :
   - Naviguez dans le répertoire du projet backend et exécutez `dotnet run`.

3. **Lancement de l'Application Blazor** :
   - Ouvrez le répertoire du projet frontend et exécutez `dotnet run`.

4. **Accédez à l'Application** :
   - Ouvrez votre navigateur et accédez à `https://localhost:5051` pour lancer le jeu.

## Contribution

Ce projet a été développé par :

- **OHAYON Samuel**
- **POIROT Valentin**
