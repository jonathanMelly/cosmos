//langage de type 'pseudo-code' pour apprendre à programmer
lexer grammar CosmosLexer ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

LABEL_AUTEUR : 'Auteur:' -> pushMode(TEXTE_1LIGNE);

LABEL_DATE : 'Date:' ;
CONTENU_DATE :  CHIFFRE CHIFFRE? //jour
                POINT
                CHIFFRE CHIFFRE? //mois
                POINT
                CHIFFRE CHIFFRE (CHIFFRE CHIFFRE)? ; //année

ENTREPRISE_ENTETE : 'Entreprise:' -> pushMode(TEXTE_1LIGNE) ;

DESCRIPTION_ENTETE : 'Description:' -> pushMode(TEXTE_NLIGNES) ;

DEBUT : 'Voici les ordres du programme' ;
BIBLIOTHEQUE: 'à classer dans la bibliothèque' ;
FIN   : 'Fin' ;
DE_LA_TRANSMISSION : 'de la transmission' ;

SUR:'sur';
CHOISIR_COULEUR:'Choisir la couleur';
RECUPERER:'Récupérer la saisie et la stocker dans ';
RECUPERER_TOUCHE:'Attendre la prochaine touche et la stocker dans ';
DECOUPER:'Découper';
PLACER_LE_CURSEUR:'Placer le curseur à la ';
LIGNE:'ligne ';
COLONNE:'colonne ';
PLACER_ALEATOIRE:'Placer un nombre aléatoire compris entre ';

DANS:'dans ';
AFFICHER:'Afficher ';
MASQUER:'Masquer ';
LE_CURSEUR:'le curseur';
INSERER: 'Insérer '|'Copier ';
ATTENDRE:'Attendre ';
MS:'ms';
ROUGE:'rouge';
VERT:'vert';
BLEU:'bleu';
BLANC:'blanc';
NOIR:'noir';
GRIS:'gris';
FONCE:'foncé';
POUR_LE:'pour le';
TEXTE:'texte';
FOND:'fond';

PRESSION_TOUCHE:'la pression d\'une touche';

EFFACER_ECRAN: 'Effacer l\'écran';

ALLOUER_TERME : 'Allouer ' | 'Créer ' ;
INITIALISATION_TERME : 'avec ' | 'et y enregistrer ' ;

LA:'la ';
UNE:'une ';
VALEUR:'valeur ';
VARIABLE : PREFIXE_VARIABLE PREFIXE_VARIABLE? (MOT|VALEUR_NOMBRE|(LETTRE (POINT? (LETTRE|CHIFFRE))*)) ; //double préfixe pour les variables internes...
DE: 'de ' | 'enregistrée dans ';

ZONE_MEMOIRE : 'zone mémoire ' ;
ZONE_NOM : 'nommée ' ;

REPETER:'Répéter ';
TANT_QUE:'tant que ';

AUTANT_DE_FOIS:'autant de fois qu\'il y a de ' ;
LE_NOMBRE_DE_FOIS:'le nombre de fois correspondant à ';




SI:'Si ';
SINON_SI:'sinon si ';
ET_SINON:'et sinon';
ALORS:'alors';


OPERATEUR_COMPARAISON_EQUIVALENT : 'vaut ' | 'est égal à ' | '==' | 'est égale à ' ;
OPERATEUR_COMPARAISON_DIFFERENT : 'est différent de ' | 'n\'est pas égal à ' | '!=' | '<>' | 'est différente de' | 'n\'est pas égale à' ;
OPERATEUR_COMPARAISON_PLUS_GRAND : 'est plus grand que ' | 'est supérieur à ' | '>' | 'est plus grande que' | 'est supérieure à'  ;
OPERATEUR_COMPARAISON_PLUS_PETIT : 'est plus petit que ' | 'est inférieur à ' | '<' | 'est plus petite que' | 'est inférieure à';
OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL : 'est plus grand ou égal à ' | 'est supérieur ou égal à' | '>=' |'est plus grande ou égale à' | 'est supérieure ou égale à'  ;
OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL : 'est plus petit ou égal à ' | 'est inférieur ou égal à' | '<=' | 'est plus petite ou égale à' | 'est inférieure ou égale à';

VRAI: 'vrai'|'OK' ;
FAUX: 'faux'|'KO' ;

//On ne peut pas avoir deux entrées pour la même définition
//Le 'et' étant utilisé à plusieurs endroits, il faut le gérer pour la partie logique
//à partir de la définition 'ET'...
ET : 'et ' ;
OPERATEUR_LOGIQUE_ET: '&&' ;

OPERATEUR_LOGIQUE_OU: 'ou' | '||' ;
OPERATEUR_LOGIQUE_OU_EXCLUSIF: 'ou au contraire ' | 'xor' ;
OPERATEUR_LOGIQUE_EST : 'est ' ;
OPERATEUR_LOGIQUE_NON : 'l\'inverse de ' | '!' | 'not' ;

OPERATEUR_MATH_EGAL : '=' ;
PREFIXE_VARIABLE : '#' ;


PARENTHESE_GAUCHE : '(' ;
PARENTHESE_DROITE : ')' ;

LE_TEXTE : 'le texte ' ;
VALEUR_TEXTE : '"' ~["]* '"' ;

LE_NOMBRE : 'le nombre ' ;
VALEUR_NOMBRE : CHIFFRE+ (POINT CHIFFRE+)? ;

OPERATEUR_MATH_PLUS : '+' | 'plus ' ;
OPERATEUR_MATH_MOINS : '-' | 'moins ' ;
OPERATEUR_MATH_FOIS : '*' | 'fois ' ;
OPERATEUR_MATH_DIVISE : '/' | 'divisé par ';
OPERATEUR_MATH_PUISSANCE : '^' | 'élevé '? 'à la puissance ';
OPERATEUR_MATH_RACINE_CARREE : 'racine carrée de ' ;

LE_RESULTAT_DE : 'le résultat de ' ;

fragment CHIFFRE : '0'..'9' ;

fragment MINUSCULE : 'a'..'z' ;
fragment MAJUSCULE : 'A'..'Z' ;
fragment SYMBOLES_LETTRE : [_] ;
fragment LETTRE : MINUSCULE | MAJUSCULE | SYMBOLES_LETTRE ;

VIRGULE : ',' ;
POINT: '.' ;
POINT_INTERROGATION: '?' ;
SUIVANT : '>>' ;
DEUX_POINT: ':' ;
FOIS: 'x' ;


TABULATION : '\t' | '    ' ;
RETOUR_DE_CHARIOT : '\r'? '\n' ;

//fragment CARACTERE : ~[."\\\r\n ] ;

//Tout le reste...
MOT : LETTRE (LETTRE|CHIFFRE)* ;
ESPACE: ' ' -> skip ;
COMMENTAIRE_LIGNE: (TABULATION? | TABULATION+) '//' ~[\n]* RETOUR_DE_CHARIOT -> skip ;
COMMENTAIRE : '/*' .*? '*/' -> skip ;


//WS: [ \t\r\n\u000C]+ -> channel(HIDDEN);
//WS : [ \r\n\t] + -> skip ;
//WS : [ ] + -> channel(HIDDEN) ;

mode TEXTE_1LIGNE;
TEXTE_LIBRE_MONOLIGNE: .+? RETOUR_DE_CHARIOT -> popMode ;

mode TEXTE_NLIGNES;
TEXTE_LIBRE_MULTILIGNE: .+? RETOUR_DE_CHARIOT RETOUR_DE_CHARIOT+ -> popMode;//arrêt à au moins 2 retour à la lignes ;
