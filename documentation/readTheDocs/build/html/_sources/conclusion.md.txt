## Conclusion et perspectives

### Problème rencontrés

Durant ce travail, j’ai dû surmonter plusieurs difficultés, que ce soit des choses que je ne connaissais pas, ou bien des inconnues techniques.

Je pense que la plus grosse contrainte que j’ai eu, est que j’ai dû créer une API que je n’avais pas prévu à la base. Quand j’ai commencé l’application Caiman, j’ai discuté avec M.Maréchal et M.Schmid, les deux m’ont conseillé de créer une API pour accéder à la base de données depuis Caiman. Donc, J’ai pris une semaine complète pour créer mon API.Originellement, ce temps passé sur l’API n’était pas prévu mais je pense que cela m’a fait gagner du temps finalement.

Au niveau du code tout s’est relativement bien passé. L’un des seuls points où j’ai eu des difficultés est le téléchargement de jeux. Que ce soit du côté serveur ou client, je n'avais jamais fais de l’upload / téléchargement de fichiers. J’ai été conseillé par M.Schmid, ce qui m’a bien aidé.

Un autre point qui a été compliqué c’est la façon dont j'allais distribuer mon application. L'outil inclus dans Visual Studio n’étant pas vraiment mis à jour par Microsoft, j'ai du chercher de mon côté comment créer un programme d’installation, mais je n’ai pas vraiment trouvé. Finalement, M.Schmid m’a aidé mais tout n’était pas réglé pour autant. Le problème était que quand j’installais Caiman, certains fichiers des émulateurs n'étaient plus là donc je ne pouvais plus exécuter de jeu.

Le plus gros défi a été de créer une interface graphique. Durant notre formation nous ne sommes pas formés pour cela. Alors, j’ai dû beaucoup me documenter pour le projet. Finalement, je pense avoir une interface agréable et de surcroît utilisable avec une manette.


### Expérience acquise durant le travail

J’ai énormément appris de choses durant ce travail, en particulier sur le développement en C#, et sur le fonctionnement des émulateurs. Je n'avais jamais créé d’application aussi complexe. Pour faire fonctionner mon application, j’ai dû faire énormément de choses différentes. J’ai apprécié mettre toutes ces choses en relation du développement C#, du développement de site web, la création d’une API et la configuration d’un serveur Debian. J’ai donc pu créer une application de A à Z (sans compter les émulateurs bien sur) qui au final fonctionnent bien.


### Améliorations envisageables


#### Caiman C#

J’ai eu une grande frustration durant ce travail, le fait de ne pas avoir 2 mois de plus. Chaque jour sur lequel j’ai travaillé sur l’application Caiman, j’avais énormément d’idées qui me venaient en tête. Les principales étaient.:



*   Ajouter d’autres émulateurs
*   Synchroniser les paramètres de caiman entre les différents pc de l’utilisateur
*   Améliorer l’interface
*   Ajouter un système d’amis pour voir plus facilement ce à quoi jouent les autres joueurs
*   Créer un meilleur tri dans les jeux
*   Faire la liste des jeux les plus téléchargés
*   Avoir une meilleur gestion des téléchargements
*   Prendre en compte plus de manettes (PS4,Switch,etc)

Vous l’avez compris, cette liste est sans fin. Mais je savais pertinemment avant même de commencer ce travail que je ne pourrais pas être vraiment satisfait du résultat final.

J’aurais aussi aimé pouvoir créer un installateur pour Caiman, mais quand j’ai essayé j’ai eu des soucis avec les fichiers des émulateurs. J’ai donc finalement dû y renoncer à cause d’un manque de temps.


#### API

L’API n'étant pas prévu à la base, elle a été une charge de travail supplémentaire mais qui finalement m’a fait gagner du temps. Des améliorations sont possibles, par exemple je pense que mes endpoints pourraient être améliorés et surtout plus nombreux. Par exemple, je pourrais créer un endpoint pour les téléchargements des jeux et des fichiers de sauvegarde.


### Conclusion Globale

J’ai eu un réel plaisir à créer Caiman. Ce fut une expérience vraiment intéressante pour moi. Durant ma formation, je n’avais jamais pu créer de “vrai” projet en C#, alors quand j’ai dû choisir le sujet de mon travail, j’ai tout de suite sauté sur l'occasion d’en faire.

J’ai eu la chance de pouvoir travailler sur ce projet en sachant qu’il était atypique. Je voulais vraiment pour mon diplôme travailler sur un sujet qui me plait.

Pour conclure, je pense que Caiman est une réussite. Mon but principal était qu'une personne qui ne connaisse rien aux jeux vidéo et en particulier aux émulateurs puisse jouer à un jeu en moins de 5 minutes, ce qui est le cas. Voilà pourquoi je pense que mon projet est réussi. 
