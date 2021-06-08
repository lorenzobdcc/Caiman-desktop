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
