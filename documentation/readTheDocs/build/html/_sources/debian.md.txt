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

