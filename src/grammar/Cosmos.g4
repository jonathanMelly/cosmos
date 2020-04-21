﻿﻿//langage de type 'pseudo-code' pour apprendre à programmer
grammar Cosmos ;

//Removes clscompliant warning on build
@header {#pragma warning disable 3021}

programme : entete RETCHAR+ mainStart RETCHAR+ instruction+ RETCHAR? RETCHAR? RETCHAR? mainEnd .*? EOF ;

entete : auteur RETCHAR date RETCHAR entreprise RETCHAR description ; 

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

description : DESCRIPTION_ENTETE MOT+ (VIRGULE RETCHAR MOT+)* ;
DESCRIPTION_ENTETE : 'Description:' ;

mainStart: DEBUT nomDuProgramme=MOT (BIBLIOTHEQUE bibliotheque=MOT)? DEUX_POINT ;
mainEnd: FIN DE_LA_TRANSMISSION? POINT;
DEBUT : 'Voici les ordres du programme' ;
BIBLIOTHEQUE: 'à classer dans la bibliothèque' ;
FIN   : 'Fin' ;
DE_LA_TRANSMISSION : 'de la transmission' ;

instruction : (instruction_simple | instruction_complexe) ;

instruction_simple   : TAB+ (afficher|allouer|affecter) POINT RETCHAR ; //terminaison identique pour chaque
instruction_complexe : TAB+ (selection) ; //terminaison spécifique pour chaque

afficher : 'Afficher' expression;
allouer : 'Allouer' zone_memoire ('avec' expression)? ;
affecter : ('Copier' expression 'dans' zone_memoire) | (variable OPERATEUR_MATH_EGAL expression ) ;

zone_memoire : ZONE? VARIABLE ;
ZONE : 'la zone mémoire' ;

selection : 
    'Si' base_si 
    sinon_si* 
    sinon? 
    TAB+ POINT_INTERROGATION RETCHAR ;
 
base_si : condition=expression_booleenne 'alors' RETCHAR instruction+ ;
sinon_si : TAB+ 'sinon si' base_si ;
sinon : TAB+ 'et sinon' RETCHAR instruction+ ;

OPERATEUR_COMPARAISON_EQUIVALENT : 'vaut' | 'est égal à' | '==' ;
OPERATEUR_COMPARAISON_DIFFERENT : 'est différent de' | 'n\'est pas égal à' | '!=' | '<>' ;
OPERATEUR_COMPARAISON_PLUS_GRAND : 'est plus grand que' | '>' ;
OPERATEUR_COMPARAISON_PLUS_PETIT : 'est plus petit que' | '<';
OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL : 'est plus grand ou égal à' | '>=' ;
OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL : 'est plus petit ou égal à' | '<=';

VRAI: 'vrai'|'OK' ;
FAUX: 'faux'|'KO' ;

OPERATEUR_LOGIQUE_ET: 'et' | '&&' ;
OPERATEUR_LOGIQUE_OU: 'ou' | '||' ;
OPERATEUR_LOGIQUE_OU_EXCLUSIF: 'ou au contraire' | 'xor' ;
OPERATEUR_LOGIQUE_EST : 'est' ;
OPERATEUR_LOGIQUE_NON : 'l\'inverse de' | '!' | 'not' ;

OPERATEUR_MATH_EGAL : '=' ;
VARIABLE : PREFIXE_VARIABLE (MOT|VALEUR_NOMBRE) ;
PREFIXE_VARIABLE : '#' ;

expression 
        : expression_non_booleenne
        | expression_booleenne
        | variable
        ;

expression_non_booleenne 
        : expression_textuelle 
        | expression_numerique 
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_booleenne 
        : gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_OU droite=expression_booleenne 
        | gauche=expression_booleenne operateur=(OPERATEUR_LOGIQUE_ET|OPERATEUR_LOGIQUE_OU_EXCLUSIF) droite=expression_booleenne //priorité différente ?
        | gaucheNb=expression_non_booleenne 
            operateurNb=(  OPERATEUR_COMPARAISON_EQUIVALENT 
                         | OPERATEUR_COMPARAISON_DIFFERENT
                         | OPERATEUR_COMPARAISON_PLUS_GRAND
                         | OPERATEUR_COMPARAISON_PLUS_GRAND_OU_EGAL
                         | OPERATEUR_COMPARAISON_PLUS_PETIT
                         | OPERATEUR_COMPARAISON_PLUS_PETIT_OU_EGAL) 
         droiteNb=expression_non_booleenne
        | gauche=expression_booleenne operateur=(OPERATEUR_COMPARAISON_EQUIVALENT | OPERATEUR_COMPARAISON_DIFFERENT) droite=expression_booleenne
        | gauche=expression_booleenne operateur=OPERATEUR_LOGIQUE_EST (VRAI|FAUX) //construction sympa ;-)
        | OPERATEUR_LOGIQUE_NON sousExpression=expression_booleenne
        | (VRAI | FAUX)
        | PARENTHESE_GAUCHE sousExpression=expression_booleenne PARENTHESE_DROITE
        ;

//Exprimée dans l'ordre de priorité des opérateurs
expression_numerique 
        : gauche=expression_numerique operateur=(OPERATEUR_MATH_PUISSANCE | OPERATEUR_MATH_RACINE_CARREE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_FOIS | OPERATEUR_MATH_DIVISE) droite=expression_numerique
        | gauche=expression_numerique operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) droite=expression_numerique
        | atome_numerique
        | operateur=(OPERATEUR_MATH_PLUS | OPERATEUR_MATH_MOINS) sousExpression=expression_numerique
        | PARENTHESE_GAUCHE sousExpression=expression_numerique PARENTHESE_DROITE
        ;

expression_textuelle : atome_textuel ; //todo ou concaténation ... ?

atome_textuel : chaine_de_caractere ;
atome_numerique : nombre ;

PARENTHESE_GAUCHE : '(' ;
PARENTHESE_DROITE : ')' ;

variable : ('la valeur de')? VARIABLE;

chaine_de_caractere : LE_TEXTE? VALEUR_TEXTE ;
LE_TEXTE : 'le texte' ;
VALEUR_TEXTE : '"' ~["]* '"' ;

nombre : LE_NOMBRE? VALEUR_NOMBRE ;
LE_NOMBRE : 'le nombre' ;
VALEUR_NOMBRE : CHIFFRE+ (POINT CHIFFRE+)? ;

OPERATEUR_MATH_PLUS : '+' | 'plus' ;
OPERATEUR_MATH_MOINS : '-' | 'moins' ;
OPERATEUR_MATH_FOIS : '*' | 'fois' ;
OPERATEUR_MATH_DIVISE : '/' | 'divisé par';
OPERATEUR_MATH_PUISSANCE : '^' | 'élevé '? 'à la puissance';
OPERATEUR_MATH_RACINE_CARREE : 'racine carrée de' ;

MOT : LETTRE+ ;

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

TAB : '\t' | '    ' ;
RETCHAR : '\r'? '\n' ; 

//fragment CARACTERE : ~[."\\\r\n ] ;

ESPACE: ' '+ -> skip ;
COMMENTAIRE_LIGNE: (TAB? | TAB+) '//' ~[\n]* RETCHAR -> skip ;
COMMENTAIRE : '/*' .*? '*/' -> skip ;
NOOP : TAB+ RETCHAR -> skip ;

//WS: [ \t\r\n\u000C]+ -> channel(HIDDEN);
//WS : [ \r\n\t] + -> skip ;
//WS : [ ] + -> channel(HIDDEN) ;
