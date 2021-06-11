# Analyse fonctionnelle


## Base de données


### Schéma

Le schéma suivant représente la structure de la base de données. La base de données est commune au site web et à l'application Caiman. Les tables sont principalement utilisées pour stocker les informations des utilisateurs et des différents jeux.

![alt_text](images/caiman_database.png "image_tooltip")


### Table categorie

Cette table est utilisée pour stocker les différentes catégories auxquelles les jeux peuvent appartenir.

### Table console

Cette table sert à stocker les différentes consoles prises en charge par l'application Caiman. Chaque console doit être reliée à un émulateur.

### Table emulator

La table émulateur sert à lister les différents émulateurs disponible pour l'application, il est possible qu' un émulateur soit compatible avec plusieurs consoles.


### Table favoritegame

Sert à lister les jeux favoris des utilisateurs.


### Table file

La table file sert à lister le nom d’un fichier, cela peut être un fichier .iso d’un jeu, un fichier de sauvegarde ou un fichier de configuration utilisateur. 


### Table filesave

Sert  faire le lien entre un émulateur, un utilisateur, et un fichier de sauvegarde. Alors elle permet de connaître les sauvegardes pour un émulateur particulier d'un utilisateur.


### Table game

Table qui liste les jeux disponibles. Chaque jeu a plusieurs informations: un nom, une description, une console. La console permet de à l’application de savoir quel émulateur doit être utilisé. 


### Table gamehascategorie

Sert à assigner des catégories aux jeux.


### Table rôle

Sert à lister les différents rôles (utilisateur, administrateur), elle ne sert que pour le site web sachant que toute la partie administration est sur le site. 


### Table timeingame

Sert à stocker le temps de jeu en minutes de chaque utilisateur. Le temps de jeu est spécifique à chaque jeu. Il est mis à jour directement depuis l’application Caiman.


### Table user

Cette table sert à stocker les informations de compte de chaque utilisateur de Caiman. Le compte est commun au site web et à l'application. Le mot de passe de l’utilisateur est crypté avec les fonctions de sécurité de php.


### Table userconfigfile

Cette table sert à faire le lien entre un fichier de configuration et un utilisateur. Le fichier de configuration sert à connaître la configuration. 

## Site web Caiman


### Création de compte

L’utilisateur du site a la possibilité de créer un compte qui sera commun au site et à l'application. La création de compte nécessite de renseigner son email, de donner un nom d’utilisateur ainsi que un mot de passe.





![alt_text](images/SignIn.png "image_tooltip")



### connexion

La connexion à son compte utilisateur permet de modifier nos informations de compte et d’ajouter ou de supprimer des jeux à la liste de favoris.

Si l’utilisateur oublie son mot de passe, il a la possibilité de le réinitialiser. L’utilisateur qui se décide de réinitialiser son mot de passe reçoit un mail contenant un lien de réinitialisation.



![alt_text](images/login.png "image_tooltip")



### modification des informations d’un utilisateur

Un utilisateur connecté a la possibilité de modifier ces informations:



*   mot de passe
*   liste de jeux favoris 
*   la visibilité de son profil pour les autres utilisateurs



![alt_text](images/usersInfos.png "image_tooltip")



### affichage des jeux

Tous les utilisateurs ont la possibilité d’afficher la liste de jeux disponible. Il n’y a pas de restriction particulière. 

Les informations disponibles pour chaque jeu sont les suivantes:



*   Nom
*   Description
*   Catégories du jeu



![alt_text](images/gamesDetails.png "image_tooltip")



### affichage d’un profil utilisateur

Il est possible de consulter la page personnelle d'un utilisateur si celui-ci a rendu son compte publique. Les informations disponibles sont celle-ci:



*   Nom d’utilisateur
*   Jeux favoris
*   Nombres d’heures de jeux sur chaque jeu



![alt_text](images/UserFavoritesGames.png "image_tooltip")




![alt_text](images/UserTimeInGame.png "image_tooltip")



### ajout d’un jeu à la base de données / sur le Bunker pour le fichier .ISO 

L’ajout d’un jeu se fait grâce à un formulaire, plusieurs champs sont à renseigner:



*   le nom du jeu
*   une description
*   une image
*   la console du jeu qui est uploadé
*   le nom que va porter le jeu sur le Bunker
*   le fichier de base du jeu

L’ajout à la base va créer deux entrées. Une dans la table Game et une autre dans la table file. Après avoir été ajouté depuis le site web, le jeu devient accessible depuis l'application Caiman et le site web pour de la consultation.

 


![alt_text](images/FormAddGame.png "image_tooltip")



### modification d’un jeu

La modification d’un jeu ne peut être faite que par un administrateur. Les modifications possibles sont les suivantes:



*   nom
*   description
*   console
*   catégories


![alt_text](images/GameDetailsAdmin.png "image_tooltip")



### Administration

Les administrateurs ont la possibilité de faire plusieurs choses. Donc, je vais les lister ici, il n’est pas nécessaire de les détailler.



*   modifier un jeu
*   ajouter des catégories
*   ajouter ou supprimer des catégories à un jeu



![alt_text](images/FormAddCategory.png "image_tooltip")



### Téléchargement

L’un des intérêts du site est de pouvoir télécharger l'application Caiman. Le téléchargement de cette application nécessite d'être authentifié sur le site. Si un invité se rend sur la page de téléchargement sans être authentifié, une invitation lui sera faite de s’authentifier ou de créer un compte.


![alt_text](images/downloadNotConnected.png "image_tooltip")


![alt_text](images/download.png "image_tooltip")



### Fonctionnalitées manquante

Malheureusement dû au temps imposé j’ai dû faire des choix organisationnel. J’ai donc décidé de me focaliser sur L’application Caiman au lieu du site internet ce qui explique que certaines fonctionnalités ne soient pas finalisées ou mises en place. Je vais lister les ce qui n’a pas été fini complètement ou abandonné.



*   Modification complète des données des jeux
*   Modification d’une catégorie
*   Réinitialisation de mot de passe
*   Création de compte administrateur

### API Caiman

L’API sert à pouvoir accéder à la base de données depuis l’application Caiman. Je vais détailler les endpoint et la structure de l’API.


### Structure de l’API

![alt_text](images/schema-API.png "image_tooltip")


Pour expliquer la structure de l’API, je vais expliquer étape par étape comment un appel se passe.



1. L'utilisateur envoie une requête à la page index.php de api.caiman.cfpt.info.
2. La requête est réceptionnée par index.php. L’url est ensuite traité par le .htaccess pour savoir où doit envoyer à quel controller.
3. Le contrôleur décide selon les informations reçues quelle méthode il doit exécuter.
4. Le DAO est appelé et va rechercher dans la base de données les données demandé
5. Le DAO créé la réponse grâce au model.
6. La réponse est envoyée à l'utilisateur par l’intermédiaire de la page index.php


### Categories


### GET

Permet de recevoir la liste des catégories disponibles.

Les informations reçues sont les suivantes:



*   id
*   nom


### Games


### GET

Retourne la liste des jeux.

Les informations reçues sont les suivantes:



*   id
*   description
*   nom de l’image
*   id de la console
*   id du fichier du jeu


### GET(?byName)

Retourne la liste des jeux qui dans le nom contient ce que l’utilisateur a demandé.

Les informations reçues sont les suivantes:



*   id
*   description
*   nom de l’image
*   id de la console
*   id du fichier du jeu


### GET(?byCategory)

Retourne la liste des jeux qui appartiennent à une catégorie.

Il faut spécifier l’id de la catégorie qui est demandée.

Les informations reçues sont les suivantes:



*   id
*   description
*   nom de l’image
*   id de la console
*   id du fichier du jeu


### GET(?byFavoriteUser)

Retourne la liste des jeux favoris d’un utilisateur.

Il faut spécifier l’id de l’utilisateur.

Les informations reçues sont les suivantes:



*   id
*   description
*   nom de l’image
*   id de la console
*   id du fichier du jeu


### GET(?byUserTime)

Retourne la liste des jeux auxquels un joueur a joué.

Il faut spécifier l’id de l’utilisateur.

Les informations reçues sont les suivantes:



*   id
*   description
*   nom de l’image
*   id de la console
*   id du fichier du jeu
*   nombre de minutes en jeu


### GET(?gameFileName)

Retourne le nom du fichier d’un jeu.

Les informations reçues sont les suivantes:



*   filename


### GET(?gameConsole)

Retourne la console d’un jeu.

Les informations reçues sont les suivantes:



*   name
*   folderName


### GET(?idGame&apiKey)

Retourne le fichier d’un jeu.

Les informations reçues sont les suivantes:



*   fichier.iso


### GET(?idGameTime&idUser)

Retourne le temps de jeu sur un jeu. 

Les informations reçues sont les suivantes:



*   minutes


### GET(?idEmulator&idUser&apiKey)

Retourne un fichier zip contenant les sauvegardes d’un joueur pour un émulateur particulier 

Les informations reçues sont les suivantes:



*   fichier.zip


### POST(?idEmulator&idUser&apiKey)

Upload un fichier zip contenant les sauvegardes d’un joueur pour un émulateur particulier 


### POST(idGameAdd&idUser)

Ajouter un jeu en favoris pour un utilisateur particulier


### POST(idGameRemove&idUser)

Supprime un jeu en favoris pour un utilisateur particulier


### POST(idGameCheck&idUser)

Vérifie si un jeu est déjà en favoris et retourne un booléen


### POST(idGameTimeAdd&idUser)

Ajouter une minute de jeu à un jeu particulier pour un utilisateur


### Users


### GET(sans paramétres)

Retourne la liste des utilisateurs.

Les informations reçues sont les suivantes:



*   id
*   username


### POST(avec apitoken)

Retourne un utilisateur en particulier

Les informations reçues sont les suivantes:



*   id
*   username
*   password
*   salt
*   apitoken
*   caimanToken
*   email
*   idRole


### User/connection


### POST(?username, password)

Permet de vérifier les informations de connexion d’un utilisateur

Les informations reçues sont les suivantes:



*   id
*   username
*   password
*   salt
*   apitoken
*   caimanToken
*   email
*   idRole


### POST(caimanToken)

Permet de recevoir les informations d’un utilisateur grâce à un token généré à chaque connexion.

Les informations reçues sont les suivantes:



*   id
*   username
*   password
*   salt
*   apitoken
*   caimanToken
*   email
*   idRole

## Application  Caiman C#


### Connexion

L’utilisateur de Caiman doit obligatoirement être connecté pour pouvoir utiliser caiman. La connexion se fait avec le nom d’utilisateur et le mot de passe. Une fois la connexion valable, elle est maintenue tant que l’utilisateur ne se déconnecte pas ou tant que l’utilisateur ne s'est pas connecté sur un autre ordinateur.

![alt_text](images/login_caiman.png "image_tooltip")



### Inscription

L’inscription n’est pas disponible sur caiman mais un bouton est disponible pour être redirigé sur le site web de Caiman.


### Téléchargement de jeu

L’utilisateur a la possibilité de télécharger des jeux. Les jeux disponibles dans l’application sont ajoutés depuis le site internet de Caiman. Les téléchargements des jeux se font les un après les autres. 
 
![alt_text](images/Caiman_non_downloaded_game.png "image_tooltip")


![alt_text](images/Caiman_download_queu.png "image_tooltip")



### Lancement d’un jeu

Caiman inclut deux émulateurs Dolphin un PCSX2. Ces deux émulateurs permettent d'exécuter des jeux de Gamecube et Wii pour Dolphin et de Playstation 2 pour PCSX2. Pour lancer un jeu, il suffit de le télécharger, puis de cliquer sur Play. Il n’est donc pas nécessaire de lancer soit même un émulateur.


![alt_text](images/caiman_downloaded_game.png "image_tooltip")



### Synchronisation des sauvegarde entre le pc client et le Bunker

Les sauvegardes de l’utilisateur sont synchronisées entre les différents pc qu’il utilise. La synchronisation se fait à la connexion, les sauvegardes sont stockées sur les serveurs de Caiman. La copie des sauvegardes des utilisateurs est envoyée automatiquement donc l’utilisateur n’a pas de manipulation à faire. L’envoie se passe dès que l’utilisateur sauvegarde, cela permet d’éviter de perte de sauvegardes si un problème se produit durant le moment où le joueur est en train de jouer.


### Modification de la configuration utilisateur

L’utilisateur a la possibilité de modifier plusieurs paramètres graphiques. Il a la possibilité de choisir en trois mode de configuration global:



*   Original
*   1080p
*   4K

C’est différent mode modifie l'antialiasing et la définition native de l’émulateur qui va être utilisé.

L’utilisateur a aussi la possibilité de choisir entre lancer le jeu plein écran ou non et il peut choisir si le jeu doit être en 16/9 ou en 4/3.

![alt_text](images/caiman_configuration.png "image_tooltip")



### Application de la configuration utilisateur

La configuration est appliquée avant de lancer un jeu, c’est à dire que la configuration n’est pas appliquée si l’utilisateur est déjà en jeu. 


### Ajout/suppresion de jeu en favoris

L’utilisateur a la possibilité de modifier ses jeux favoris directement depuis caiman.

![alt_text](images/caiman_add_favorite.png "image_tooltip")


Suppression de jeu des favoris

![alt_text](images/caiman_remove_favorite.png "image_tooltip")



### Affichage de jeux par catégories

L’utilisateur a la possibilité d’afficher les jeux par catégories. Les catégories sont créées manuellement par un administrateur. Il y a certaines catégories "spéciales", par exemple les jeux favoris et les jeux uniques téléchargés par chaque utilisateur.

![alt_text](images/caiman_categories.png "image_tooltip")



### Nombre d’heures de jeu

Le nombre de minutes de jeu d’un utilisateur est mis à jour à chaque minute de jeu. le nombre de minutes de jeu est visible sur la page de chaque jeu si l’utilisateur n’a pas encore joué.

![alt_text](images/caiman_time_played.png "image_tooltip")



### Gestion des manettes

Pour Caiman, les manettes supportées sont les manettes pour xbox qui fonctionne avec Xinput. Il est possible d’utiliser d'autres manettes pour cela, il faut passer par un programme qui va convertir les inputs de la manette non compatible en input de manette xbox.

Pour gérer les déplacements, j’utilise les touches “haut, bas, gauche,droite”, le stick gauche, la validation se fait avec la touche “A” et le retour arrière avec la touche “B”.

## Interface utilisateur utilisable à la manette


### Réception des input des manettes connecté

L’utilisateur de Caiman a la possibilité de pouvoir utiliser l’application au clavier souris mais aussi à la manette. Pour ce faire, j’ai utilisé le paquet nuGet “SharpDX.XInput”. Ce paquet me permet de connaître les manettes connectées au pc ainsi que les touches appuyées par l’utilisateur.

La seule manette qui peut se déplacer dans l'application est la manette 1. Pour connaître les boutons de la manette, j’utilise la fonction getInput(). Cette fonction me permet de connaître les touches qui sont pressées à un instant T.  Je vais chercher les inputs toutes les 2ms pour être sûr de ne pas louper d’inputs. 

Les inputs sont ensuite traités par l’interface de l’application qui décide quoi en faire selon le contexte.


### Transformation des input de la manette en événement 

Les inputs de la manette sont analysés par la form principale de Caiman. Selon la ou les touches qui sont pressées, l’application exécute des actions différentes. Par exemple, quand la touche “A” est pressée, alors le programme envoie la touche ENTER à l'application ce qui me permet de cliquer sur des boutons. 

Pour gérer les événements, je fais un test pour savoir si l'utilisateur utilise l’application ou non . Si l’application n’est pas focus par l’utilisateur, seul une partie des actions sont possibles pour éviter que des actions inattendues puissent arriver alors que l’application n’est plus visible.


### Structure de l’affichage

L’affichage est constitué d’un “XboxMainForm”. Il sert à contenir tous les autres panels, il est aussi chargé de la gestion des inputs de la manette de l’utilisateur 1.

Un “XboxMainForm” contient une liste de XboxUserController qui elles contiennent différentes choses comme des boutons des images ou des labels.

Le XboxMainForm est aussi responsable de la gestion des demandes de l'utilisateur, par exemple si l’utilisateur veut afficher l’accueil de Caiman, il va lui passer un objet contenant sa demande. Il va donc traiter les demandes et afficher les panels selon les besoins de l'utilisateur.


### Déplacement dans un panel de l’application

L’application est conçue avec des “panels”, c'est-à-dire une liste de listes de controls. Cette liste de controls est propre à chaque panel. Les panels contiennent aussi une variable position_x et position_y qui permettent de connaître le control actuellement sélectionné par l’utilisateur. Quand l’utilisateur décide de se déplacer, il demande au panel de modifier ses variables x et y. Avant de valider ce changement, le panel regarde si le déplacement demandé par l’utilisateur est possible ou non.

Il existe 3 possibilités:



1. Le déplacement est possible, alors la position sur l’axe x,y est modifiée.
2. Le déplacement est impossible car il n’y a rien à l'emplacement demandé. Dans ce cas, le panel va décider de bouger le curseur sur un des emplacements possibles.
3. L’utilisateur est à la fin du panel et “sors du panel” dans ce cas il va se diriger dans un autre panel s’il y en a un dans la direction demandée.


### Déplacement de panel en panel

Chaque “panel” possède un pointeur sur le panel du haut, du bas, de droite et de gauche.

Ces pointers ne sont pas forcément utilisés, ils ont le droit d'être nul.

Si on prend l'exemple suivant: 

![alt_text](images/exemple_3_panel.jpeg "image_tooltip")


Nous avons 3 panels différents qui contiennent chacun plusieurs controls.

Le panel 1 possède donc deux pointeurs différents, un sur le panel 2 et un autre sur le panel 3.

trop confus 

Si l’utilisateur se trouve en bas du panel 1 et qu’il décide de se déplacer encore plus  bas, il ne pourra pas car aucune case n’est disponible dans ce panel. C’est pourquoi une vérification  sera faite pour savoir si un panel est indiqué comme le panel “down”, si tel est le cas le focus va changer de panel.

Un autre cas possible est que l’utilisateur va peut-être décider de retourner sur le panel 1. La contrainte est de savoir où doit pointer le panel haut du panel 3. Actuellement, un seul panel peut être défini par côté, mais la solution est de créer de petits panels pour éviter que ces situations arrivent.

## Serveur Debian 10

J’ai utilisé un serveur sous Debian10 pour héberger le site web de caiman, l’API et les fichiers des différents jeux.


### Configuration de PHP

J’ai dû installer PHP pour le site web et l’API. Donc, je vais détailler la configuration que j’ai faite pour Caiman.



*   version 7.4
*   max_input_time = 600
*   max_execution_time = 300
*   post_max_size = 8G
*   upload_max_filesize = 10G

J’ai dû grandement augmenter la taille d’upload pour pouvoir uploader les fichiers des jeux sur le serveur.


### Modification des droits des dossiers

J’ai modifié les droits d’écriture pour le dossier où les images sont stockées et les dossiers où sont stockés les fichiers des jeux. Le dossier où sont stockées les sauvegardes des utilisateurs a lui aussi eu ces droits modifiés.

Chaque émulateur qui est supporté par caiman a un dossier ou les jeux compatibles sont stockés. Le dossier pour les sauvegardes contient les sauvegardes pour chaque émulateur. Il n'est donc pas nécessaire de créer plusieurs sous-dossiers.


### Services installé

J’ai aussi installer un service FTP pour pouvoir transférer les fichiers sur le serveur plus facilement que par ssh. J’ai aussi installé mySQL et apache2.


### Création de virtual host

Pour simplifier l'accès au site de Caiman et à l'API, les deux ne possèdent pas la même URL. Pour cela, j’ai dû configurer un sous-domaine pour l’API. Le nom de domaine de “base” est “caiman.cfpt.info” mais j’ai créé le sous-domaine “api.caiman.cfpt.info” pour les requêtes à l’API.

Le chemin où pointe “caiman.cfpt.info“ est “/var/www/caimanWeb/www” et le chemin pour “api.caiman.cfpt.info” est “/var/www/caimanAPI/public”. Pour créer les deux domaines j’ai dû modifier le fichier “/etc/apache2/sites-enabled/000-default.conf”. et ajouter les deux chemin et leur nom.

