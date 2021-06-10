# Analyse organique


## Technologies utilisées


### HTML


### CSS


### PHP

J’ai utilisé le php pour le site web de Caiman ainsi que pour l’API.


### C#

L’application Caiman a été développé en C#, j’ai utilisé les paquets suivants:



*   Microsoft.ASPNet.WEbApi.Client
*   Newtonsoft.Json
*   SharpDX


## Architecture du projet

Le projet se divise en 3 parties: l'application Caiman pour windows, le site web et l’API. Chaque partie du projet contient une documentation doxygene qui lui est propre.


### Caiman

Je vais expliquer ci-dessous l'arborescence des fichiers de l’application Caiman et comment les émulateurs y sont intégrés. 

![alt_text](images/structure_caiman.png "image_tooltip")


Je vais expliquer ce que contiennent les différents dossiers.

**Database**

Contient les classes servant à appeler la base de données.

**interfaceG**

Ce dossier contient les fichiers de base de l’affichage.

**interfaceG\usercontrol**

Ce dossier contient les différents panels qui sont utilisés dans Caiman pour créer l’affichage.

**interfaceG\XboxControl**

Ce dossier contient une version modifiée de plusieurs contrôles de base de Windows Form.

**logique**

Ce dossier contient les classes servant à gérer les émulateurs et les sauvegardes et plus généralement toute la logique de l'application.

**models**

Ce dossier contient les modèles pour simplifier l’interaction entre la base de données et Caiman.

**emulators**

Le dossier est bien présent mais n’est pas visible sur la photo. Ce dossier contient tous les fichiers des émulateurs.


### Site web

Le site web contient en plus des fichiers du site les fichiers des jeux et des sauvegardes des utilisateurs.

![alt_text](images/dossier_caimanweb.png "image_tooltip")


**games**

Ce dossier contient dans un sous dossier spécifique à chaque émulateur les fichiers des jeux.

**release**

Ce dossier contient le fichier .zip de la version téléchargeable de Caiman depuis le site.

**saves**

Ce dossier contient les fichiers de sauvegardes des utilisateurs.

**www**

Ce dossier contient les fichiers du site web de Caiman.

![alt_text](images/dossier_caimanweb_WWW.png "image_tooltip")


**common**

Ce dossier contient les fichiers de base de la vue  comme le footer ou le header.

**controllers**

Ce dossier contient les différents controllers de l’application.

**css**

contient les fichiers de css

**img**

contient les images des jeux

**models**

contient les fichiers pour simplifier l'accès à la base de données

### API

![alt_text](images/dossier_api_caiman.png "image_tooltip")

Le dossier de base de l’api contient deux dossiers importants .


#### app

![alt_text](images/dossier_api_caiman_app.png "image_tooltip")

**Controllers**

 Ce dossier contient les différents controllers de l’API.

**DataAccessObject**

Ce dossier contient les différents fichiers servant à exécuter des requêtes à la base de données.

**Models**

Ce dossier contient les modèles utilisés par l’API.

**System**

Ce dossier contient des fichiers servant à se connecter à la base de données.


#### public

Je ne vais pas développer davantage sur le contenu du dossier, en sachant que le détail des différents endpoint est disponible dans la documentation. Néaumoins, ce dossier comporte les points d'accès de l’API.

## Description technique: Site web


### Création de compte

Caiman propose aux utilisateurs de se créer un compte.Se compte est commun au site internet et à l’application. Pour pouvoir se créer un compte, il faut renseigner plusieurs champs.



*   un nom d’utilisateur
*   un mail
*   un mot de passe
*   un validation du mot de passe

Dans l’état actuel de l’application, l’émail n’est pas utilisé.

```PHP
    // check if email alredy used
    $sqlrequestEmail = "SELECT * FROM user WHERE email = :search_email ";
    $this->psCheckEmail = $this->dbh->prepare($sqlrequestEmail);
    $this->psCheckEmail->setFetchMode(PDO::FETCH_ASSOC);

    // check if username alredy used
    $sqlRequestUsername = "SELECT * FROM user WHERE username = :search_username ";
    $this->psCheckUsername = $this->dbh->prepare($sqlRequestUsername);
    $this->psCheckUsername->setFetchMode(PDO::FETCH_ASSOC);

    $sqlInsert = "INSERT INTO user  (username, password, email, salt,apitocken)
    
    VALUES (:insert_username, :insert_password, :insert_email, :insert_salt, :insert_apiTocken)";
    $this->psInsert = $this->dbh->prepare($sqlInsert);
    $this->psInsert->setFetchMode(PDO::FETCH_ASSOC);
```


### Connexion

Pour se connecter au site web de caiman, il faut renseigner son nom d’utilisateur et son mot de passe.

```PHP
 $this->psLogin->execute(array(':search_username' => $this->search_username));
            $result = $this->psLogin->fetchAll();
            if ($result != null) {
                if (md5($result[0]["salt"].$password_verify) == $result[0]["password"]  ) {
                    $returnArray = $result;
                    $_SESSION['error'] = "Welcome back: ". $result[0]['username'];
                }else
                {
                    $_SESSION['error'] = "Invalid log in";
                }
            }
```


### Fonctionnalité disponible une fois connecté

Quand un utilisateur se connecte à l’application, il a accès à de nouvelles fonctionnalités. L’utilisateur a maintenant la possibilité de modifier son mot de passe, ajouter/supprimer des jeux de ses favoris et de modifier la visibilité de son profil (si le profil est visible, il sera affiché dans la liste des utilisateurs).

```php
public function updatePassword(string $newPassword, string $newPasswordRepeat, string $oldPassword)
    {
        $dbh = new PDO('mysql:host=' . HOST . ';dbname=' . DBNAME, USER, PASSWORD, array(
            PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8",
            PDO::ATTR_PERSISTENT => true
        ));

        $hasBeenUpdated = 1;
        if (md5($_SESSION['user']->salt.$oldPassword) == $this->getUserPassword()) {

            if ($newPasswordRepeat == $newPassword) {
                try {
                    $salt = rand(1,10000);
                    $sqlUpdatePassword = "UPDATE user  SET password = :update_password, salt = :update_salt WHERE id = :id_user";
                    $psUpdatePassword = $dbh->prepare($sqlUpdatePassword);
                    $psUpdatePassword->execute(array(':update_password' => md5($salt.$newPassword), ':id_user' => $this->idUser, ':update_salt' => $salt));

                    $hasBeenUpdated = 0;
                } catch (PDOException $e) {
                    print "Erreur !: " . $e->getMessage() . "<br>";
                    die();
                }
            } else {
                $hasBeenUpdated = 2;
            }
        } else {
            $hasBeenUpdated = 4;
        }

        return $hasBeenUpdated;
    }

```


### Ajout de jeu

L’ajout de jeu ne peut se faire que par le site et seulement par les administrateurs du site.

Pour pouvoir ajouter un jeu, il faut au préalable avoir sur le disque dur de l’administrateur un fichier .iso du jeu, il faut aussi envoyer une image. Cette image sera affichée sur le site web et dans l'application Caiman.

Pour pouvoir envoyer le formulaire, il faut remplir les champs suivants:



*   nom du jeu
*   description
*   nom de l’image(le nom doit respecter un certain format)
*   le fichier de l’image
*   la console sur lequel le jeu tourne
*   le nom du fichier du jeu(le nom doit respecter un certain format)
*   le fichier du jeu

Le serveur est configuré pour recevoir des fichiers jusqu'à 8GB. Donc pour les fichiers de Gamecube qui ne peuvent faire que 1.4GB et les fichiers de Playstation 2 qui peuvent faire jusqu'à 7 GB, il n’y a pas de problèmes.

```PHP
public function uploadGame($gameFileName, $consoleId)
    {
        $uploadIsValid = false;
        $target_dir = "../games/" . $this->getConsoleFolderName($consoleId) . "/";

        $target_file =  basename($_FILES["fileGame"]["name"]);
        $uploadOk = 1;
        $fileType = strtolower(pathinfo($target_file, PATHINFO_EXTENSION));

        //rename file
        $newfilename = $gameFileName ;


        // Check if file already exists
        if (file_exists($target_file)) {
            echo "Sorry, file already exists.";
            $uploadOk = 0;
        }
        if ($uploadOk == 0) {
            echo "Sorry, your file was not uploaded.";
            // if everything is ok, try to upload file
        } else {
            if (move_uploaded_file($_FILES["fileGame"]["tmp_name"], $target_dir . $newfilename)) {
                $uploadIsValid = true;
            } else {
                //Sorry, there was an error uploading your file
            }
        }
        return $uploadIsValid;
    }
```


### Ajout / suppression de catégories au jeux

Chaque jeu peut appartenir à une ou plusieurs catégories. Seuls les administrateurs ont le droit d’ajouter des catégories aux jeux.

Pour pouvoir modifier les catégories d’un jeu, il faut qu'un administrateur aille sur la page du jeu qui doit être modifiée.

```php
public function addCategorieToGame(int $idGame, int $idCategorie)
    {
        $result = null;
        try {
            $this->psCheckIfGameHasCategorie->execute(array(':insert_idCategorie' => $idCategorie, ':insert_idGame' => $idGame));
            $result = $this->psCheckIfGameHasCategorie->fetchAll();
        } catch (PDOException $e) {
            print "Erreur !: " . $e->getMessage() . "<br>";
            die();
        }
        if ($result == null) {
            try {
                $this->psAddCategorieToGame->execute(array(':insert_idCategorie' => $idCategorie, ':insert_idGame' => $idGame));
            } catch (PDOException $e) {
                print "Erreur !: " . $e->getMessage() . "<br>";
                die();
            }
        }
    }
```


### Ajout de catégories

La liste des catégories est définie par les administrateurs, ils ont la possibilité d’en ajouter depuis le menu d’administration. Pour ajouter une catégorie, il suffit de spécifier le nom que va porter la nouvelle catégorie.

```php
public function addCategorie(string $categorieName)
    {
        try {
            $this->psAddCategorie->execute(array(':categorie_name' => $categorieName));
            $result = $this->psAddCategorie->fetchAll();
        } catch (PDOException $e) {
            print "Erreur !: " . $e->getMessage() . "<br>";
            die();
        }
        return $result;
    }
```


### Téléchargement de Caiman

Le téléchargement de Caiman se fait sous forme de fichier .zip. Pour pouvoir télécharger l’application, il faut être authentifié. Actuellement, pour pouvoir modifier le fichier caiman.zip que l'utilisateur télécharge, il faut passer par le FTP. Le site web ne permet pas de le mettre à jour.

```php
public function downloadCaiman()
    {

        $zipFile = "../release/Caiman.zip";

        $file_name = basename($zipFile);

        header("Content-Type: application/zip");
        header("Content-Disposition: attachment; filename=$file_name");
        header("Content-Length: " . filesize($zipFile));

        readfile($zipFile);
        exit;
    }
}
```


### Modification des jeux

Actuellement, il est possible de modifier le nom et la description d'un jeu.

## Description technique: API


### Téléchargement de jeu

Les jeux sont sous forme de fichier .iso dans le dossier “caimanWeb\games\”.Par contre, ce n’est pas tout pour simplifier, il faut bien séparer les jeux des différentes consoles. J’ai décidé de créer des dossiers par émulateurs. C’est pourquoi il va y avoir des sous-dossiers “GamecubeWii\” et “Playstation2\”. Chaque jeu est stocké avec le nom que l’administrateur lui aura donné quand il l’a ajouté depuis le site web.

Le chemin sur le serveur web pour accéder au fichier des jeux n’est pas public. Par conséquent, il a donc fallu que je trouve une solution pour que n’importe qui ne puisse pas télécharger un jeu. Pour ce faire, j'ai créé une route dans mon API qui prend en paramètres l’id du jeu et l’apiKey de l’utilisateur qui veut télécharger le jeu.

La fonction getURL(idGame,apiKey) permet de recevoir le lien de téléchargement pour un jeu. Avant de valider le téléchargement, l’API vérifie que l’apiKey qui lui a été donné est valide. Pour savoir si l’apiKey est valide, j'utilise la fonction DAOUser->Find(apikey) qui me retourne l’utilisateur en lien avec cette apiKey. Si l’apiKey est valide, je vais donc devoir reconstituer le chemin vers le dossier ou est le jeu stocké.

Pour connaître le nom du dossier où se trouve le fichier, il faut savoir de quelle console est le jeu. Pour connaître cette information, je dois rechercher un jeu grâce à son id. Quand j’ai l’id de la console en lien avec le jeu, je dois encore faire une recherche pour savoir quel est son nom de dossier dans la base de données. Alors j'utilise la fonction DAOConsole->find(idConsole) pour connaître son nom de dossier.

Désormais que nous avons le dossier dans lequel se trouve le fichier du jeu, il faut maintenant connaître son nom de fichier. Pour cela, j'utilise la fonction DAOFile->find(file). l’id du fichier est connu grâce à l’appel la fonction DAOGame->find().

Â présent que nous avons toutes les parties du chemin, il est possible de construire le chemin complet en concaténant toutes les parties. Ensuite pour renvoyer le jeu à l'utilisateur, j’utilise les fonctions fopen(path,’rb’) et fpassthru(fopen).

```php
public function getURL(int $idGame, string $apikey)
    {

        $user = $this->DAOUser->find($apikey);
        $fullpath = "";
        if (is_null($user)) {
            return ResponseController::notFoundResponse();
        } else {
            $game = $this->DAOGame->find($idGame);
            $file = $this->DAOFile->find($game->idFile);
            $console = $this->DAOConsole->find($game->idConsole);
            $fullpath = "../../../../caimanWeb/games/" . $console->folderName . "/" . $file->filename;


            if (file_exists($fullpath)) {
                header('Content-Description: File Transfer');
                header('Content-Type: application/octet-stream');
                header('Content-Disposition: attachment; filename=' . basename($fullpath));
                header('Content-Transfer-Encoding: binary');
                header('Expires: 0');
                header('Cache-Control: must-revalidate, post-check=0, pre-check=0');
                header('Pragma: public');
                header('Content-Length: ' . filesize($fullpath));
                $fp = fopen($fullpath, 'rb');
                fpassthru($fp);
                exit;
            }
        }

        return ResponseController::successfulRequest($fullpath);
    }
```


### Téléchargement de sauvegarde

Les sauvegardes des utilisateurs sont stockées sous forme de fichiers .zip. L'intérêt d’utiliser des fichiers .zip est la taille et le fait qu'un fichier puisse contenir toutes les sauvegardes instantanément. Les fichiers de sauvegardes se trouvent dans le dossier “\CaimanWeb\saves\”.

Le nom attribué au fichier est décidé au premier envoie de sauvegardes, le nom est le md5 du microtime de l’heure d’envoie. 

Pour recevoir le fichier, l’API possède une route qui prend les paramètres suivants:



*   idEmulator
*   iduser
*   apikey

Cette route va envoyer le fichier demandé. Comme pour les fichiers des jeux, les sauvegardes sont dans un dossier privé du serveur. La fonction getURLsave( idEmulator, Iduser,apikey) se charge de renvoyer les fichiers à l'utilisateur. Pour faire ça il faut connaître le nom du fichier qui doit être envoyé. Il faut donc utiliser la fonction DAOFileSave->find( idemulator, idUser) pour connaître les informations du fichier.

Désormais que le nom du fichier est connu, il est possible de retourner le fichier grâce à la fonction fopen(path,’rb’) et fpassthru(fopen) pour renvoyer le fichier à l'utilisateur.

```php
public function getURLSave(int $idEmulator, int $idUser, string $apikey)
    {

        $user = $this->DAOUser->find($apikey);
        $fullpath = "";
        if (is_null($user)) {
            return ResponseController::notFoundResponse();
        } else {
            $fileSave = $this->DAOFileSave->find($idEmulator, $idUser);

            if (is_null($fileSave)) {
                return ResponseController::notFoundResponse();
                exit;
            }
            $file = $this->DAOFile->find($fileSave->idFile);
            $fullpath = "../../../../caimanWeb/saves/" . $file->filename;


            if (file_exists($fullpath)) {
                header('Content-Description: File Transfer');
                header('Content-Type: application/zip');
                header('Content-Disposition: attachment; filename=' . basename($fullpath));
                header('Content-Transfer-Encoding: binary');
                header('Expires: 0');
                header('Cache-Control: must-revalidate, post-check=0, pre-check=0');
                header('Pragma: public');
                header('Content-Length: ' . filesize($fullpath));
                $fp = fopen($fullpath, 'rb');
                fpassthru($fp);
                exit;
            }
        }

        return ResponseController::successfulRequest();
    }
```

Dans l’état actuel de l’applicationCaiman, il est impossible de télécharger seulement la sauvegarde d'un seul jeu, la structure des sauvegardes des émulateurs ne laisse pas vraiment le choix.


### Upload de sauvegarde

Pour enregistrer les sauvegardes, il y a une route dans l’API qui prend en paramètres:



*   idEmulator
*   iduser
*   apikey
*   file

Les fichiers de sauvegarde concernent un émulateur àchaque fois, donc il faut le spécifier à l'envoi. Il faut aussi spécifier l’utilisateur à qui appartiennent ces sauvegardes, il faut aussi transmettre une apiKey valide et finalement le fichier à envoyer.

Quand l’API reçoit ces informations, il faut déjà vérifier si l’apiKey reçu est bien valide. Si elle est valide, il faut ensuite vérifier si l'utilisateur a déjà créé une sauvegarde pour cet émulateur. Pour effectuer cette vérification, il faut utiliser la fonction DAOFileSave->find(idEmulator,idUser) qui retourne les informations du fichier s’il existe.

```php
public function AddSave($idEmulator, $idUser, $apiKey, $file)
    {

        if (is_null($idEmulator) || is_null($idUser) || is_null($apiKey)) {
            return ResponseController::notFoundResponse();
            exit;
        }
        $user = $this->DAOUser->find($apiKey);

        if ($user == null) {
            return ResponseController::notFoundResponse();
            exit;
        }
        $isNewFile = false;
        $newfilename = $this->DAOFileSave->FindFileName($idEmulator, $idUser);

        if ($newfilename == null) {
            $newfilename = md5(microtime());
            $newfilename = $newfilename . ".zip";
            $isNewFile = true;
        }
        $target_dir = "../../../../caimanWeb/saves/";

        if (move_uploaded_file($file, $target_dir . $newfilename)) {
            if ($isNewFile) {
                $this->DAOFileSave->AddFileSave($idEmulator, $idUser, $newfilename);
            }
        } else {
            return ResponseController::uploadFailed();
            exit;
        }

        return ResponseController::successfulRequest();
    }
```


#### Sauvegarde déjà présente

Si la sauvegarde d’un émulateur est déjà présente, il faut utiliser la fonction move_uploaded_file(file,target_dir). Tout d’abord il faut connaître le nom du fichier qui est attribué à la sauvegarde. Pour faire cela, il faut appeler la fonction DAOFileSave->findFilename(idEmulator,idUser) celle-ci va renvoyer le nom du fichier.Il suffit maintenant d’écraser le fichier présent sur le serveur par le nouveau.

```php
    public function findFileName(int $idEmulator, int $idUser)
    {

        $statement = "
    SELECT f.filename filesave FROM `filesave` as fs
                LEFT JOIN file as f
                ON fs.idFile = f.id
                WHERE idUser = :ID_user AND idEmulator = :ID_emulator;";
        try {
            $statement = $this->db->prepare($statement);
            $statement->bindParam(':ID_user', $idUser, \PDO::PARAM_INT);
            $statement->bindParam(':ID_emulator', $idEmulator, \PDO::PARAM_INT);
            $statement->execute();

            $file = new File();

            if ($statement->rowCount() == 1) {
                $result = $statement->fetch(\PDO::FETCH_ASSOC);
                $file->filename = $result['filesave'];
            }
            return $file->filename;
        } catch (\PDOException $e) {
            exit($e->getMessage());
        }
    }
```


#### Sauvegarde pas encore présente

S’il n’y a aucune sauvegarde pour l’émulateur en question et l’utilisateur en question. Il faut commencer par donner un nom au fichier. Ce nom est constitué du MD5 du microtime() et de l'extension “.zip”. Quand le nom pour le fichier aété créé, il faut uploader le fichier dans le dossier de sauvegarde grâce à la fonction move_uploaded_file(file,path). 

Quand l’upload est validé, un ajout de fichier de sauvegarde va se faire grâce à la fonction DAOFileSave->AddFileSave(idEmulator,idUser,newfilename). Cette fonction va créer une entrée dans la base de données pour le nouveau fichier.

```php
public function AddFileSave($idEmulator, $idUser, $newFileName)
    {
        $statementFile = "
        INSERT INTO file
        (filename, dateUpdate)
        VALUES
        (:FILENAME, NOW())";
        try {
            $statementFile = $this->db->prepare($statementFile);
            $statementFile->bindParam(':FILENAME', $newFileName, \PDO::PARAM_STR);
            $statementFile->execute();
            //when create file is done
            $statementFileSave = "
            INSERT INTO filesave
            (idUser, idEmulator,idFile)
            VALUES
            (:ID_user,:ID_emulator, :ID_file)";
            try {
                $statementFileSave = $this->db->prepare($statementFileSave);
                $statementFileSave->bindParam(':ID_user', $idUser, \PDO::PARAM_INT);
                $statementFileSave->bindParam(':ID_emulator', $idEmulator, \PDO::PARAM_INT);
                $lastInsertId = $this->db->lastInsertId();
                $statementFileSave->bindParam(':ID_file', $lastInsertId, \PDO::PARAM_INT);
                $statementFileSave->execute();
            } catch (\PDOException $e) {
                exit($e->getMessage());
            }
        } catch (\PDOException $e) {
            exit($e->getMessage());
        }
    }
```


### Mise à jour du caimanToken

Le caimanToken sert à pouvoir se connecter sans mot de passe à l'application Caiman. Se token doit être changée à chaque fois que l’utilisateur se connecte, que ce soit en se connectant avec un mot de passe ou par le caimanToken justement.

Pour ce faire, j’ai créé une function DAOUser->updateCaimanToken(apiToken) qui sert à modifier le caimanToken d’un utilisateur dans la base de données. Le caimanToken est un md5 du microtime actuel. Par conséquent, il est presque impossible que deux utilisateurs aient le même. 

```php
public function updateCaimanToken(string $apitocken)
    {
        $statement = "
        UPDATE user
        SET caimanToken = :CAIMAN_TOKEN
        WHERE apitocken = :API_TOCKEN;";

        try {
            $statement = $this->db->prepare($statement);
            $statement->bindParam(':API_TOCKEN', $apitocken);
            $caimanTocken = md5(microtime());
            $statement->bindParam(':CAIMAN_TOKEN', $caimanTocken);
            $statement->execute();
        } catch (\PDOException $e) {
            exit($e->getMessage());
        }
    }
```

## Description technique: Application Caiman

Diagramme de classe

![alt_text](images/DiagramLogique.png "image_tooltip")



### Création de dossier


#### Structure des dossiers

Caiman nécessite de créer certains dossiers et fichiers pour pouvoir fonctionner correctement. Ces dossiers se trouvent dans le dossier “%appdata%\Roaming\Caiman” 

![alt_text](images/dossier_appdata_caiman.png "image_tooltip")


**Caiman**

Ce dossier contient les fichiers de configuration graphique, la liste des jeux téléchargés et le dernier CaimanToken reçu.

**img**

Ce dossier contient les différentes images des jeux, ces images sont téléchargées sur le site internet de caiman.

**users**

Ce dossier contient les dossiers personnels de chaque utilisateur de l'application qui s’est connecté sur le pc. Il possède les fichiers de sauvegarde de l’utilisateur en question.


#### Création des dossiers

**Dossier de base**

Les dossiers pour caiman doivent obligatoirement être créés. Donc au début du lancement de l’application Caiman, il y a toujours une vérification pour savoir si les dossiers sont bien présents. Si c’est le premier lancement de Caiman ou si les dossiers ont été supprimés, ils vont être créés.

```C#
private void CreateAppDataFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var basePath = Path.Combine(appDataPath, @"Caiman\");
            var caimanConfigPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            var imgPath = Path.Combine(appDataPath, @"Caiman\img\");
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\games.ini");
            var configPath = Path.Combine(appDataPath, @"Caiman\Caiman\config.ini");
            var loginPath = Path.Combine(appDataPath, @"Caiman\Caiman\login.ini");

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }
            if (!Directory.Exists(caimanConfigPath))
            {
                Directory.CreateDirectory(caimanConfigPath);
            }
            if (!Directory.Exists(@"C:\Caiman\Playstation2"))
            {
                Directory.CreateDirectory(@"C:\Caiman\Playstation2\");
            }
            if (!Directory.Exists(@"C:\Caiman\GamecubeWii\"))
            {
                Directory.CreateDirectory(@"C:\Caiman\GamecubeWii\");
            }
            if (!File.Exists(loginPath))
            {
                using (StreamWriter sw = File.CreateText(loginPath))
                {
                    sw.WriteLine("token = 0");
                }
                loginFile = new ConfigFileEditor(caimanConfigPath, "login.ini");
            }
            if (!File.Exists(gamesPath))
            {
                using (StreamWriter sw = File.CreateText(gamesPath))
                {

                }
                configFile = new ConfigFileEditor(caimanConfigPath, "config.ini");
            }
            if (!File.Exists(configPath))
            {
                //if the file do not exist create it with with the default emulators value
                using (StreamWriter sw = File.CreateText(configPath))
                {
                    sw.WriteLine("configuration = 1080");
                    sw.WriteLine("fullscreen = true");
                    sw.WriteLine("definition = 3");
                    sw.WriteLine("formatSeizeNeuvieme = true");
                    sw.WriteLine("filtrageAnioscopique = 3");
                }
                configFile = new ConfigFileEditor(caimanConfigPath, "config.ini");

            }


        }
```

**Dossier des utilisateurs**

La création des dossiers des utilisateurs pour gérer correctement les sauvegardes. Ces dossiers sont créés à la première connexion de l’utilisateur.

```C#
 public void CreateUserFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userPath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\");
            var savePath = Path.Combine(userPath, @"Save\");
            var savePathPlaystation = Path.Combine(savePath, @"Playstation2\");
            var savePathGamecubeWii = Path.Combine(savePath, @"GamecubeWii\");

            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            if (!Directory.Exists(savePathPlaystation))
            {
                Directory.CreateDirectory(savePathPlaystation);
            }
            if (!Directory.Exists(savePathGamecubeWii))
            {
                Directory.CreateDirectory(savePathGamecubeWii);
            }

        }
```


### Verification des jeux présent sur le disque

Pour connaître les jeux qui ont été téléchargés, il existe un fichier comprenant les ids des jeux. Ce fichier se trouve dans le dossier “appdata” de Caiman. Il est donc commun à tous les utilisateurs. Ce fichier est mis à jour quand un utilisateur a fini de télécharger un jeu ou quand il décide d’en supprimer un.

```C#
private void CheckIfGameFileIsPresentOnDisk()
        {
            List<string> lst_idGames = gamesListConfigFile.GetAllValueInList();

            foreach (var idGameString in lst_idGames)
            {
                if (idGameString != "")
                {
                    int idGame = Convert.ToInt32(idGameString);
                    if (!File.Exists(@"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\" + callAPI.CallFileNameGame(idGame)))
                    {
                        gamesListConfigFile.DeleteValue(idGameString);
                    }
                }

            }
        }
```

Si l’utilisateur décide de supprimer un fichier sans passer par caiman il pourrait y avoir un souci alors pour pallier à ce problème une vérification est faite. Au lancement de l’application, si un fichier est manquant, alors le fichier qui contient les id des jeux sera mis à jour.



### Paramètres graphique


#### Liste des paramètres graphique

L’utilisateur a la possibilité de modifier plusieurs paramètres:

La configuration global de l’émulateur



*   Original
*   1080p
*   4K

Si le jeu doit se lancer en mode plein écran



*   true
*   false

Si le jeu doit se lancer en 16/9



*   true
*   false

Les différents modes graphiques modifient ces paramètres:



*   le filtrage anisotropique
*   l’upscale de la définition

Le filtrage anisotropique permet entre autres de diminuer l'effet de crénelage, ainsi qu’à améliorer l’affichage des textures vue depuis un angle de vue extrême.

L’upscale de la définition permet d'augmenter la définition native du rendu. Par exemple, la définition de base de la Playstation 2 sur le jeu Kingdom Heart( 512x448). Cette définition est donc la définition que l’utilisateur va avoir en “Original”, mais s’il opte pour le paramètre 1080p, il va avoir une définition native de 1536x1344. En mode 4K le rendu sera en définition 4096x3584.


#### Fichiers de configuration PCSX2

Pour l'émulateur PCSX2, il y a deux fichiers de configuration qui doivent être modifiés.

Les fichiers se trouvent dans le dossier “PCSX2\inis\”.

Le fichier “GSDX.ini” permet de modifier les paramètres de d’upscale et de filtrage anisotropique.

Le fichier “PCSX2_ui.ini” permet de modifier l’affichage en plein écran et le format d’affichage.

Le paramètre de format d’écran dans le fichier de configuration de PCSX n’est pas un booléen mais du texte je dois avant l'écriture dans le fichier le convertir.

Pareil pour le paramètre de mode plein écran 

```C#
public override void UpdateConfigurationFile()
        {
            configFileGSdx.UpdateProperties("upscale_multiplier", definition.ToString());
            configFileGSdx.UpdateProperties("MaxAnisotropy", filtrageAnioscopique.ToString());

            // the format is not true or false so i have to format it
            if (fullScreen)
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "enabled");
            }
            else
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "disabled");
            }

            // the format is not true or false so i have to format it 
            if (formatSeizeNeuvieme)
            {
                configFilePCSX2_ui.UpdateProperties("AspectRatio", "16:9");
            }
            else
            {
                configFilePCSX2_ui.UpdateProperties("AspectRatio", "4:3");
            }
        }
```


#### Fichiers de configuration Dolphin

Pour l'émulateur Dolphin, il y a deux fichiers de configuration qui doivent être modifiés.

Les fichiers se trouvent dans le dossier “Dolphin\User\Config\”.

Le fichier “GFX.ini” permet de modifier les paramètres de d’upscale et de filtrage anisotropique.

Le fichier “Dolphin.ini” permet de modifier l’affichage en plein écran et le format d’affichage.

Le paramètre de format d’écran dans le fichier de configuration de Dolphin n’est pas un booléen mais un nombre. Donc avant l'écriture dans le fichier je dois le convertir.

```C#
public override void UpdateConfigurationFile()
        {
            configFileDolphin.UpdateProperties("Fullscreen", fullScreen.ToString());
            configFileGFX.UpdateProperties("InternalResolution", definition.ToString());
            configFileGFX.UpdateProperties("MaxAnisotropy", filtrageAnioscopique.ToString());

            if (formatSeizeNeuvieme)
            {
                configFileGFX.UpdateProperties("AspectRatio", "1");
            }
            else
            {
                configFileGFX.UpdateProperties("AspectRatio", "2");
            }
            
        }
```


#### Sauvegarde des paramètres graphique

Les paramètres graphiques définis par l’utilisateur se trouvent dans le dossier “appdata\Caiman\Caiman\config.ini”.

Les paramètres sont mis à jour grâce à la classe ConfigFileEditor, cette classe permet la manipulation de fichier “.ini” que ce soit la lecture,la modification, la suppression, la création de propriétés.

```C#
public void ApplyGlobalConfiguration(string configuration)
        {
            switch (configuration)
            {
                case "original":
                    configFile.UpdateProperties("definition", "1");
                    configFile.UpdateProperties("filtrageAnioscopique", "1");
                    configFile.UpdateProperties("configuration", "original");
                    ScanConfiguration();
                    break;
                case "1080":
                    configFile.UpdateProperties("definition", "3");
                    configFile.UpdateProperties("filtrageAnioscopique", "4");
                    configFile.UpdateProperties("configuration", "1080p");
                    ScanConfiguration();
                    break;
                case "4K":
                    configFile.UpdateProperties("definition", "8");
                    configFile.UpdateProperties("filtrageAnioscopique", "4");
                    configFile.UpdateProperties("configuration", "4K");
                    ScanConfiguration();
                    break;
                default:
                    break;
            }
        }
```


### Téléchargement d’images

Le téléchargement des images se fait à la création d’un objet de type Game. Le téléchargement se fait à partir du site web de Caiman “caiman.cfpt.info”. Les fichiers sont téléchargés dans le dossier appdata. L'intérêt de stocker les images dans ce dossier est de créer un système de cache qui sera accessible à tous les utilisateurs.  \
 \
Le téléchargement se fait grâce à un WebClient. Avant tout téléchargement de fichier, Caiman vérifie si le fichier n’est pas déjà existant, ce qui permet d'éviter de télécharger plusieurs fois la même image.

```C#
private void DownloadImage()
        {

            if (!File.Exists(imgPath + imageName))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(URL_IMAGES_CAIMAN + imageName), (imgPath + imageName));
                }
            }
        }
```


### Exécution de jeu


#### Choix du jeu à lancer

Le choix du jeu se fait à partir de la page de visualisation des détails d’un jeu. Quand l’utilisateur clique sur le bouton “Play” l’id du jeu qui doit être exécuté est envoyé à la fonction EmulatorManager.StartGame(idGame).

La fonction va ensuite faire un appel à l'API pour connaître de quelle console est le jeu qui doit être exécuté et selon la console un émulateur différent sera utilisé.


#### Emulateur embarqué avec Caiman

Caiman embarque deux émulateurs PCSX2 et Dolphin ces émulateurs servent respectivement à l'exécution de jeux de Playstation 2 et de Gamecube/Wii. L'exécution n’est pas la même selon l’émulateur, alors je vais détailler pour chacun. Avant l'exécution d’un jeu, l'application des paramètres graphiques est faite mais je ne vais pas le détailler ici.


#### Exécution avec PCSX2

En premier lieu, je vais recréer le chemin qui mène au fichier qui doit être exécuté par exemple “C:\Caiman\Playstation2\DRAGON_QUEST_VIII.iso”. Par la suite, je vais créer une variable pour les différents paramètres, dans la version de caiman actuel on ne peut pas changer ces paramètres se sera donc toujours “ --nogui --portable”.

Le premier paramètre fait en sorte que l’émulateur n’affiche pas d’interface graphique, seule la fenêtre d'exécution du jeu est visible. Le deuxième paramètre sert à dire à PCSX2 que les fichiers de configurations qui doivent être utilisés sont ceux du dossier de l’émulateur, et non ceux du dossier créé pour chaque utilisateur windows par PCSX2.

Pour lancer l'exécution, je vais lancer le processus de PCSX2.exe avec en premier paramètre le chemin de l’iso à exécuter et après les paramètres cités précédemment.

```C#
public override void Execute(int idGame)
        {
            string path = @"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\";
            string filename = callAPI.CallFileNameGame(idGame);
            UpdateConfigurationFile();
            int process = Process.GetProcessesByName(PROCESS_NAME).Length;

            if (process == 0)
            {
                //param pour ne pas afficher l'interface graphique --portable --nogui
                string param = " --nogui --portable";

                processEmulator = Process.Start(PCSX2Folder + EXE_NAME, path + filename + param);

            }
            else
            {
                Close();
                Execute(idGame);
            }
        }
```


#### Exécution avec Dolphin

Ensuite, je vais recréer le chemin qui mène au fichier qui doit être exécuté par exemple “C:\Caiman\GamecubeWii\METROID_PRIME.iso”. Je vais également créer une variable pour les différents paramètres, dans la version de caiman actuel on ne peut pas changer ces paramètres, il sera toujours “ --batch”.

Le paramètre “--batch” fait en sorte de ne pas afficher l’interface de Dolphin, et donc d’avoir seulement la fenêtre d'exécution du jeu.

Pour lancer l'exécution, je vais lancer le processus de PCSX2.exe avec en premier paramètre le chemin de l’iso a exécuter et après le paramètre cité précédemment.

```C#
public override void Execute(int idGame)
        {
            string path = @"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\";
            string filename = callAPI.CallFileNameGame(idGame);
            
            UpdateConfigurationFile();
            int process = Process.GetProcessesByName(PROCESS_NAME).Length;

            if (process == 0)
            {
                //param pour ne pas mettre de gui et en fullscreen --portable
                string param = " --batch";

                processEmulator = Process.Start(dolphinFolder + EXE_NAME, param + " --exec \"" + path + filename);

            }
            else
            {
                Close();
                Execute(idGame);
            }
        }
```


#### Information complémentaire 

Il n'est pas possible de lancer un jeu alors qu' un autre est toujours en cours.


### Affichage et calcul du temps de jeu

Le temps de jeu est comptabilisé dans la base de données à la minute près.


#### Affichage dans le détail d’un jeu

Quand un utilisateur de Caiman se rend sur la page de détails d’un jeu, il verra son nombre d’heures et de minutes de jeu. Pour récupérer cette information, je fais un appel à la base de données .CallTimeInGameUser(idGame,idUser) cette appel va me rendre un objet TimeInGame qui va permettre de formater la réponse de l’API et afficher les heures et le minutes sous le format “10h10”. Si l’utilisateur n’a pas jouer au jeu, il verra afficher “Time played: 00h00”.

```C#
public string TimeHoursMinutes
        {
            get
            {
                string time = "";
                int hours = this.minutes / 60;
                int minutesInt = this.minutes % 60;
                if (minutesInt == 60)
                {
                    hours++;
                    minutesInt = 0;
                }
                string minutesString = minutesInt.ToString();
                string hoursString = "";
                if (minutesInt < 10)
                {
                    minutesString = "0" + minutesInt;
                }
                if (hours < 10)
                {
                    hoursString = "0" + hours;
                }
                time = hoursString + " h " + minutesString;
                return time;
            }
        }
```


#### Affichage du temps de jeu actuel

Quand un utilisateur lance un jeu, un objet GameTimer va être créé, cet objet sert à compter le temps de jeu de la session actuelle et à mettre à jour la base de données.

La classe GameTimer va initialiser in timer pour rafraîchir l’affichage de la navbar avec la valeur adéquate. Ce timer se rafraîchit toutes les secondes. Celui-ci va également appeler la fonction UpdateTimer, UpdateTimer va incrémenter le nombre de secondes de jeu et toutes les 60 secondes, il va faire un appel à l'API pour incrémenter le temps de jeu de l’utilisateur dans la base de données. L'incrémentation se fait donc toutes les minutes, cela permet d'être assez précis dans le décompte de temps de jeu.

```C#
public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(UpdateTimer);

            timer.Interval = 1000;
            timer.Start();
        }

public void UpdateTimer(object sender, EventArgs e)
        {
            counter++;

            if (counter == 60)
            {
                minutes++;
                counter = 0;

                callAPI.AddOneMinuteToGame(game.id, emulatorsManager.user.id);
            }

        }
```

L’affichage de temps de jeu se fait dans la navbar en haut à gauche, Les informations affichées sont les suivantes:



*   Nom du jeu en cours
*   heures et minutes de jeu de la session actuel sous le format 10m50 


### Téléchargement de jeux


#### Type de fichiers 

Les fichiers utilisés par les émulateurs sont des fichiers .iso, que ce soit par PCSX2 ou Dolphin.


#### Choix du fichier à télécharger

Le fichier à télécharger se fait sur la page de détails d’un jeu, si le jeu n’a pas déjà été téléchargé. Un bouton “download” sera présent, ce bouton va envoyer l’id du jeu qui doit être télécharger.

```C#
if (!File.Exists(gamePath))
                {
                    XboxButton btn_download = new XboxButton("download", game.id, 0, 0);
                    btn_download.Text = "Download: " + game.name;
                    btn_download.Location = new System.Drawing.Point(500, 650);
                    btn_download.Click += new System.EventHandler(bouton_Click);
                    lstControls[rowCounter].Add(btn_download);
                    Controls.Add(btn_download);
                    rowCounter++;
                    lstControls.Add(new List<Control>());
                }
``` 


#### Ajout d’un jeu à la liste de téléchargement

Les téléchargement des jeux sont gérés par la classe “DownloadManager”, cette classe contient 3 listes de téléchargement différentes:



*   lst_download
*   lst_activeDownload
*   lst_finishDownload

La première contient les téléchargement en attente, la deuxième le téléchargement en cours et la troisième la liste des téléchargements terminés.

Quand un téléchargement est créé, il est automatiquement ajouté à la liste de téléchargement en attente.


#### Lancement d’un téléchargement 

À l’ajout d’un téléchargement, la fonction StartDownload() est appelée. Cette fonction vérifie si dans la liste de téléchargement il sera lancé. Si aucun téléchargement n’est en cours, la fonction NextDownload() va être lancée.

La fonction NextDownload() sert à savoir si un téléchargement est en cours,Si un téléchargement est en cours, il sera déplacé dans la liste des téléchargements finis, si aucun téléchargement n’est en cours le premier téléchargement de la liste en attente est lancé


#### Téléchargement d’un fichier 

Quand la fonction download.StartDownload() est lancée, le WebClient se lance également. Ce WebClient télécharge le fichier en appelant l’API de caiman en passant en paramètres l’id du jeu qui doit être télécharger et l’apiKey de l’utilisateur.

```C#
public void StartDownload()
        {
            
            if (!CheckIfFileIsPresent())
            {
                webClient = new WebClient();
                Uri uri = new Uri(URL_TO_GAMES + "?idGame="+idGame+"&apiKey="+apiKey);
                webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                webClient.DownloadFileAsync(uri,pathToFolder+"temp."+filename);
                active = true;
            }
        }
```

Le fichier sera téléchargé dans le dossier spécifique à l'émulateur, par exemple si un jeu pour l’émulateur PCSX2 est téléchargé alors le jeu sera télécharger dans le dossier

“C:\Caiman\Playstation2”. Par contre, le fichier n’est pas téléchargé avec le nom final, le préfix “temp.” est ajouté.Le préfixe est ajouté pour éviter que si un utilisateur quitte Caiman pendant un téléchargement, il ne se retrouve pas avec un fichier incomplet au prochain lancement de l’application.

A la fin du téléchargement d’un fichier, la fonction DownloadManager.NextDownload() est appelée. l’id du jeu est ajouté à la liste des jeux téléchargés, et le fichier est renommé avec le nom correct. 

```C#
public void NextDownload()
        {
            if (lst_activeDownload.Count >0)
            {
                lst_finishDownload.Add(lst_activeDownload[0]);
                lst_activeDownload.RemoveAt(0);
            }

            if (lst_download.Count() == 1)
            {
                lst_activeDownload.Add(lst_download[0]);
                lst_download.RemoveAt(0);
                StartDownload();
            }
            
            
        }
```


### Synchronisation des sauvegardes


#### Structure des sauvegardes

Les fichiers de sauvegardes des émulateurs sont faits ainsi:

PCSX2:

Les fichiers de sauvegardes pour PCSX2 sont 2 fichiers de 8MB, comme  les memory cards étaient à l’époque de la Playstation 2. Ces deux fichiers contiennent les différentes sauvegardes des jeux.

![alt_text](images/dossier_memcards.png "image_tooltip")


Dolphin:

Les fichiers de sauvegardes de l’émulateur Dolphin ne concernent qu'un seul jeu à la fois. 

![alt_text](images/dossier_card_a.png "image_tooltip")


La solution pour structurer les sauvegardes des différents utilisateurs est celle-ci:

Chaque utilisateur possède un dossier personnel dans le dossier appdata de Caiman. Ce dossier contient le fichier de configuration graphique de l’application et un dossier ou ces sauvegardes sont présentes.

Le dossier de l’utilisateur lorenzo1227:

![alt_text](images/dossier_lorenzo1227.png "image_tooltip")


Le dossier contenant les sauvegardes de l'utilisateur lorenzo1227:

![alt_text](images/save_lorenzo1227.png "image_tooltip")



#### Upload des sauvegardes

Pour pouvoir synchroniser les sauvegardes entre les différents PCs d’un utilisateur, j’ai commencé par savoir si l’un des fichiers présents dans les dossiers des différents émulateurs a été mis à jour. Pour ce faire, j'ai un timer qui va faire une vérification sur la dernière date de modification du fichier. S’il s'avère qu'une modification a été faite dans l’un des dossiers de sauvegardes, je copie l’intégralité du dossier de l’émulateur vers le dossier appdata de l'utilisateur.

```C#
private void ScanDateFile()
        {
            int counter = 0;
            lst_saveTimeOld.Clear();
            lst_saveTimeOld.AddRange(lst_saveTimeNow);
            lst_saveTimeNow.Clear();
            //if a file has been added or delete sync the folder
            if (initialCounterFile != CountFileInFolder())
            {
                if (isLocalFile)
                {
                    initialCounterFile = CountFileInFolder();
                    MoveAllFileToUserFolder();
                }
            }
            foreach (FileInfo save in lst_save)
            {

                save.Refresh();

                lst_saveTimeNow.Add(save.LastWriteTime.Ticks.ToString());
                if (lst_saveTimeOld.Count() > 0)
                {
                    try
                    {
                        if (lst_saveTimeOld[counter] != lst_saveTimeNow[counter])
                        {
                            //if the file has benn modified since the last time move or sync the folder
                            if (lst_saveTimeOld[counter] != "")
                            {
                                if (isLocalFile)
                                {
                                    MoveFileToUserFolder(save);
                                }
                                else
                                {
                                    UploadSave();
                                }
                            }

                        }
                        counter++;
                    }
                    catch { }
                }


            }
        }

        public void MoveAllFileToUserFolder()
        {
            DirectoryInfo d = new DirectoryInfo(savePath);
            FileInfo[] Files = d.GetFiles(); 
            foreach (FileInfo file in Files)
            {
                try
                {

                    File.Copy(file.FullName, Path.Combine(destinationPath, file.Name), true);
                }
                catch (Exception)
                {
                }
            }
        }
```

Si je trouve une différence alors cela veut dire que le fichier a été modifié. Quand un fichier a été modifié je crée une copie de ce fichier dans le dossier “username/save/nom_de_emulateur/”.

Quand un fichier a été mis à jour, supprimé ou créé dans le dossier de sauvegarde de l’utilisateur, je zip les fichiers présents dans le dossier pour les envoyer sur le serveur de Caiman.

```C#
public void UploadSave()
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePathZipDolpin = "";
            var savePathZipPCSX2 = "";
            var savePath = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\");

            savePathZipDolpin = Path.Combine(appDataPath, @"Caiman\users\" +  emulatorsManager.user.username+ @"\Save\GamecubeWii\");
            savePathZipPCSX2 = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\Playstation2\");
            //zip the save of the emulators
            ZipFile.CreateFromDirectory(savePathZipPCSX2, savePath + "tempPCSX2.zip");
            ZipFile.CreateFromDirectory(savePathZipDolpin, savePath + "tempDolphin.zip");
            //call the api to create a copy of the save to the Bunker
            callAPI.UploadSave(1, emulatorsManager.user.id, emulatorsManager.user.apitoken, savePath + "tempDolphin.zip");
            callAPI.UploadSave(2,emulatorsManager.user.id,emulatorsManager.user.apitoken, savePath + "tempPCSX2.zip");
            //delete the temporary zip file
            File.Delete(savePath + "tempPCSX2.zip");
            File.Delete(savePath + "tempDolphin.zip");
            
        }
```


#### Download des sauvegardes présente sur le serveur de Caiman

Le lancement du téléchargement des sauvegardes est lancé à la connexion de l’utilisateur. Un appel à l'API est fait pour les deux émulateurs:

```C#
 public void CreateDownload(int idEmulator, string apiKey)
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePath = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\");


            if (lst_download == null)
            {
                lst_download = new List<DownloadSave>();
            }
            lst_download.Add(new DownloadSave(savePath, idEmulator,user.id, apiKey,user.username,this));
        }
```

Ces appels à l'API vont télécharger des fichiers .zip contenant les sauvegardes des différents émulateurs. Ces deux fichiers vont être décompressé dans le dossier spécifique de chaque émulateur. Avant de pouvoir décompresser le dossier, je dois au préalable supprimer le contenu des dossiers de destination (Si je dois supprimer le contenu des dossiers c’est parce que la classe Zipfile de C# sous la version 5.0 ne peut pas “override” le contenu d’un dossier).

Quand les fichiers zip sont décompressés le contenu est envoyé dans les dossiers des différents émulateurs.

## Description technique: Interface graphique

Diagramme de classe

![alt_text](images/DiagramVue.png "image_tooltip")



### Organisation des boutons

Pour faire en sorte de pouvoir se déplacer de bouton en bouton, il m’a fallu trouver une stratégie pour organiser les boutons. Pour structurer la liste des boutons, j’ai finalement décidé de créer une liste de liste de boutons. La liste principale sert de rangé et la “sous liste” sert de colonne. Cela permet de connaître plus aisément l’emplacement des boutons qu’une simple liste à une seule dimension.


### Gestion des inputs


#### Interaction avec les manettes

L’application Caiman prend en charge les manettes reconnues nativement par windows (Xinput). Pour pouvoir communiquer avec la manette, j’ai utilisé le paquet NuGet “SharpDX”, il me permet de connaître les informations et les inputs des manette connectés au pc.

Pour simplifier l’interaction avec la manette de l’utilisateur, j’ai créé une classe “XboxController.cs”, Cette classe me permet de connaître les manettes connectées et de savoir à un instant T les inputs de chaque manette.


#### Transformation d’inputs en événement. 

Pour simplifier la navigation, seule la manette une a le droit de se déplacer dans l’application de Caiman. Pour connaître les inputs, je les rafraîchis toutes les 10ms, cela permet de ne pas avoir de latence ou de louper des inputs. 


#### Gestion des boutons

Quand la fonction ScanInput reçoit un string contenant les inputs de la manette 1. Ce string est envoyé dans un switch qui va faire une comparaison entre l’input précédent de la manette et l’input actuel. Si l’input actuel est différent de l’ancien alors celaveut dire que l’utilisateur a pressé un bouton puis il l'a soit relâché soit fait une autre combinaison de touches.

Le code suivant n'est pas complet!
```C#
public void ScanInput(object sender, EventArgs e)
        {

            // if the user 1 controller is connected
            if (xboxController.lstController[0].IsConnected)
            {
                string input = xboxController.lstController[0].GetState().Gamepad.Buttons.ToString();
                int inputAnalogLeftX = xboxController.lstController[0].GetState().Gamepad.LeftThumbX;
                int inputAnalogLeftY = xboxController.lstController[0].GetState().Gamepad.LeftThumbY;
```

```C#
if (input == "A" && old_input != "A")
                    {
                        SendKeys.Send("{ENTER}");
                    }
```



#### Gestion du joystick gauche

La réception des inputs pour le joystick n’est pas comme pour les boutons, en sachant que les joysticks sont analogiques, je reçois une valeur entre -32000 et +32000. Je vais recevoir ces valeurs pour l’axe X et l’axe Y. Pour savoir si l’utilisateur a poussé le joystick dans une direction, j’ai défini une “deadzone” dans laquelle je considère qu' aucune direction n’est définie par l’utilisateur de la manette. J’ai défini cette zone à 20000, j’ai fait des tests pour savoir où je trouvais que la “deadzone” devait s'arrêter. Puis j’ai fait comme pour les boutons,  j’ai fait une comparaison avec la valeur précédente pour savoir si le joystick vient d'être poussé.

```C#
                bool leftAnalogUp = false;
                bool leftAnalogDown = false;
                bool leftAnalogLeft = false;
                bool leftAnalogRight = false;

                if (inputAnalogLeftX > DEAD_ZONE_JOYSTICK)
                {
                    leftAnalogRight = true;
                }

                if (inputAnalogLeftX < -DEAD_ZONE_JOYSTICK)
                {
                    leftAnalogLeft = true;
                }

                if (inputAnalogLeftY > DEAD_ZONE_JOYSTICK)
                {
                    leftAnalogUp = true;
                }

                if (inputAnalogLeftY < -DEAD_ZONE_JOYSTICK)
                {
                    leftAnalogDown = true;
                }
```


### Découpage de l’interface

 L’interface de l’application Caiman est divisée en trois parties hormis pour la connexion.


#### Navbar

La première est la navbar qui contient les différents boutons de navigation et les informations sur le jeu en cours. Tous les boutons sont créés dynamiquement, le placement s’adapte à l'écran principal de l’utilisateur, mais s’il a un écran vraiment trop petit, les informations sur le jeu en cours et les boutons risquent de s'entremêler.

Pour afficher le jeu en cours, le navigateur est obligé de se rafraîchir et de se redessiner toutes les secondes pour afficher le temps de jeu. Pour savoir si un jeu est en cours,lal'a navbar regarde si le GameTimer est lancé. Si le GameTimer est lancé c’est qu'un jeu est en cours, dans ce cas il va afficher le nom et le temps de jeu.

```C#
private void UpdateData(object sender, EventArgs e)
        {
            if (xboxMainForm.emulatorsManager.gameTimer != null)
            {
                if (actualGameName != "")
                {
                    lbl_game_actual.Text = " Now playing: " + actualGameName + "  " + xboxMainForm.emulatorsManager.gameTimer.ToString();
                }
                else
                {
                    lbl_game_actual.Text = "";
                }
            }
            else
            {
                lbl_game_actual.Text = "";
            }
        }
```


#### Sidebar

La sidebar contient des boutons qui permettent d’afficher différentes listes de jeux. Ces boutons sont en partie créés dynamiquement et en partie reçus de l’API. Les premiers boutons sont ceux qui concernent l’utilisateur, on va les avoir dans l’ordre:



*   Downloaded games
*   Favorites games
*   All games

Ces catégories ne sont pas créées dynamiquement.

Par contre, les suivants sont reçus de l’API, ils concernent les différentes catégories disponibles pour Caiman. À l’appui de ces boutons, la liste des jeux va être chargée et le main panel va afficher la liste des jeux reçus par l’API.

```C#
public void CreateListNavButton()
        {
            List<string> lst_navbar = new List<string>();
            List<Category> lst_category = xboxMainForm.callAPI.CallAllCategories();
            lst_navbar.Add("Downloaded games");
            lst_navbar.Add("Favorites games");
            lst_navbar.Add("All games");

            foreach (var item in lst_category)
            {
                lst_navbar.Add(item.name);
            }
            for (int i = 0; i < lst_navbar.Count; i++)
            {
                lstControls.Add(new List<Control>());
                lstControls[i].Add(new XboxButton());
            }

            //update the buttons infos
            for (int a_row = 0; a_row <= (lst_navbar.Count -1); a_row++)
            {

                List<string> lstString = new List<string>();
                XboxButton tempButton = new XboxButton("side", a_row, a_row, 0);
                lstControls[a_row][0] = tempButton;
                lstControls[a_row][0].Text = lst_navbar[a_row];
                lstControls[a_row][0].Location = new System.Drawing.Point(0 * 100 + 20, a_row * 75 + 15);
                lstControls[a_row][0].Width = 200;
                lstControls[a_row][0].Height = 50;
                lstControls[a_row][0].Name =  "btn_"+ lst_navbar[a_row];


                Controls.Add(lstControls[a_row][0]);
                lstControls[a_row][0].Click += new System.EventHandler(bouton_Click);

            }

            //set the action of button
            XboxButton downloadedGames = (XboxButton)lstControls[0][0];
            downloadedGames.contextInfos.contexte = "downloadedGames";
            XboxButton userFavoritesGames = (XboxButton)lstControls[1][0];
            userFavoritesGames.contextInfos.contexte = "favorite";
            XboxButton allGames = (XboxButton)lstControls[2][0];
            allGames.contextInfos.contexte = "home";

            for (int i = 0; i < lst_category.Count; i++)
            {
                XboxButton tempButton = (XboxButton)lstControls[(i+3)][0];
                tempButton.contextInfos.contexte = "category";
                tempButton.contextInfos.id_contexte = (lst_category[i].id);
            }

        }


    }
```


#### Main panel

Néanmoins,le main panel ne peut pas vraiment être décrit, c’est un panel contextuel qui va être défini par les besoins de l’utilisateur.


### Affichage d’une liste de jeu

Pour l’affichage des listes des jeux, j’ai essayé de faire en sorte que le panel soit le plus flexible possible. le panel demande juste une liste de jeux, il n’y a pas de panel fait pour les jeux favoris ou les jeux déjà présents sur le disque.

Le panel utilise la fonction CreateListGames() pour dessiner les différents boutons de lien vers les détails des jeux. Pour que certaines images ne dépasse pas l’écran et qu’elles soient coupées, la fonction calcul le nombre d’images qu’elle peut afficher par ligne. Le calcul se fait selon la définition de l’écran de l’utilisateur. Les images affichées sont celles qui sont dans le dossier appdata de Caiman.

```C#
public void CreateListGames()
        {

            string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            XboxImage tempXboxImage = new XboxImage();
            int max_rank = Width / (315);
            int tempPos_x = 0;
            int tempPos_y = 0;
            lstControls.Add(new List<Control>());


            foreach (var game in lst_games)
            {
                if (tempPos_x == max_rank)
                {
                    lstControls.Add(new List<Control>());
                    tempPos_y++;
                    tempPos_x = 0;
                }

                lstControls[tempPos_y].Add(new XboxImage());
                Image img = new Bitmap((imgPath+ game.imageName));
                XboxImage tempButton = new XboxImage("game", img, game.id, tempPos_x, tempPos_y);
                lstControls[tempPos_y][tempPos_x] = tempButton;
                lstControls[tempPos_y][tempPos_x].Location = new System.Drawing.Point(tempPos_x * 300 + 15, tempPos_y * 430 + 15);

                Controls.Add(lstControls[tempPos_y][tempPos_x]);
                tempButton.Click += new System.EventHandler(bouton_Click);



                tempPos_x++;
            }
        }
```


### Gestion des erreurs de déplacement

Le déplacement à la manette se fait par déplacement haut,bas,gauche,droite. Donc, J’ai décidé de faire en sorte que si l’utilisateur se déplace dans une case qui n'existe pas, ils soient soit déplacés dans l’une des casses disponibles proche de la case demandée, autrement  rien ne se passe.

Pour cela, j'ai dû décider arbitrairement ce qui se passait. si un mouvement illégal était fait. J’ai essayé de reproduire les déplacements utilisés dans l’interface de la console Xbox. Il y a certains cas qui ne sont pas forcément bien gérés. Il serait possible de faire un déplacement en calculant la position actuelle et en se basant sur la position en pixel. Cette alternative pourrait être intéressante.

```C#
public void MoveActivateControl(string destination = "")
        {
            //top = 1
            //right = 2
            //down = 3
            //left = 4
            if (destination == "down")
            {
                if (lstControls[position_y][position_x] == null)
                {
                    int x = position_x;
                    int y = position_y;

                    int y_right_not_disponible;
                    while (x < lstControls[position_y].Count() && lstControls[position_y][x] == null)
                    {
                        if (x > lstControls[position_x-1].Count())
                        {
                            break;
                        }
                        x++;
                    }
                    if (x == lstControls[position_y].Count())
                    {
                        y_right_not_disponible = y;

                        while (y_right_not_disponible>0)
                        {
                            y_right_not_disponible --;
                            if (lstControls[y_right_not_disponible][position_x] != null)
                            {
                                position_y = y_right_not_disponible;
                                lstControls[(position_y)][position_x].Focus();
                                return;
                            }
                        }
                    }
                    lstControls[(position_y)][x].Focus();
                    Position_x = x;
                }
                else
                {
                    lstControls[(position_y)][position_x].Focus();
                }
            }
            else
            {
                if (position_x < lstControls[position_y].Count())
                {


                    if (lstControls[position_y][position_x] == null)
                    {
                        int x = position_x - 1;
                        int y = position_y;
                        while (lstControls[position_y][x] == null)
                        {
                            if (x < 0)
                            {

                                break;
                            }
                            x--;
                        }
                        lstControls[(position_y)][x].Focus();
                        Position_x = x;
                    }
                    else
                    {
                        lstControls[(position_y)][position_x].Focus();
                    }
                }
                else {
                    position_y--;
                }
            }
        }
```


### Passage de la souris à la manette

Durant mes tests de l'application, je me suis rendu compte que le fait de passer de la manette à la souris et inversement, posait un problème. Quand je passais de l’un à l'autre la position du curseur n’était pas sauvegardée. Quand je passais de la souris à la manette, le curseur se retrouvait à une position erronée. Pour pouvoir pallier au problème, chaque bouton a une position définie et quand l’utilisateur clique dessus ,un événement se déclenche et notifie la forme que le bouton actuel a changé.

```C#
protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            //tell to the topMainForm which control is active
            XboxUserControl xboxUserControl = (XboxUserControl)this.Parent;
            if (xboxUserControl != null)
            {
            xboxUserControl.position_x = this.contextInfos.position_x;
            xboxUserControl.position_y = this.contextInfos.position_y;
            }


            if ((XboxMainForm)this.TopLevelControl != null)
            {
                XboxMainForm topMainForm = (XboxMainForm)this.TopLevelControl;
                topMainForm.ActiveControl1 = xboxUserControl;
            }
        }
```
