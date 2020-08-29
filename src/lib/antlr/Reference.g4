//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete RETOUR_DE_CHARIOT+ mainStart (instruction|noop)+ mainEnd .*? EOF ;

entete : auteur RETOUR_DE_CHARIOT date RETOUR_DE_CHARIOT entreprise RETOUR_DE_CHARIOT description ;

auteur : LABEL_AUTEUR MOT MOT? ;
LABEL_AUTEUR : 'Auteur:' ;

date : LABEL_DATE CONTENU_DATE ;
LABEL_DATE : 'Date:' ;
CONTENU_DATE :  CHIFFRE CHIFFRE? //jour
                POINT
                CHIFFRE CHIFFRE? //mois
                POINT
                CHIFFRE CHIFFRE (CHIFFRE CHIFFRE)? ; //année

entreprise : ENTREPRISE_ENTETE MOT ;
ENTREPRISE_ENTETE : 'Entreprise:' ;

description : DESCRIPTION_ENTETE MOT+ (VIRGULE RETOUR_DE_CHARIOT MOT+)* ;
DESCRIPTION_ENTETE : 'Description:' ;

mainStart: DEBUT nomDuProgramme=MOT (BIBLIOTHEQUE bibliotheque=MOT)? DEUX_POINT ;
mainEnd: FIN DE_LA_TRANSMISSION? POINT;
DEBUT : 'Voici les ordres du programme' ;
BIBLIOTHEQUE: 'à classer dans la bibliothèque' ;
FIN   : 'Fin' ;
DE_LA_TRANSMISSION : 'de la transmission' ;

instruction : TABULATION+ (instruction_simple | instruction_complexe) ;
noop:TABULATION* RETOUR_DE_CHARIOT ;

instruction_simple   : (afficher|allouer|affecter|recuperer|generer_aleatoire|placer_curseur|dormir|colorier|decouper) POINT RETOUR_DE_CHARIOT ; //terminaison identique pour chaque
instruction_complexe : selection|boucle ; //terminaison spécifique pour chaque

afficher : 'Afficher' expression;
allouer : ALLOUER_TERME une_zone_memoire (INITIALISATION_TERME? expression)? ;
affecter : (('Insérer'|'Copier') expression 'dans' la_zone_memoire) | (variable OPERATEUR_MATH_EGAL expression ) ;
recuperer: 'Récupérer la saisie et la stocker dans'?  la_zone_memoire;
placer_curseur: 'Placer le curseur à la' (ligne='ligne'|colonne='colonne') expression_numerique;
generer_aleatoire: 'Placer un nombre aléatoire compris entre ' min=expression_numerique ET max=expression_numerique 'dans' la_zone_memoire;
dormir: 'Attendre' expression_numerique 'ms';
colorier: 'Choisir la couleur' (red='rouge'|green='vert'|blue='bleu'|white='blanc'|black='noir'|gray='gris') dark='foncé'? 'pour le' (text='texte'|background='fond');
decouper: 'Découper' source=expression 'sur' separateur=expression;

ALLOUER_TERME : 'Allouer' | 'Créer' ;
INITIALISATION_TERME : 'avec' | 'et y enregistrer' ;

la_zone_memoire : ('la' ZONE_MEMOIRE ZONE_NOM?)? VARIABLE;
une_zone_memoire : ('une' ZONE_MEMOIRE ZONE_NOM?)? VARIABLE;
ZONE_MEMOIRE : 'zone mémoire' ;
ZONE_NOM : 'nommée' ;

boucle :
    'Répéter' (expression_numerique FOIS | 'tant que' expression_booleenne | boucle_avec_variable) RETOUR_DE_CHARIOT
    (instruction|noop)+
    TABULATION+ SUIVANT RETOUR_DE_CHARIOT ;

boucle_avec_variable : 'autant de fois qu\'il y a de ' VARIABLE | 'le nombre de fois correspondant à' variable;

selection :
    'Si' base_si
    sinon_si*
    sinon?
    TABULATION+ POINT_INTERROGATION RETOUR_DE_CHARIOT ;

base_si : condition=expression_booleenne 'alors' RETOUR_DE_CHARIOT (instruction|noop)+ ;
sinon_si : TABULATION+ 'sinon si' base_si ;
sinon : TABULATION+ 'et sinon' RETOUR_DE_CHARIOT (instruction|noop)+ ;

OPERATEUR_COMPARAISON_EQUIVALENT : 'vaut' | 'est égal à' | '==' | 'est égale à' ;
OPERATEUR_COMPARAISON_DIFFERENT : 'est différent de' | 'n\'est pas égal à' | '!=' | '<>' | 'est différente de' | 'n\'est pas égale à' |;
OPERATEUR_COMPARAISON_PLUS_GRAND : 'est plus grand que' | 'est supérieur à' | '>' | 'est plus grande que' | 'est supérieure à' |  ;
OPERATEUR_COMPARAISON_PLUS_PETIT : 'est plus petit que' | 'est inférieur à' | '<' | 'est plus petite que' | 'est inférieure à';
OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL : 'est plus grand ou égal à' | 'est supérieur ou égal à' | '>=' |'est plus grande ou égale à' | 'est supérieure ou égale à'  ;
OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL : 'est plus petit ou égal à' | 'est inférieur ou égal à' | '<=' | 'est plus petite ou égale à' | 'est inférieure ou égale à';

VRAI: 'vrai'|'OK' ;
FAUX: 'faux'|'KO' ;

ET : 'et' ;
OPERATEUR_LOGIQUE_ET: '&&' ;
OPERATEUR_LOGIQUE_OU: 'ou' | '||' ;
OPERATEUR_LOGIQUE_OU_EXCLUSIF: 'ou au contraire' | 'xor' ;
OPERATEUR_LOGIQUE_EST : 'est' ;
OPERATEUR_LOGIQUE_NON : 'l\'inverse de' | '!' | 'not' ;

OPERATEUR_MATH_EGAL : '=' ;
VARIABLE : PREFIXE_VARIABLE PREFIXE_VARIABLE? (MOT|VALEUR_NOMBRE|(LETTRE (POINT? (LETTRE|CHIFFRE))*)) ; //double préfixe pour les variables internes...
PREFIXE_VARIABLE : '#' ;

//Variable doublé pour éviter que la première règle de sous-expression prenne le dessus
expression
        : variable
        | expression_comparable
        | expression_booleenne
        ;

//Variable doublé pour éviter que la première règle de sous-expression prenne le dessus
expression_comparable
        : variable
        | expression_textuelle
        | expression_numerique
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_booleenne
        : gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_OU droite=expression_booleenne
        | gauche=expression_booleenne operateur=(ET | OPERATEUR_LOGIQUE_ET | OPERATEUR_LOGIQUE_OU_EXCLUSIF) droite=expression_booleenne //priorité différente ?
        | gaucheNb=expression_comparable
            operateurNb=(  OPERATEUR_COMPARAISON_EQUIVALENT
                         | OPERATEUR_COMPARAISON_DIFFERENT
                         | OPERATEUR_COMPARAISON_PLUS_GRAND
                         | OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL
                         | OPERATEUR_COMPARAISON_PLUS_PETIT
                         | OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL)
          droiteNb=expression_comparable
        | gauche=expression_booleenne operateur=(OPERATEUR_COMPARAISON_EQUIVALENT | OPERATEUR_COMPARAISON_DIFFERENT) droite=expression_booleenne
        | gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_EST (VRAI|FAUX) //construction sympa ;-)
        | OPERATEUR_LOGIQUE_NON sousExpression=expression_booleenne
        | (VRAI | FAUX | variable)
        | PARENTHESE_GAUCHE sousExpression=expression_booleenne PARENTHESE_DROITE
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_numerique
        : gauche=expression_numerique operateur=(OPERATEUR_MATH_PUISSANCE | OPERATEUR_MATH_RACINE_CARREE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_FOIS | OPERATEUR_MATH_DIVISE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) droite=expression_numerique
        | (atome_numerique | variable)
        | operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) sousExpression=expression_numerique
        | PARENTHESE_GAUCHE sousExpression=expression_numerique PARENTHESE_DROITE
        ;

expression_textuelle : atome_textuel ;

atome_textuel : chaine_de_caractere ;
atome_numerique : nombre ;

PARENTHESE_GAUCHE : '(' ;
PARENTHESE_DROITE : ')' ;

LA_VALEUR : 'la valeur' ;

variable : (LA_VALEUR ('de' | 'enregistrée dans'))? la_zone_memoire;

chaine_de_caractere : LE_TEXTE? VALEUR_TEXTE ;
LE_TEXTE : 'le texte' ;
VALEUR_TEXTE : '"' ~["]* '"' ;

nombre : (LE_NOMBRE|LA_VALEUR)? VALEUR_NOMBRE ;
LE_NOMBRE : 'le nombre' ;
VALEUR_NOMBRE : CHIFFRE+ (POINT CHIFFRE+)? ;

OPERATEUR_MATH_PLUS : '+' | 'plus' ;
OPERATEUR_MATH_MOINS : '-' | 'moins' ;
OPERATEUR_MATH_FOIS : '*' | 'fois' ;
OPERATEUR_MATH_DIVISE : '/' | 'divisé par';
OPERATEUR_MATH_PUISSANCE : '^' | 'élevé '? 'à la puissance';
OPERATEUR_MATH_RACINE_CARREE : 'racine carrée de' ;

fragment CHIFFRE : '0'..'9' ;

fragment MINUSCULE : 'a'..'z' ;
fragment MAJUSCULE : 'A'..'Z' ;
fragment SYMBOLES_LETTRE : [-_] ;
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
