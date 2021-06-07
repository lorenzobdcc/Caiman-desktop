# Cahier des charges


## Objectifs

Caiman est une application pour Windows regroupant plusieurs émulateurs de console de jeu. L’utilisateur peut exécuter les jeux depuis Caiman. 

Caiman a pour but d'être simple d’utilisation, aucune connaissance n’est requise pour l’utiliser. Caiman permet une utilisation complète à la manette ou au clavier/souris. L’avantage de Caiman est qu’il ne requiert aucune configuration. Caiman est utilisable instantanément par l’utilisateur, contrairement  d’un émulateur traditionnel qui requiert une configuration relativement compliquée.

Le téléchargement des jeux se fait directement depuis Caiman, un serveur nommé le “Bunker” contient les fichiers des différents jeux.

Un site web permet de créer un compte pour accéder à l’application , de voir les informations des joueurs ainsi que de télécharger Caiman.


## Spécification


### Emulateurs et contrôles

Les différents émulateurs qui permettent d'exécuter des jeux dans Caiman sont:



*   PCSX2 (playstation 2)
*   Dolphin (gamecube / wii)

L’utilisateur doit utiliser une manette de Xbox pour utiliser Caiman à la manette (une seule manette est requise pour jouer mais plusieurs peuvent être connectées, pour jouer en multijoueurs local).

Caiman est utilisable à la manette à l'exception des formulaires (création de compte, importation de jeux).


### Création d’un compte utilisateur

L’utilisateur a l’obligation de créer un compte pour pouvoir utiliser Caiman.

L’utilisateur peut créer un compte directement depuis Caiman s'il n’en possède pas.


### Interface

Dans l'interface, un système de catégories existe pour permettre de se retrouver plus aisément dans la liste des jeux à télécharger. Les différentes catégories sont créées par un administrateur.

Exemple de catégories:



*   jeux Mario
*   jeux Zelda
*   jeux d’aventure
*   jeux de réflexion
*   jeux multijoueur
*   jeux favoris
*   etc..

L’utilisateur peut ajouter depuis Caiman des jeux favoris pour pouvoir les retrouver plus facilement. ( une catégorie “favoris” est visible contenant les jeux favoris de l’utilisateur authentifié )


### Paramètre graphiques

L’utilisateur a la possibilité de modifier certains paramètres d’émulation:



*   La définition des jeux (définition native, proche de 1080p, proche de 4K).
*   La langue des machines.
*   Si les jeux doivent être lancés en plein écran ou non.
*   Format d’écran 16/9 ou 4/3.


### Gestion des sauvegardes

Il existe plusieurs cas où les sauvegardes de l’utilisateur se synchronisent:



1. Le joueur sauvegarde sa partie directement dans un jeu depuis Caiman. La sauvegarde que l’utilisateur vient de créer est envoyée sur le Bunker et remplace la version présente.
2. Quand le joueur lance Caiman, une vérification est faite pour savoir si une version plus récente des sauvegardes du joueur sont présentes, si c’est le cas, elles sont téléchargées sur la machine du joueur.


### Spécifications du “Bunker” 

Le Bunker contient les fichiers des différents jeux au format (.ISO).

Le Bunker contient les fichiers de sauvegardes des utilisateurs. Ces sauvegardes seront envoyées de la machine de l’utilisateur vers le Bunker quand l’utilisateur sauvegarde sa partie depuis un jeu.

Les données des utilisateurs (email, jeux favoris, heures de jeux) sont stockées dans une base de données.


### Spécification site web (site présent dans le Bunker)

Sur le site web, il est possible de créer un compte en spécifiant un mail et un mot de passe.

L’interface web permet de rechercher des utilisateurs pour voir leur profil.

L’affichage d’un profil permet de voir les jeux favoris d’un utilisateur ainsi que son nombre d'heures de jeu sur chaque jeu.

Le site web permet à un administrateur d’ajouter des jeux et de modifier les informations des jeux.

Le site web permet de modifier les informations de compte d’un utilisateur.

Un utilisateur peut faire la demande de réinitialiser son mot de passe.


### Installation

Caiman est téléchargeable depuis le site web. L'installateur contient les émulateurs, il n’y a donc rien à télécharger d’autre.


## Limites du Projet

Caiman sera limité aux jeux que j’ai pu ajouter moi même ainsi qu’aux émulateurs que j’aurais importé, la totalité des consoles de jeu ne sera donc pas prise en charge.


## Calendrier

Le début du travail de diplôme est fixé au lundi 19 avril et le rendu est fixé au 11 juin à 12h00.

Le travail est à exécuter soit en présentiel et en télétravail du lundi au vendredi et cela 8h00 par jour.


## Matériel

J’ai à ma disposition pour la réalisation de Caiman:



*   Mon pc pour le développement
    *   Intel i7-7700 3.6 GHz
    *   32 GB de ram
    *   GTX 1060 3GB
*   Un serveur pour héberger le site et les fichiers du store
*   Une manette de Xbox Series