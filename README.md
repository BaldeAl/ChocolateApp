# C#
      Créer un programme de gestion de commande de chocolat.
      
      
      Votre DB en Json:
      1 fichier des administrateurs : Guid Id, Login (string), Password (string)
      1 fichier des acheteurs contenant: GUID Id, adresse (string), nom(string), prenom (string), telephone (int)
      1 fichier des articles contenant : Guid Id, Reference (string), prix (float)
      1 fichier des ArticlesAchetés contenant: Guid idAcheteur, Guid IdChocolat, Quantité (int), DateAchat (datetime)
      
      
      
      Lancement de l'application
      
      
      L'application doit être vide au premier démarrage et créer votre DB ainsi que l'initialisation de toutes les valeurs dans cette DB.
      Toutes les actions de création de la DB doivent être visible au démarrage et dans les logs.
      
      
      Quand l'installation est finie l'écran de la console doit se vider pour ne faire apparaître le choix suivant (1: Utilisateur 2: Administrateur)
      
      
      
      L'application console ou Winform ou WPF:
      
      
      On doit pouvoir choisir qui utilise l'application (Administrateur / Utilisateur)
      
      
      Administrateur:
      L'administrateur doit avoir un accès privilégié protégé par un Login / password.
      Le password doit contenir au moins 6 caractères alphanuméric et un caractère spécial (ex: AZERT1%). Si la saisie est mauvaise alors un message doit apparaitre.
      Seul l'administrateur peut saisir des articles.
      Seul l'administrateur peut créer un fichier txt (format facture) donnant la somme des articles vendus.
      Seul l'administrateur peut créer un fichier txt (format facture) donnant la somme des articles vendus par acheteurs.
      Seul l'administrateur peut créer un fichier txt (format facture) donnant la somme des articles vendus par date d'achat.
      
      
      Utilisateur:
      L'utilisateur n'a pas de login password.
      L'utilisateur doit saisir son nom, prenom, adresse et téléphone avant de pouvoir ajouter un article.
      L'utilisateur doit choisir un article après l'autre et saisir la quantité de chacun.
      Tant que l'utilisateur n'a pas fini sa commande il doit pouvoir saisir un nouvel article.
      Pour finir sa commande l'utilisateur doit taper sur la touche 'F'.
      Pour voir le prix de sa commande en cours il doit taper la touche 'P'
      Quand l'utilisateur a fini sa commande, un fichier texte doit etre fait avec un recapitulatif de ses articles, le prix de chacun et le prix total. Le fichier sera sauvegardé dans un dossier à son nom et le nom du fichier aura le format suivant "Nom-Prenom-Jour-Mois-Annee-Heure-Minute.txt"
      
      
      Toutes les actions faite doivent être logguées et lisibles.
      Exemple: un utilisateur ajoute un chocolat a sa liste le log doit etre => 10/10/2023 Ajout d'un kinder 100g à 10h23 par Toto l'asticot
      Administrateur ajout les kinder bueno à 10h00 à la liste d'article.
      
      
      
      
      Préconisation:
      1 projet contenant les models
      1 projet contenant les services d'interaction avec les fichiers (ecriture, lecture)
      1 projet contenant les services permettant la gestion des différentes liste (Administrateurs, Acheteurs, Articles)
      1 projet contenant les services de logs
      1 projet contenant les différents appel vers les services qu'on appelera "Projet".Core
      1 projet contenant l'application console ou Visuel
      
      
      Les données saisies doivent être controlées. L'utilisateur ne peut pas saisir "test" si il doit donner sa quantité de chocolat
      
      
      
      Bonus:
      La liste des articles et leur prix sera toujours visible dans un autre écran.
      Chaque fonction ne dépasse pas 10 lignes écrites (les lignes vides ne compte pas).
      Les standards d'écriture sont respectés.
      Les calculs sont parallélisés.
      Les méthodes sont asynchrones.
      Chaque classe (hors model) doit avoir une Interface (ex: FileWriter => IFileWriter)
      Chaque méthode a un retour dans les services. Pas de void.
