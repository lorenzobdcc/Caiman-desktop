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
